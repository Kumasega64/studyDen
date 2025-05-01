# secrets.py
import os
from dotenv import load_dotenv
from pydantic_settings import BaseSettings

load_dotenv()  # Load variables from .env into environment

class Settings(BaseSettings):
    OPENAI_API_KEY: str

    class Config:
        env_file = ".env"

secrets = Settings()
