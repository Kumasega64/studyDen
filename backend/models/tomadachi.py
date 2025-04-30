from sqlmodel import SQLModel, Field, ForeignKey
from datetime import datetime
from typing import Optional

class Tomadachi(SQLModel, table=True):
    id: Optional[int] = Field(default=None, primary_key=True)
    user_id: int = Field(foreign_key="user.id", unique=True)
    name: str
    species: str
    level: int
    experience: int
    created_at: datetime