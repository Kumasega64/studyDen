# services/book_service.py

from sqlmodel import Session
from models import Book
from fastapi import HTTPException

def get_book_service(book_id: int, session: Session):
    book = session.get(Book, book_id)
    if not book:
        raise HTTPException(status_code=404, detail="Book not found")
    return book