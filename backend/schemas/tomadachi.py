from pydantic import BaseModel
from datetime import datetime
from typing import Optional

class TomadachiCreate(BaseModel):
    user_id: int
    name: str
    species: Optional[str] = "Unknown"

class TomadachiRead(BaseModel):
    id: int
    user_id: int
    name: str
    species: str
    level: int
    experience: int
    created_at: datetime

    class Config:
        orm_mode = True