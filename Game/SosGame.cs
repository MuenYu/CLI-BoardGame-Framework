namespace Game
{
    internal class SosGame : Game
    {
        protected int[] scores;
        public int[] Scores { get { return scores; } }

        protected int curPieceRow;
        protected int curPieceCol;
        protected string curPieceSign;

        public SosGame(Board board, Player[] players, int curPlayer, HelpSystem helpSystem, HistoryManager historyManager, GameSaver gameSaver, int[] scores) 
            : base(board, players, curPlayer, helpSystem, historyManager, gameSaver)
        {

            this.scores = scores;
        }

        protected override Game Undo()
        {
            Game g = base.Undo();
            if(g is SosGame)
            {
                SosGame sos = (SosGame)g;
                scores = sos.scores;
            }
            return g;
        }

        public override List<string> CalculateLegalMoves()
        {
            List<string> legalMoves = new List<string>();
            for (int i = 0; i < Board.Cells.Length; ++i)
            {
                for (int j = 0; j < Board.Cells[i].Length; ++j)
                {
                    if (Board.Cells[i][j] == null)
                    {
                        legalMoves.Add($"place {i+1} {j+1} S");
                        legalMoves.Add($"place {i+1} {j+1} O");
                    }
                }
            }
            return legalMoves;
        }

        // return bool: 
        // true: continue this round
        // false: go to next round
        public override bool ExecuteCmd(string cmd)
        {
            if (!CheckAndParseCmd(cmd)) return false;
            // put the piece on the board
            Board.Cells[curPieceRow][curPieceCol] = new Piece(curPieceSign);
            return CheckScoring();
        }

        public bool CheckScoring()
        {
            int score = 0 ;
            switch (curPieceSign)
            {
                case "S":
                    score = CheckScore4S();
                    break;
                case "O":
                    score = CheckScore4O();
                    break;
            }
            if (score != 0)
            {
                Console.WriteLine($"{Players[CurPlayer].Name} connected SOS, one more turn");
            }
            scores[CurPlayer] += score;
            return score == 0; // once get score, continue another round
        }

        public int CheckScore4S()
        {
            int score = 0;
            int[] dr = new int[] {-1,-1,0,1,1,1,0,-1 };
            int[] dc = new int[] {0,1,1,1,0,-1,-1,-1 };
            for (int i = 0; i < dr.Length; ++i)
            {
                // coord starts from 1, so minus 1
                int p1Row = curPieceRow + dr[i];
                int p2Row = curPieceRow + 2 * dr[i];
                int p1Col = curPieceCol + dc[i];
                int p2Col = curPieceCol + 2 * dc[i];
                // check whether p2 is on the board
                if (Board.IsValidCoord(p2Row,p2Col)) // if p2 is on the board
                {
                    Piece p1 = Board.Cells[p1Row][p1Col];
                    Piece p2 = Board.Cells[p2Row][p2Col];
                    if (p1 != null && p2 != null)
                    {
                        if (p1.Sign == "O" && p2.Sign == "S")
                        {
                            ++score;
                        }
                    }
                }
            }
            return score;
        }

        public int CheckScore4O()
        {
            int score = 0;
            int[] dr = new int[] {-1,-1,0,1 };
            int[] dc = new int[] {0, 1, 1, 1 };
            for (int i = 0; i < dr.Length; ++i)
            {
                int p1Row = curPieceRow + dr[i];
                int p2Row = curPieceRow - dr[i];
                int p1Col = curPieceCol + dc[i];
                int p2Col = curPieceCol - dc[i];
                // check if p1 and p2 are on the board
                if (Board.IsValidCoord(p1Row,p1Col) && Board.IsValidCoord(p2Row, p2Col))
                {
                    Piece p1 = Board.Cells[p1Row][p1Col];
                    Piece p2 = Board.Cells[p2Row][p2Col];
                    if (p1 != null && p2 != null)
                    {
                        if (p1.Sign == "S" && p2.Sign == "S")
                        {
                            ++score;
                        }
                    }
                }
            }

            return score;
        }

        // check the command validity
        // if the command is not correct, just throw exception
        // if the command is correct, then parse it
        // - if the command has been handled, return false
        // - if the command has not been handled, return true
        protected override bool CheckAndParseCmd(string cmd)
        {
            const string FormatIssue = "wrong command format";
            const string UnsupportedCmd = "unsupported command";
            const string WrongParameter = "wrong Parameter";
            const string ExistingPiece = "you cannot place piece on the place where had piece already";

            bool flag = base.CheckAndParseCmd(cmd);
            if (flag) // if need further check
            {
                string[] fragments = cmd.Split(' ');
                // command format is not correct
                if (fragments.Length != 4) throw new Exception($"{FormatIssue}");
                // the command is not 'place'
                else if (fragments[0] != "place") throw new Exception(UnsupportedCmd);
                // if the symbol is not s or o
                else if (fragments[3] != "S" && fragments[3] != "O") throw new Exception($"{WrongParameter}: the third parameter must be S or O");
                // if the second parameter is not integer
                else if (!Int32.TryParse(fragments[1], out curPieceRow)) throw new Exception($"{WrongParameter}: the first parameter must be an integer");
                // if the third parameter is not integer
                else if (!Int32.TryParse(fragments[2], out curPieceCol)) throw new Exception($"{WrongParameter}: the second parameter must be an integer");
                // input coord starts from 1, but array starts from 0
                curPieceRow -= 1; curPieceCol -= 1;
                // if the coordinate is not valid for the board
                if (!Board.IsValidCoord(curPieceRow, curPieceCol)) throw new Exception($"{WrongParameter}: the row or column is out of board");
                // if the place has piece already
                else if (Board.Cells[curPieceRow][curPieceCol] != null) throw new Exception(ExistingPiece);
                curPieceSign = fragments[3];
            }
            return flag;
        }
        protected override void CheckWinner()
        {
            Console.WriteLine($"{Players[0].Name} connected {scores[0]} SOS");
            Console.WriteLine($"{Players[1].Name} connected {scores[1]} SOS");

            if (scores[0] > scores[1])
                Console.WriteLine($"The winner is {Players[0].Name}");
            else if (scores[0] == scores[1])
                Console.WriteLine("Draw");
            else
                Console.WriteLine($"The winner is {Players[1].Name}");
        }

        protected override bool IsGameEnd()
        {
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
    }
}
