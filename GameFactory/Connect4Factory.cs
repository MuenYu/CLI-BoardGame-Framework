using Sharprompt;


namespace Game
{
    class Connect4Factory : GameFactory
    {
        public override Game CreateGame()
        {
            Player[] ps = InitPlayers();
            Board b = InitBoard();
            GameSaver gs = InitGameSaver();
            HistoryManager hm = InitHistoryManager();
            HelpSystem hs = InitHelpSystem();            
            Game g = new Connect4Game(b, ps, 0, hs, hm, gs);
            return g;
        }

        public override Game LoadGame()
        {
            Connect4Game g = null;
            GameSaver gs = InitGameSaver();
            HistoryManager hm = InitHistoryManager();
            HelpSystem hs = InitHelpSystem();
            string path = Prompt.Input<string>("Please enter the path of game saving file");
            string json = IOUtil.ReadFromFile(path);
            g = SerUtil.ToObj<Connect4Game>(json);
            return new Connect4Game(g.Board, g.Players, g.CurPlayer, hs, hm, gs);
        }

        protected override Board InitBoard()
        {
            return new RegularBoard(7, 6);
        }

        protected override GameSaver InitGameSaver()
        {
            return new GameSaver();
        }

        protected override HelpSystem InitHelpSystem()
        {
            string name = "Connect4";
            string description = "Two players take turns dropping pieces on a 7x6 board. The player forms an unbroken chain of four pieces horizontally, vertically, or diagonally, wins the game.";
            List<string> helps = new List<string>()
            {
                "drop: drop <column>\n\t\te.g., 'drop 3' means drop a piece on 3rd column of the board",
            };
            return new HelpSystem(name, description, helps);
        }

        protected override HistoryManager InitHistoryManager()
        {
            return new HistoryManager();
        }

        protected override Player[] InitPlayers()
        {
            Player[] players = new Player[2];
            List<string> options = new List<string>()
            {
                "Human vs Human",
                "Human vs Computer (Human First)",
                "Computer vs Human (Computer First)",
            };
            string choice = Prompt.Select("Choose the mode you want to play", options, defaultValue: options[0]);
            int index = options.IndexOf(choice);
            switch (index)
            {
                case 0:
                    players[0] = new HumanPlayer("Player 1");
                    players[1] = new HumanPlayer("Player 2");
                    break;
                case 1:
                    players[0] = new HumanPlayer("Player 1");
                    players[1] = new ComputerPlayer("Player 2");
                    break;
                case 2:
                    players[0] = new ComputerPlayer("Player 1");
                    players[1] = new HumanPlayer("Player 2");
                    break;
            }
            return players;
        }
    }
}
