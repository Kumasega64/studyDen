using System;
using System.Linq;

namespace MathGame
{
    class Program // This is the main program
    {
        static void Main()
        {
            Game timer = new Game();
            timer.Timer(); // Start the game
        }
    }

    class EquationGenerator
    {
        static Random obj = new Random();

        public string GenerateEquation(out double CorrectAnswer)
        {
            int random = obj.Next(1, 6);
            double x = obj.Next(1, 20);
            double y = obj.Next(1, 20);
            double z = obj.Next(1, 20);

            // For square root question, only use perfect squares
            int[] perfect_squares = { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121, 144 };
            double s = perfect_squares[obj.Next(perfect_squares.Length)];

            CorrectAnswer = ProblemInt(x, y, s, random);
            string AnswerString = ProblemString(x, y, s, random);
            return AnswerString;
        }

        private string ProblemString(double x, double y, double s, int random)
        {
            switch (random)
            {
                case 1:
                    return $"{x} + {y}";
                case 2:
                    return $"{x} - {y}";
                case 3:
                    return $"{x} x {y}";
                case 4:
                    return $"{x} / {y}\n(Round to two decimal places)";
                case 5:
                    return $"√{s}";
                default:
                    return "Error";
            }
        }

        private double ProblemInt(double x, double y, double s, int random)
        {
            switch (random)
            {
                case 1:
                    return x + y;
                case 2:
                    return x - y;
                case 3:
                    return x * y;
                case 4:
                    return Math.Round(x / y, 2);
                case 5:
                    return Math.Sqrt(s);
                default:
                    throw new ArgumentOutOfRangeException(nameof(random), random, "Invalid operation specified.");
            }
        }
    }

    class Game
    {
        private int score = 0;

        public void Timer()
        {
            EquationGenerator generation = new EquationGenerator();

            var startTime = DateTime.UtcNow;
            TimeSpan timeLimit = TimeSpan.FromSeconds(30);

            while (DateTime.UtcNow - startTime < timeLimit)
            {
                double correctAnswer;
                string problem = generation.GenerateEquation(out correctAnswer);
                Console.WriteLine($"Solve: {problem}");

                double answer;
                while (!double.TryParse(Console.ReadLine(), out answer))
                {
                    Console.Write("Invalid input. Please enter a number: ");
                }

                if (Math.Abs(answer - correctAnswer) < 0.01)
                {
                    score++;
                    Console.WriteLine("Correct! Now try this one:");
                }
                else
                {
                    Console.WriteLine($"Wrong. The correct answer was {correctAnswer}. Try another:");
                }
            }

            Console.WriteLine($"Time's up! Your final score was {score}");
        }
    }
}
