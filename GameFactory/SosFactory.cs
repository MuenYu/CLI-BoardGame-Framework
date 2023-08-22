using Sharprompt;


namespace Game
{
    class SosFactory : GameFactory
    {
        public override Game CreateGame()
        {
            Player[] ps = InitPlayers();
            Board b = InitBoard();
            GameSaver gs = InitGameSaver();
            HistoryManager hm = InitHistoryManager();
            HelpSystem hs = InitHelpSystem();
            int[] scores = new int[2];
            Game g = new SosGame(b,ps,0,hs,hm,gs,scores);
            return g;
        }

        public override Game LoadGame()
        {
            Game g = base.LoadGame();
            if (g is not SosGame) throw new Exception("the game file is not SOS game");
            SosGame sg = (SosGame)g;
            GameSaver gs = InitGameSaver();
            HistoryManager hm = InitHistoryManager();
            HelpSystem hs = InitHelpSystem();
            return new SosGame(sg.Board,sg.Players,sg.CurPlayer,hs,hm,gs,sg.Scores);
        }

        protected override Board InitBoard()
        {
            const int MinSize = 3;
            const int MaxSize = 20;

            int size = Prompt.Input<int>("What's the size of the board? (input an integer between 3-20)");
            size = size < MinSize ? MinSize : size; // 3 * 3 at least
            size = size > MaxSize ? MaxSize : size; // the maximum size of the board is 20
            return new RectBoard(size,size);
        }

        protected override GameSaver InitGameSaver()
        {
            return new GameSaver();
        }

        protected override HelpSystem InitHelpSystem()
        {
            string name = "SOS";
            string description = "Two players take turns to add either an S or an O (no requirement to use the same\r\nletter each turn) on a board with at least 3x3 squares in size. If a player makes the sequence\r\nSOS vertically, horizontally or diagonally they get a point and also take another turn. Once the\r\ngrid has been filled up, the winner is the player who made the most SOSs.";
            List<string> helps = new List<string>()
            {
                "place: place <row> <column> <piece_sign>\n\t\te.g., 'place 2 3 x' means put a piece with x sign on 2nd row, 3rd column of the board",
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
            switch(index)
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
