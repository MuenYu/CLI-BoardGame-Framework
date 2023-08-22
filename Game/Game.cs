
namespace Game
{
    abstract class Game
    {
        protected Board board;
        protected Player[] players;
        protected int curPlayer;
        protected bool isQuit;
        protected HelpSystem helpSystem;
        protected HistoryManager historyManager;
        protected GameSaver gameSaver;

        public Board Board { get { return board; } }
        public Player[] Players { get { return players; } }
        public int CurPlayer { get { return curPlayer; } }

        public Game(Board board, Player[] players, int curPlayer, HelpSystem helpSystem, HistoryManager historyManager, GameSaver gameSaver)
        {
            this.board = board;
            this.players = players;
            this.curPlayer = curPlayer;
            this.helpSystem = helpSystem;
            this.historyManager = historyManager;
            this.gameSaver = gameSaver;
        }

        protected virtual Game Undo()
        {
            Game g = historyManager.Recover();
            board = g.Board;
            return g;
        }

        public void Play()
        {
            helpSystem.ShowGameInfo();
            historyManager.SnapShot(this);
            while (!isQuit)
            {
                try
                {
                    if (IsGameEnd())
                    {
                        this.Board.ShowBoard();
                        CheckWinner();
                        break;
                    }
                    if (!Players[CurPlayer].TakeMove(this)) continue;
                    historyManager.SnapShot(this);
                    curPlayer = (CurPlayer + 1) % Players.Length;
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // TODO remove it after test
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        // only provide basic command check (game logic irrelevant cmd)
        // if the basic command is correct, then run it
        // need concrete game class to implement the check function.
        // return true means need further check
        protected virtual bool CheckAndParseCmd(string cmd)
        {
            switch (cmd)
            {
                case "help":
                    helpSystem.ShowGameHelp();
                    return false;
                case "quit":
                    isQuit = true;
                    return false;
                case "undo":
                    Undo();
                    return false;
                case "save":
                    gameSaver.Save(this);
                    return false;
            }
            return true;
        }

        // if the command is not correct, just throw exception
        // if the round continue, return true
        // if not, return false
        public abstract bool ExecuteCmd(string cmd);

        public abstract List<string> CalculateLegalMoves();

        protected abstract bool IsGameEnd();

        protected abstract void CheckWinner();
    }
}
