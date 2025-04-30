from fastapi import APIRouter, Depends, HTTPException
from sqlmodel import Session, select
from models import Book
from db.session import get_session  # Assumes you have a `get_session` dependency
from typing import List
from datetime import datetime

router = APIRouter(prefix="/books", tags=["Books"])


@router.post("/", response_model=Book)
def create_book(book: Book, session: Session = Depends(get_session)):
    book.created_at = datetime.utcnow()
    session.add(book)
    session.commit()
    session.refresh(book)
    return book


@router.get("/", response_model=List[Book])
def get_books(session: Session = Depends(get_session)):
    books = session.exec(select(Book)).all()
    return books


@router.get("/{book_id}", response_model=Book)
def get_book(book_id: int, session: Session = Depends(get_session)):
    book = session.get(Book, book_id)
    if not book:
        raise HTTPException(status_code=404, detail="Book not found")
    return book


@router.delete("/{book_id}", response_model=Book)
def delete_book(book_id: int, session: Session = Depends(get_session)):
    book = session.get(Book, book_id)
    if not book:
        raise HTTPException(status_code=404, detail="Book not found")
    session.delete(book)
    session.commit()
    return book
