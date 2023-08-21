namespace Game
{
    internal class ComputerPlayer : Player
    {
        protected Random r = new Random();

        public ComputerPlayer(string name) : base(name) { }

        public override bool TakeMove(Game g)
        {
            List<string> availableMoves = g.CalculateLegalMoves();
            int randomIndex = r.Next(0,availableMoves.Count);
            return g.CheckAndExeCmd(availableMoves[randomIndex]);
        }
    }
}
