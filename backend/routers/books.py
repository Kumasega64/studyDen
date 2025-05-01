#books.py routers

from fastapi import APIRouter, Depends, HTTPException
from sqlmodel import Session, select
from services.book_service import get_book_service
from models import Book
from db.session import get_session  # Assumes you have a `get_session` dependency
from typing import List
from datetime import datetime
from schemas.book import BookCreate, BookRead

router = APIRouter(prefix="/books", tags=["Books"])


@router.post("/", response_model=BookRead)
def create_book(book: BookCreate, session: Session = Depends(get_session)):
    db_book = Book(
        user_id=book.user_id,
        title=book.title,
        description=book.description,
        category=book.category,
        created_at=datetime.utcnow()
    )

    session.add(db_book)
    session.commit()
    session.refresh(db_book)
    return db_book


@router.get("/", response_model=List[BookRead])
def get_books(session: Session = Depends(get_session)):
    books = session.exec(select(Book)).all()
    return books


@router.get("/{book_id}", response_model=BookRead)
def get_book(book_id: int, session: Session = Depends(get_session)):
    return get_book_service(book_id, session)


@router.delete("/{book_id}", response_model=BookRead)
def delete_book(book_id: int, session: Session = Depends(get_session)):
    book = session.get(Book, book_id)
    if not book:
        raise HTTPException(status_code=404, detail="Book not found")
    session.delete(book)
    session.commit()
    return book
