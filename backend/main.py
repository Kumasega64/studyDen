from fastapi import FastAPI, Depends
from fastapi.middleware.cors import CORSMiddleware
from sqlmodel import Session, select
from db.session import engine, create_db_and_tables
from routers import questions, books, users, tomadachi, generate_lesson

app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

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
app.include_router(tomadachi.router, prefix="/api", tags=["Tomadachi"])
app.include_router(generate_lesson.router, prefix="/api", tags=["Generate Lesson"])

@app.get("/")
def root():
    return {"message": "Tomadachi API running!"}
