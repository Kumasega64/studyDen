from sqlmodel import SQLModel, Field, Relationship, ForeignKey
from datetime import datetime
from typing import Optional, List

class Book(SQLModel, table=True):
    id: Optional[int] = Field(default=None, primary_key=True)
    user_id: int = Field(foreign_key="user.id")
    title: str
    description: str
    category: Optional[str] = Field(default=None)
    last_studied: Optional[datetime] = Field(default=None)
    created_at: datetime = Field(default_factory=datetime.utcnow)

    questions: List["Question"] = Relationship(
        back_populates="book",
        sa_relationship_kwargs={"cascade": "all, delete-orphan"}
    )