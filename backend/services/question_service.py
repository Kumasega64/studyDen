from datetime import datetime
from sqlmodel import Session
from models.question import Question
from schemas.question import QuestionCreate, QuestionRead

def create_question_service(data: QuestionCreate, session: Session) -> Question:
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
