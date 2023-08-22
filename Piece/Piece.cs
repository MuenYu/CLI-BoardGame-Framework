namespace Game
{
    class Piece
    {
        protected string sign;
        public string Sign
        {
            get
            {
                return sign;
            }
        }

        public Piece(string sign)
        {
            this.sign = sign;
        }

        public virtual void ShowPiece()
        {
            Console.Write(sign);
        }

    }
}
