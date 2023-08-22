using Sharprompt;

namespace Game
{
    internal class HumanPlayer : Player
    {
        public HumanPlayer(string name) : base(name) { }

        public override bool TakeMove(Game g)
        {
            // print board
            g.Board.ShowBoard();
            // get user input
            string cmd = Prompt.Input<string>(this.name);
            return g.ExecuteCmd(cmd);
        }
    }
}
