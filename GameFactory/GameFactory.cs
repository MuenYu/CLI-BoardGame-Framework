namespace Game
{
    abstract class GameFactory
    {
        public abstract Game CreateGame();

        public abstract Game LoadGame();

        protected abstract HelpSystem InitHelpSystem();
        protected abstract GameSaver InitGameSaver();
        protected abstract HistoryManager InitHistoryManager();
        protected abstract Board InitBoard();
        protected abstract Player[] InitPlayers();
    }
}
