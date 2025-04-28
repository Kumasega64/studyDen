using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        QuizGame game = new QuizGame();
        game.Start();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey(); // Keeps the console window open after completion
    }
}

class QuizGame
{
    private int score = 0;

    // Define a Question structure
    struct Question
    {
        public string Text;
        public string CorrectAnswer;
        public List<string> WrongAnswers;

        public Question(string text, string correctAnswer, List<string> wrongAnswers)
        {
            Text = text;
            CorrectAnswer = correctAnswer;
            WrongAnswers = wrongAnswers;
        }
    }

    public void Start()
    {
        List<Question> questions = new List<Question>
        {
            new Question("What is my favorite color?", "green", new List<string>{ "blue", "red", "yellow" }),
            new Question("What planet is known as the Red Planet?", "Mars", new List<string>{ "Venus", "Earth", "Jupiter" }),
            new Question("Which language is used to write Unity scripts?", "C#", new List<string>{ "Java", "Python", "Ruby" }),
            new Question("Which data type holds true or false?", "bool", new List<string>{ "int", "string", "char" }),
            new Question("How many legs does a spider have?", "8", new List<string>{ "6", "10", "12" })
        };

        foreach (var question in questions)
        {
            AskQuestion(question);
        }

        Console.WriteLine($"\nYour final score: {score}/{questions.Count}");
    }

    private void AskQuestion(Question q)
    {
        Console.WriteLine($"\n{q.Text}");

        // Combine and shuffle answers
        List<string> allAnswers = new List<string>(q.WrongAnswers);
        allAnswers.Add(q.CorrectAnswer);
        Shuffle(allAnswers);

        char option = 'a';
        Dictionary<char, string> choices = new Dictionary<char, string>();

        foreach (var ans in allAnswers)
        {
            choices[option] = ans;
            Console.WriteLine($"{option}. {ans}");
            option++;
        }

        Console.Write("\nWhats the Answer: ");
        char userInput;
        while (!char.TryParse(Console.ReadLine().ToLower(), out userInput) || !choices.ContainsKey(userInput))
        {
            Console.Write("Invalid input. Try again: ");
        }

        if (choices[userInput] == q.CorrectAnswer)
        {
            score++;
            Console.WriteLine("Correct answer!");
        }
        else
        {
            Console.WriteLine($"Wrong answer! The correct answer was: {q.CorrectAnswer}");
        }
    }

    private void Shuffle(List<string> list)
    {
        Random rng = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int swapIndex = rng.Next(i + 1);
            string temp = list[i];
            list[i] = list[swapIndex];
            list[swapIndex] = temp;
        }
    }
}


