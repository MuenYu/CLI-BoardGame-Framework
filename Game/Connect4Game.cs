using System.Data.Common;

namespace Game
{
    internal class Connect4Game : Game
    {
        const int ROW = 6, COLUMN = 7;


        string[] symbol = new string[] { "O", "X" };

        public Connect4Game(Board board, Player[] players, int curPlayer, HelpSystem helpSystem, HistoryManager historyManager, GameSaver gameSaver)
            : base(board, players, curPlayer, helpSystem, historyManager, gameSaver)
        {
        }


        public override bool CheckAndExeCmd(string cmd)
        {
            int column = int.Parse(cmd.Substring(5));
            // put the piece on the board
            for (int row = Board.Cells[Board.Cells.Length].Length - 1; row >= 0; row--)
            {
                if (board.Cells[row][column] == null)
                {
                    board.Cells[row][column] = new Piece(symbol[curPlayer]);
                    
                }
            }
            return false;
        }
        /*
        public override bool ExecuteCmd(string cmd)
        {
            if (!CheckAndParseCmd(cmd)) return false;

            if (cmd.StartsWith("drop "))
            {
                int column;
                if (Int32.TryParse(cmd.Substring(5), out column))
                {
                    column--; // Adjust for 0-based indexing
                    if (column >= 0 && column < board.Columns)
                    {
                        return DropPiece(column);
                    }
                    else
                    {
                        Console.WriteLine("Invalid column number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid column number format.");
                }
            }
            else
            {
                Console.WriteLine("Unsupported command.");
            }

            return true;
        }
        */

        /*

        private bool DropPiece(int column)
        {
            for (int row = Board.Cells.Length - 1; row >= 0; row--)
            {
                if (board.Cells[row][column] == null)
                {
                    board.Cells[row][column] = new Piece(players[curPlayer].Symbol);
                    return CheckWinner(row, column);
                }
            }

            Console.WriteLine("Column is full. Try a different column.");
            return true; // Continue with the same player's turn
        }
        */

        public bool CheckChain()
        {
            // Check rows
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Piece piece1 = Board.Cells[row][col];
                    Piece piece2 = Board.Cells[row][col + 1];
                    Piece piece3 = Board.Cells[row][col + 2];
                    Piece piece4 = Board.Cells[row][col + 3];

                    if (piece1.Sign == symbol[curPlayer] && piece2.Sign == symbol[curPlayer] &&
                        piece3.Sign == symbol[curPlayer] && piece4.Sign == symbol[curPlayer])
                    {
                        return true;
                    }
                }
            }

            // Check columns
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Piece piece1 = Board.Cells[row][col];
                    Piece piece2 = Board.Cells[row + 1][col];
                    Piece piece3 = Board.Cells[row + 2][col];
                    Piece piece4 = Board.Cells[row + 3][col];
                    if (piece1.Sign == symbol[curPlayer] && piece2.Sign == symbol[curPlayer] &&
                        piece3.Sign == symbol[curPlayer] && piece4.Sign == symbol[curPlayer])
                    {
                        return true;
                    }
                }
            }

            // Check diagonals
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Piece piece1 = Board.Cells[row][col];
                    Piece piece2 = Board.Cells[row + 1][col + 1];
                    Piece piece3 = Board.Cells[row + 2][col + 2];
                    Piece piece4 = Board.Cells[row + 3][col + 3];
                    Piece piece5 = Board.Cells[row][col + 3];
                    Piece piece6 = Board.Cells[row + 1][col + 2];
                    Piece piece7 = Board.Cells[row + 2][col + 1];
                    Piece piece8 = Board.Cells[row + 3][col];
                    if (piece1.Sign == symbol[curPlayer] && piece2.Sign == symbol[curPlayer] &&
                        piece3.Sign == symbol[curPlayer] && piece4.Sign == symbol[curPlayer])
                    {
                        return true;
                    }
                    if (piece5.Sign == symbol[curPlayer] && piece6.Sign == symbol[curPlayer] &&
                        piece7.Sign == symbol[curPlayer] && piece8.Sign == symbol[curPlayer])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override List<string> CalculateLegalMoves()
        {
            List<string> legalMoves = new List<string>();
            for (int column = 0; column < Board.Cells[Board.Cells.Length].Length; ++column)
            {
                legalMoves.Add($"drop {column + 1}");
            }
            return legalMoves;
        }

        protected override bool IsGameEnd()
        {
            if(CheckChain())
                return true;

            for (int i = 0; i < Board.Cells.Length; ++i)
            {
                for (int j = 0; j < Board.Cells[i].Length; ++j)
                {
                    if (Board.Cells[i][j] == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void CheckWinner()
        {
            if (CheckChain())
                Console.WriteLine("{0} wins!", curPlayer);
            else
                Console.WriteLine("It's a draw.");
        }
    }
}


