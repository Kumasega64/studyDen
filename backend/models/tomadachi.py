from sqlmodel import SQLModel, Field
from datetime import datetime
from typing import Optional

class Tomadachi(SQLModel, table=True):
    id: Optional[int] = Field(default=None, primary_key=True)
    user_id: int = Field(foreign_key="user.id", unique=False)
    name: str
    species: str = "Unknown"  # default value or make it optional
    level: int = 1
    experience: int = 0
    created_at: datetime = Field(default_factory=datetime.utcnow)