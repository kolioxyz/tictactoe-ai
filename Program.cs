using System;
using System.Linq;

namespace TicTacToe
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Grid game = new Grid();
            DisplayGrid(game.G);

            Console.WriteLine("Press + to let AI go first");
            Console.WriteLine("Press - to enter 2 player mode");

            char choice = Console.ReadKey().KeyChar;

            Console.Clear();
            DisplayGrid(game.G);

            switch (choice)
            {
                case '+':
                    game = HumanVSAIGame(game, new Ai(false));
                    break;
                case '-':
                    game = HumanVSHumanGame(game);
                    break;
                default:
                    int firstMove = CleanInput(choice);
                    game = game.NextState(firstMove);
                    game = HumanVSAIGame(game, new Ai(true));
                    break;
            }

            Console.WriteLine(game.State); // Game has ended
        }

        public static Grid HumanVSHumanGame(Grid grid)
        {
            while (grid.State == "playing")
            {
                int move;

                move = CleanInput(Console.ReadKey().KeyChar);
                Console.Clear();

                int[] possibleMoves = Ai.PossibleMoves(grid); // help stupid human trying to play impossible move

                if (possibleMoves.Contains(move) == false)
                {
                    Console.WriteLine("Please enter a valid move");
                }
                else
                {
                    grid = grid.NextState(move);
                }

                DisplayGrid(grid.G);
            }

            return grid;
        }

        public static Grid HumanVSAIGame(Grid grid, Ai robot)
        {
            while (grid.State == "playing")
            {
                int humanMove;
                int aiMove;

                if (grid.Turn != robot.Mark)
                {
                    humanMove = CleanInput(Console.ReadKey().KeyChar);
                    Console.Clear();

                    int[] possibleMoves = Ai.PossibleMoves(grid); // help stupid human trying to play impossible move

                    if (possibleMoves.Contains(humanMove) == false)
                    {
                        Console.WriteLine("Please enter a valid move");
                    }
                    else
                    {
                        grid = grid.NextState(humanMove);
                    }
                }
                else
                {
                    aiMove = robot.BestMove(grid);
                    Console.Clear();
                    grid = grid.NextState(aiMove);
                }

                DisplayGrid(grid.G);
            }

            return grid;
        }

        public static int CleanInput(char ch)
        {
            int pos = 0;
            do
            {
                bool isInt = int.TryParse(ch.ToString(), out pos);

                while (!isInt)
                {
                    Console.WriteLine("\nPlease enter a number");
                    isInt = int.TryParse(Console.ReadKey().KeyChar.ToString(), out pos);
                }
                if (pos == 0)
                    Console.WriteLine("\nPlease dont enter 0");
            }
            while (pos == 0);

            return pos;
        }

        public static void DisplayGrid(char[,] grid)
        {
            for (int i = 2; i >= 0; i--)
            {
                Console.Write(" ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(grid[i,j] + " | ");
                }
                Console.WriteLine("\n" + new string('-', 12));
            }
        }
    }
}
