# StudyDen Backend API

A FastAPI-based backend service for the StudyDen application, providing RESTful APIs for managing questions, books, users, and AI-powered lesson generation.

## 🚀 Features

- **User Management**: Complete user authentication and profile management
- **Question Management**: CRUD operations for study questions
- **Book Management**: Organize and manage study materials
- **AI Integration**: OpenAI-powered lesson generation
- **Tomadachi System**: Interactive learning companion features
- **RESTful API**: Well-documented endpoints with automatic OpenAPI documentation

## 🛠️ Tech Stack

- **Framework**: FastAPI
- **Database**: SQLite with SQLModel ORM
- **Authentication**: Passlib with bcrypt
- **AI Integration**: OpenAI API
- **Image Processing**: OpenCV and Tesseract OCR
- **Environment**: Python 3.8+

## 📋 Prerequisites

- Python 3.8 or higher
- pip (Python package installer)
- OpenAI API key

## 🚀 Quick Start

### 1. Clone the Repository

```bash
git clone <repository-url>
cd studyDen/backend
```

### 2. Set Up Virtual Environment

```bash
# Create virtual environment
python -m venv venv

# Activate virtual environment
# On Linux/macOS:
source venv/bin/activate
# On Windows:
venv\Scripts\activate
```

### 3. Install Dependencies

```bash
pip install -r requirement.txt
```

### 4. Environment Configuration

Create a `.env` file in the `/backend` directory:

```bash
# .env
OPENAI_API_KEY=your_openai_api_key_here
```

### 5. Run the Application

```bash
# Development mode
uvicorn main:app --reload

# Or using FastAPI CLI
fastapi dev
```

### 6. Access the API

- **API Documentation**: http://localhost:8000/docs
- **Alternative Docs**: http://localhost:8000/redoc
- **Health Check**: http://localhost:8000/

## 📚 API Endpoints

The API is organized into the following modules:

- **Questions** (`/api/questions`): Manage study questions
- **Books** (`/api/books`): Handle study materials and books
- **Users** (`/api/users`): User authentication and management
- **Tomadachi** (`/api/tomadachi`): Interactive learning companion
- **Generate Lesson** (`/api/generate-lesson`): AI-powered lesson generation

## 🐳 Docker Support

The project includes Docker support for easy deployment:

```bash
# Build the Docker image
docker build -t studyden-backend .

# Run the container
docker run -p 8000:8000 studyden-backend
```

## 📁 Project Structure

```
backend/
├── main.py              # FastAPI application entry point
├── requirements.txt     # Python dependencies
├── .env                 # Environment variables (create this)
├── Dockerfile          # Docker configuration
├── .dockerignore       # Docker ignore file
├── study_den.db        # SQLite database
├── config/             # Configuration files
├── db/                 # Database models and session
├── models/             # Data models
├── routers/            # API route handlers
├── schemas/            # Pydantic schemas
├── services/           # Business logic services
└── utils/              # Utility functions
```

## 🔧 Development

### Running Tests

```bash
# Add test commands here when tests are implemented
pytest
```

### Code Formatting

```bash
# Add formatting commands here
black .
isort .
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

If you encounter any issues or have questions:

1. Check the API documentation at `/docs`
2. Review the logs for error messages
3. Ensure your environment variables are properly configured
4. Verify your OpenAI API key is valid and has sufficient credits

---

**Happy Coding! 🎉**