from imageProcessing import getFullText, getText
import cv2
from openai import OpenAI
from dotenv import load_dotenv
import os

#load the API KEY
load_dotenv()
api_key = os.getenv("Chat_GPT_API")

#Connect to chat gpt
client = OpenAI(
  api_key=api_key
)

def getQuestions(imgs):
    all_questions = []
    for img in imgs:
        #we need to make sure the images are arrays
        #img = cv2.imread(img_path)

        text = getFullText(img)
        input= "Create 5 mutiple choice question make return the question and correct answer as a string and the wrong answers as a list from this text"+text

        completion = client.chat.completions.create(
            model="gpt-4o-mini",
            store=True,
            messages=[
                {"role": "user", "content": input }
            ]
        )

        # Get the response content
        questions_text = completion.choices[0].message.content
        
        # Append the generated questions to the list
        all_questions.append(questions_text)

    return all_questions



