from pydantic import BaseModel
from datetime import datetime
from typing import Optional

class BookCreate(BaseModel):
    user_id: int
    title: str
    description: str
    category: Optional[str] = None

class BookRead(BaseModel):
    id: int
    user_id: int
    title: str
    description: str
    category: Optional[str]
    created_at: datetime

    class Config:
        orm_mode = True

class BookUpdate(BaseModel):
    title: Optional[str]
    description: Optional[str]
    category: Optional[str]  # user-assigned tag

    class Config:
        orm_mode = True