using Sharprompt;

namespace Game
{
    abstract class GameFactory
    {
        public abstract Game CreateGame();

        public virtual Game LoadGame()
        {
            string path = Prompt.Input<string>("Please enter the path of game saving file");
            string json = IOUtil.ReadFromFile(path);
            return SerUtil.ToObj<Game>(json);
        }

        protected abstract HelpSystem InitHelpSystem();
        protected abstract GameSaver InitGameSaver();
        protected abstract HistoryManager InitHistoryManager();
        protected abstract Board InitBoard();
        protected abstract Player[] InitPlayers();
    }
}
