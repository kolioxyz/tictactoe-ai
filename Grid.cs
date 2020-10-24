using System;

namespace TicTacToe
{
    public class Grid
    {
        public char[,] G { get; set; }
        public bool Turn { get; set; }
        public string State { get; set; }
        public int EmptySpaces { get; set; }


        public Grid(bool _turn = false)
        {
            G = CreateGrid();
            Turn = _turn;
            State = "playing";
            EmptySpaces = 9;
        }
        public Grid(Grid previous)
        {
            G = previous.GetGrid();
            Turn = previous.Turn;
            State = previous.State;
            EmptySpaces = previous.EmptySpaces;
        }

        private char[,] CreateGrid()
        {
            return new char[3, 3]{
            { ' ', ' ', ' ',},
            { ' ', ' ', ' ',},
            { ' ', ' ', ' ',},
            };
        }

        private char[,] GetGrid()
        {
            char[,] grid = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid[i, j] = this.G[i, j];
                }
            }

            return grid;
        }

        // method: nextState(position)
        public Grid NextState(int position)
        {
            Grid next = new Grid(this);

            // position: 1 - 9
            int row = (position - 1) / 3;
            int column = (position - 1) % 3;


            if (next.G[row, column] != ' ') // invalid move, gof uck yourself
                return null; 


            // mark position
            if (this.Turn)
                next.G[row, column] = 'O';
            else
                next.G[row, column] = 'X';

            next.EmptySpaces -= 1;

            // check if someone won
            if (this.State == "playing")
            {
                // check diag/column/row if 3 match and are different than ' '; if so, someone won

                // check diagonals
                if (next.G[0, 0] == next.G[1, 1] && next.G[1, 1] == next.G[2, 2])
                {
                    if (next.G[0, 0] != ' ')
                        next.State = XorO();
                }

                if (next.G[2, 0] == next.G[1, 1] && next.G[1, 1] == next.G[0, 2])
                {
                    if (next.G[0, 2] != ' ')
                        next.State = XorO();
                }
                // check row
                if (next.G[row, column] == next.G[(row + 1) % 3, column] && next.G[row, column] == next.G[(row + 2) % 3, column])
                    next.State = XorO();
                // check column
                if (next.G[row, column] == next.G[row, (column + 1) % 3] && next.G[row, column] == next.G[row, (column + 2) % 3])
                    next.State = XorO();
                // check if someone won at last move
                if (next.State == "playing" && next.EmptySpaces == 0)
                    next.State = "tie";
            }


            next.Turn = !this.Turn; // change turn  
            return next;
        }

        private string XorO()
        {
            if (this.Turn)
                return "O won";
            else
                return "X won";
        }
    }
}
