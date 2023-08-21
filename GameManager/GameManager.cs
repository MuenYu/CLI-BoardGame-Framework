using Sharprompt;

namespace Game
{
    class GameManager
    {
        protected Dictionary<string, Func<GameFactory>> gameFactoryList;

        // singleton
        public static GameManager Instance = new GameManager();

        protected GameManager()
        {
            gameFactoryList = new Dictionary<string, Func<GameFactory>>()
            {
                {"sos", () => { return new SosFactory(); } },
                {"quit", () => { return null; } }
            };
        }

        public void RunManager()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("How to interact with the system: ");
            Console.WriteLine("1. Pressing arrow key to move the cursor.");
            Console.WriteLine("2. Pressing enter to select the option.");
            Console.WriteLine("3. If no shown option, inputting a command and then press enter to confirm.");


            Console.WriteLine();

            while (true)
            {
                string gameName = SelectGame();
                if (gameName == "quit")
                {
                    Console.WriteLine("Bye bye~");
                    return;
                }
                Game g = SelectNewOrLoad(gameName);
                if (g != null)
                {
                    g.Play();
                }
            }
        }

        // select game menu
        protected string SelectGame()
        {
            List<string> gameList = gameFactoryList.Keys.ToList();
            return Prompt.Select("Select your game or quit", gameList, defaultValue: gameList[0]);
        }

        // new game or load previous game
        protected Game SelectNewOrLoad(string gameName)
        {
            Game g = null;
            GameFactory factory = gameFactoryList[gameName]();
            List<string> options = new List<string>()
            {
                "New game",
                "Load previous game",
                "Back to game selection"
            };

            string choice = Prompt.Select("Do you want", options, defaultValue: options[0]);
            int index = options.IndexOf(choice);
            try
            {
                switch (index)
                {
                    case 0:
                        g = factory.CreateGame();
                        break;
                    case 1:
                        g = factory.LoadGame();
                        break;
                    case 2:
                        break;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // TODO remove it after test
                Console.WriteLine(ex.StackTrace);
            }
            return g;
        }
    }
}
