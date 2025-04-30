from fastapi import FastAPI, Depends
from sqlmodel import Session, select
from db.session import engine, create_db_and_tables
from models import User, Book
from routers import questions, books, users

app = FastAPI()

@app.on_event("startup")
def on_startup():
    create_db_and_tables()

def get_session():
    with Session(engine) as session:
        yield session

# Include the router
app.include_router(questions.router, prefix="/api", tags=["Questions"])
app.include_router(books.router, prefix="/api", tags=["Books"])
app.include_router(users.router, prefix="/api", tags=["Users"])

@app.get("/")
def root():
    return {"message": "Tomadachi API running!"}

# @app.post("/books/", response_model=Book)
# def create_book(book: Book, session: Session = Depends(get_session)):
#     session.add(book)
#     session.commit()
#     session.refresh(book)
#     return book

# @app.get("/books/", response_model=list[Book])
# def get_books(session: Session = Depends(get_session)):
#     books = session.exec(select(Book)).all()
#     return books
