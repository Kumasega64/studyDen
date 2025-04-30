from pydantic import BaseModel, root_validator
from typing import Optional, List
from datetime import datetime

class QuestionCreate(BaseModel):
    book_id: int
    question_text: str
    correct_answer: str
    wrong_answers: List[str]

class QuestionRead(BaseModel):
    id: int
    book_id: int
    question_text: str
    correct_answer: str
    wrong_answers: List[str]
    created_at: datetime

    class Config:
        orm_mode = True

class QuestionUpdate(BaseModel):
    question_text: Optional[str] = None
    correct_answer: Optional[str] = None
    wrong_answers: Optional[List[str]] = None

    @root_validator(pre=True)
    def at_least_one_field(cls, values):
        if not any(values.get(field) is not None for field in ["question_text", "correct_answer", "wrong_answers"]):
            raise ValueError("At least one field must be provided")
        return values
