import json
from sqlmodel import Field, SQLModel
from typing import Optional
from datetime import datetime


class Question(SQLModel, table=True):
    id: Optional[int] = Field(default=None, primary_key=True)
    book_id: int = Field(foreign_key="book.id")
    question_text: str
    correct_answer: str
    wrong_answers_json: str = Field(default="[]")  # Stored as a JSON string
    created_at: datetime = Field(default_factory=datetime.utcnow)

    @property
    def wrong_answers(self) -> list[str]:
        return json.loads(self.wrong_answers_json)

    @wrong_answers.setter
    def wrong_answers(self, value: list[str]):
        self.wrong_answers_json = json.dumps(value)
