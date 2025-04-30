from sqlmodel import SQLModel, Field, ForeignKey
from datetime import datetime
from typing import Optional

class Book(SQLModel, table=True):
    id: Optional[int] = Field(default=None, primary_key=True)
    user_id: int = Field(foreign_key="user.id")
    title: str
    description: str
    category: Optional[str] = Field(default=None)
    created_at: datetime = Field(default_factory=datetime.utcnow)