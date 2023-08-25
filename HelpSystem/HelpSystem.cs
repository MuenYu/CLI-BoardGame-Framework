

namespace Game
{
    class HelpSystem
    {
        protected string name;
        protected string description;
        protected List<string> helps;

        public HelpSystem(string name, string description, List<string> helps)
        {
            this.name = name;
            this.description = description;
            this.helps = new List<string>()
            {
                "undo: go back to your last turn, this command has no parameter",
                "redo: undo of undo, this command has no parameter",
                "save: save current game state to a file, this command has no parameter",
                "help: print this help message, this command has no parameter",
                "quit: quit the game, this command has no parameter",
            };
            this.helps.AddRange(helps);
        }

        public void ShowGameInfo()
        {
            Console.WriteLine();
            Console.WriteLine(this.name);
            Console.WriteLine(this.description);
            Console.WriteLine("use 'help' command to get more help.");
        }
        public void ShowGameHelp() {
            Console.WriteLine();
            Console.WriteLine(this.description);
            Console.WriteLine("Available commands for the game:");
            Console.WriteLine("Command format: command <parameter_name> ...");
            foreach (var s in helps)
            {
                Console.WriteLine($"\t{s}");
            }
        }
    }
}
