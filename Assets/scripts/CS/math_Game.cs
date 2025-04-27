//using System;
//using System.ComponentModel.Design;
//using System.Reflection.Metadata.Ecma335;
//using MathNet.Symbolics;
//using System.Linq;
//using Expr = MathNet.Symbolics.SymbolicExpression;

//namespace MathGame
//{

//    class Program // this is the masin program 
//    {
//        static void Main()
//        {
//            Game timer = new Game();
//            timer.Timer(); //Start the game
//        }
//    }

//    class EquationGenerator
//    {
//        static Random obj = new Random();

//        public string GenerateEquation(out double CorrectAnswer)
//        {
//            int random = obj.Next(1,7);
//            double x = obj.Next(1,20);
//            double y = obj.Next(1,20);
//            double z = obj.Next(1, 20);

//            //For square root question we only want a list of perfect squares
//            int[] perfect_squares = {1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121, 144};
//            double s = perfect_square[obj.Next(perfect_square.Length)];

//            CorrectAnswer = ProblemInt(x, y, s, random);
//            string AnswerString = ProblemString(x, y, s, random);
//            return AnswerString;

//        }
    

//        private string ProblemString (double x, double y, double s, int random)
//        {
//            switch (random)
//            {
//                case 1:
//                    //Addition
//                    return $"{x} + {y}";
//                case 2:
//                    //Subtraction 
//                    return $"{x} - {y}";
//                case 3: 
//                    //Multiplication
//                    return $"{x} x {y}";
//                case 4:
//                    //Divison
//                    return $"{x} / {y}\nRound two decimal places";
//                case 5: 
//                    //SquareRoot
//                    return $"√{s}";
//                case 6:
//                    return $"{x}*({y}+x ={y}\nSlove for x";
//                default:
//                    return "Error";

//            }
//        }
    

//        private Double ProblemInt(double x, double y, double s, int random )
//        {
//            switch (random)
//            {
//                case 1:
//                    //Addition
//                    return x + y;
//                case 2:
//                    //Subtraction
//                    return x - y;
//                case 3:
//                    return x * y;
//                case 4:
//                    //Divison 
//                    double remanderDouble = x / y;
//                    string remanderString = remanderDouble.ToString("F2");
//                    double remanderTwoDecimel = double.Parse(remanderString);
//                    return remanderTwoDecimel;
//                case 5:
//                    //SquareRoot
//                    return Math.Sqrt(s);
//                case 6:
//                    //Equation
//                    var xVar = Expr.Variable("x");
//                    var equation = 3 * (8 +xVar) -30;
//                    var solutions = MathNet.Symbolics.Solve.Linear(equation, xVar);
//                    return solutions.First().RealValue;
//                default:
//                    throw new ArgumentOutOfRangeException("Invalid operation specified.");
//            }
//        }     
//    }


//    class Game 
//    {
//        private int score = 0 ; // Declare score as a class-level variable
//        public void Timer()
//        {
//            EquationGenerator generation = new EquationGenerator();

//            var startTime = DateTime.UtcNow;
//            TimeSpan TimeLimit = TimeSpan.FromMinutes(1);

//            while (DateTime.UtcNow - startTime < timeLimit)
//            {
//                double CorrectAnswer;
//                string problem = generation.GenerateEquation(out CorrectAnswer);
//                Console.WriteLine($"Slove: {problem}")

//                double answer;
//                while (!double.TryParse(Console.Readline(), out answer))
//                {
//                    Console.Write("This is not valid input. Please enter an");
//                }

//                if(answer == CorrectAnswer)
//                {
//                    score++;
//                    Console.WriteLine("Correct Answer now try this one");
//                }
//                else
//                {
//                    Console.WriteLine("Wrong Try again")
//                }
//        }

//        Console.WriteLine($"Your final score was {score}");
//    }
//}