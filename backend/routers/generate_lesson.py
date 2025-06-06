from fastapi import APIRouter, UploadFile, File, Form, HTTPException, Depends
from sqlalchemy.orm import Session
from sqlmodel import select
from PIL import Image
from schemas.question import QuestionCreate
from services.question_service import create_question_service
from services.book_service import get_book_service

import pytesseract
import io
from openai import OpenAI
import json

from db.session import get_session
from config.secrets import secrets
router = APIRouter()

 #Connect to chat gpt
client = OpenAI(
  api_key=secrets.OPENAI_API_KEY 
)

def extract_text_from_image(image_bytes: bytes) -> str:
    image = Image.open(io.BytesIO(image_bytes))
    return pytesseract.image_to_string(image)

@router.post("/generate-lesson/")
async def generate_lesson(
    book_id: int = Form(...),
    file: UploadFile = File(...),
    session: Session = Depends(get_session)
):
    try:
        #Check if book exists
        get_book_service(book_id, session)  # Will raise 404 if not found

        # Step 1: Read image bytesio
        image_bytes = await file.read()

        # Step 2: Extract text with Tesseract
        extracted_text = extract_text_from_image(image_bytes)
        if not extracted_text.strip():
            raise HTTPException(status_code=400, detail="No text found in image.")

        # Step 3: Generate quiz via ChatGPT
        prompt = (
            "You are a quiz generator. Given a passage of text, return multiple choice "
            "questions in JSON format with this schema:\n\n"
            "[\n"
            "  {\n"
            "    \"question\": \"string\",\n"
            "    \"wrong_answers\": [\"string\", \"string\", \"string\"],\n"
            "    \"correct_answer\": \"string\"\n"
            "  }\n"
            "]"
        )

        response = client.chat.completions.create(
            model="gpt-4o-mini",
            store=True,
            messages=[
                {"role": "system", "content": prompt},
                {"role": "user", "content": extracted_text}
            ]
        )
        
        print("GPT Output", response)
        gpt_output = response.choices[0].message.content

        # Step 4: Parse GPT response
        try:
            parsed_questions = json.loads(gpt_output)
        except json.JSONDecodeError:
            raise HTTPException(status_code=500, detail="Invalid JSON from GPT output.")

        if not isinstance(parsed_questions, list):
            raise HTTPException(status_code=500, detail="Expected a list of questions.")

        # Step 5: Save to database
        saved = 0
        for q in parsed_questions:
            if not all(k in q for k in ("question", "wrong_answers", "correct_answer")):
                continue  # skip invalid entries
            question_data = QuestionCreate(
                book_id=book_id,
                question_text=q["question"],
                correct_answer=q["correct_answer"],
                wrong_answers=q["wrong_answers"]
            )
            create_question_service(question_data, session) #Fix this
            saved += 1

        return {"status": "success", "questions_created": saved}

    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Internal error: {str(e)}")
