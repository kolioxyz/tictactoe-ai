using System;
using System.Linq;

namespace TicTacToe
{
    public class Ai
    {
        public bool Mark { get; set; }

        public Ai(bool _mark = true)
        {
            Mark = _mark; // true: O, false: X
        }

        public int BestMove(Grid pos)
        {
            int bestmove;
            int[] possible = PossibleMoves(pos);
            int[] vals = new int[possible.Length];

            for (int i = 0; i < possible.Length; i++)
            {
                vals[i] = Minimax(pos.NextState(possible[i]), false);
            }

            bestmove = possible[Array.IndexOf(vals, vals.Max())];

            return bestmove;
        }

        private int MinimaxImproved(Grid position, bool minOrMax)
        {
            // TODO
            return 0;
        }

        private int Minimax(Grid position, bool minOrMax)
        {
            if (position.State != "playing")            // GAME HAS ENDED (leaf nodes)
            {
                if (position.State == "tie")
                    return 0;
                if (position.State == "X won")
                {
                    if (position.Turn == this.Mark)     // if it is the ai's turn and the game has ended => it has lost
                        return -1;
                    else
                        return 1;
                }
                else // if (position.state == "O won")
                {
                    if (position.Turn == this.Mark)     // same here
                        return -1;
                    else
                        return 1;
                }
            }
            else                                        // ELSE GAME HAS NOT ENDED
            {
                int[] poss = PossibleMoves(position);
                int[] vals = new int[poss.Length];
                int val;

                if (minOrMax)
                {
                    for (int i = 0; i < poss.Length; i++)
                    {
                        vals[i] = Minimax(position.NextState(poss[i]), false);
                    }
                    val = vals.Max();
                }
                else
                {
                    for (int i = 0; i < poss.Length; i++)
                    {
                        vals[i] = Minimax(position.NextState(poss[i]), true);
                    }
                    val = vals.Min();
                }


                return val;
            }
        }

        public static int[] PossibleMoves(Grid position)
        {
            // return array of numbers 1 - 9 for empty positions on grid
            int[] possible = new int[position.EmptySpaces];
            int n = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (position.G[i, j] == ' ')
                    {
                        possible[n] = i * 3 + (j + 1);
                        n++;
                    }
                }
            }
            return possible;
        }
    }
}
