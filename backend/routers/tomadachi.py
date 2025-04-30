from fastapi import APIRouter, Depends
from sqlmodel import Session, select
from models import Tomadachi
from schemas.tomadachi import TomadachiCreate, TomadachiRead
from db.session import get_session
from typing import List

router = APIRouter(prefix="/tomadachi", tags=["Tomadachi"])

@router.post("/", response_model=TomadachiRead)
def create_tomadachi(data: TomadachiCreate, session: Session = Depends(get_session)):
    tomadachi = Tomadachi(**data.dict())
    session.add(tomadachi)
    session.commit()
    session.refresh(tomadachi)
    return tomadachi

@router.get("/{user_id}/{id}", response_model=TomadachiRead)
def get_single_tomadachi(user_id: int, id: int, session: Session = Depends(get_session)):
    statement = select(Tomadachi).where(
        Tomadachi.user_id == user_id,
        Tomadachi.id == id
    )
    tomadachi = session.exec(statement).first()
    if not tomadachi:
        raise HTTPException(status_code=404, detail="Tomadachi not found")
    return tomadachi


@router.get("/", response_model=List[TomadachiRead])
def get_tomadachi(user_id: int, session: Session = Depends(get_session)):
    statement = select(Tomadachi).where(Tomadachi.user_id == user_id)
    tomadachi_list = session.exec(statement).all()
    return tomadachi_list