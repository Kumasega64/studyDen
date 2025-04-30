from fastapi import APIRouter, Depends, HTTPException
from sqlmodel import Session, select
from models import Question
from db.session import get_session
from typing import List
from datetime import datetime
import json

from schemas.question import QuestionCreate, QuestionRead, QuestionUpdate

router = APIRouter(prefix="/questions", tags=["Questions"])


@router.post("/", response_model=QuestionRead)
def create_question(data: QuestionCreate, session: Session = Depends(get_session)):
    question = Question(
        book_id=data.book_id,
        question_text=data.question_text,
        correct_answer=data.correct_answer,
        created_at=datetime.utcnow()
    )
    question.wrong_answers = data.wrong_answers

    session.add(question)
    session.commit()
    session.refresh(question)
    return question


@router.get("/", response_model=List[QuestionRead])
def get_all_questions(session: Session = Depends(get_session)):
    return session.exec(select(Question)).all()


@router.get("/book/{book_id}", response_model=List[QuestionRead])
def get_questions_by_book(book_id: int, session: Session = Depends(get_session)):
    return session.exec(select(Question).where(Question.book_id == book_id)).all()


@router.get("/{question_id}", response_model=QuestionRead)
def get_question(question_id: int, session: Session = Depends(get_session)):
    question = session.get(Question, question_id)
    if not question:
        raise HTTPException(status_code=404, detail="Question not found")
    return question


@router.delete("/{question_id}", response_model=Question)
def delete_question(question_id: int, session: Session = Depends(get_session)):
    question = session.get(Question, question_id)
    if not question:
        raise HTTPException(status_code=404, detail="Question not found")
    session.delete(question)
    session.commit()
    return question

@router.put("/{question_id}", response_model=QuestionRead)
def update_question(
    question_id: int,
    data: QuestionUpdate,
    session: Session = Depends(get_session)
):
    question = session.get(Question, question_id)
    if not question:
        raise HTTPException(status_code=404, detail="Question not found")

    if data.question_text is not None:
        question.question_text = data.question_text
    if data.correct_answer is not None:
        question.correct_answer = data.correct_answer
    if data.wrong_answers is not None:
        question.wrong_answers = data.wrong_answers  # triggers setter

    session.commit()
    session.refresh(question)
    return question
