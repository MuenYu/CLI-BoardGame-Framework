namespace Game
{
    internal class Connect4Game : Game
    {
        int col;
        const int ROW = 5, COLUMN = 6;        
        string[] symbol = new string[] { "O", "X" };

        public Connect4Game(Board board, Player[] players, int curPlayer, HelpSystem helpSystem, HistoryManager historyManager, GameSaver gameSaver)
            : base(board, players, curPlayer, helpSystem, historyManager, gameSaver)
        {
        }

        

        public override bool CheckAndExeCmd(string cmd)
        {
            if (!CheckAndParseCmd(cmd)) return false;
            // put the piece on the board
            for (int row = ROW; row >= 0; row--)
            {
                if (Board.Cells[row][col] == null)
                {
                    Board.Cells[row][col] = new Piece(symbol[curPlayer]);
                    return true;
                }
                
            }
            return true;
        }


        protected override bool CheckAndParseCmd(string cmd)
        {
            const string FormatIssue = "wrong command format";
            const string UnsupportedCmd = "unsupported command";
            const string WrongParameter = "wrong Parameter";
            const string ExistingPiece = "this column is full";

            bool flag = base.CheckAndParseCmd(cmd);
            if (flag) // if need further check
            {
                string[] fragments = cmd.Split(' ');
                // command format is not correct
                if (fragments.Length != 2) throw new Exception($"{FormatIssue}");
                // the command is not 'drop'
                else if (fragments[0] != "drop") throw new Exception(UnsupportedCmd);
                
                // if the second parameter is not integer
                else if (!Int32.TryParse(fragments[1], out col)) throw new Exception($"{WrongParameter}: the first parameter must be an integer");

                // input coord starts from 1, but array starts from 0
                col = col - 1;
                // if the coordinate is not valid for the board
                if (col < 0 || COLUMN < col) throw new Exception($"{WrongParameter}: the row or column is out of board");
                
                // if the place has piece already
                else if (Board.Cells[0][col] != null) throw new Exception(ExistingPiece);
                
            }
            return flag;
        }

        public bool CheckChain()
        {
            int lastPlayer = (curPlayer + 1) % 2;
            // Check rows
            for (int rows = 0; rows < 6; rows++)
            {
                for (int cols = 0; cols < 4; cols++)
                {
                    Piece piece1 = Board.Cells[rows][cols];
                    Piece piece2 = Board.Cells[rows][cols + 1];
                    Piece piece3 = Board.Cells[rows][cols + 2];
                    Piece piece4 = Board.Cells[rows][cols + 3];

                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        if (piece1.Sign == symbol[lastPlayer] && piece2.Sign == symbol[lastPlayer] &&
                            piece3.Sign == symbol[lastPlayer] && piece4.Sign == symbol[lastPlayer])
                        {
                            return true;
                        }
                    }
                }
            }


            // Check columns
            for (int rows = 0; rows < 3; rows++)
            {
                for (int cols = 0; cols < 7; cols++)
                {
                    Piece piece1 = Board.Cells[rows][cols];
                    Piece piece2 = Board.Cells[rows + 1][cols];
                    Piece piece3 = Board.Cells[rows + 2][cols];
                    Piece piece4 = Board.Cells[rows + 3][cols];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        if (piece1.Sign == symbol[lastPlayer] && piece2.Sign == symbol[lastPlayer] &&
                        piece3.Sign == symbol[lastPlayer] && piece4.Sign == symbol[lastPlayer])
                        {
                            return true;
                        }
                    }
                }
            }

            // Check diagonals
            for (int rows = 0; rows < 3; rows++)
            {
                for (int cols = 0; cols < 4; cols++)
                {
                    Piece piece1 = Board.Cells[rows][cols];
                    Piece piece2 = Board.Cells[rows + 1][cols + 1];
                    Piece piece3 = Board.Cells[rows + 2][cols + 2];
                    Piece piece4 = Board.Cells[rows + 3][cols];
                    Piece piece5 = Board.Cells[rows][cols + 3];
                    Piece piece6 = Board.Cells[rows + 1][cols + 2];
                    Piece piece7 = Board.Cells[rows + 2][cols + 1];
                    Piece piece8 = Board.Cells[rows + 3][cols];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        if (piece1.Sign == symbol[lastPlayer] && piece2.Sign == symbol[lastPlayer] &&
                        piece3.Sign == symbol[lastPlayer] && piece4.Sign == symbol[lastPlayer])
                        {
                            return true;
                        }
                    }
                    if (piece5 != null && piece6 != null && piece7 != null && piece8 != null)
                    {
                        if (piece5.Sign == symbol[lastPlayer] && piece6.Sign == symbol[lastPlayer] &&
                        piece7.Sign == symbol[lastPlayer] && piece8.Sign == symbol[lastPlayer])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override List<string> CalculateLegalMoves()
        {
            List<string> legalMoves = new List<string>();
            for (int i = 0; i <= COLUMN; ++i)
            {
                legalMoves.Add($"drop {i + 1}");
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
            int lastPlayer = (curPlayer + 1) % 2;
            if (CheckChain())
                Console.WriteLine("Player {0} wins!", lastPlayer + 1);
            else
                Console.WriteLine("It's a draw.");
        }
    }
}


