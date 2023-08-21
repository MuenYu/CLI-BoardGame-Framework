namespace Game
{
    abstract class Board
    {
        protected Piece[][] cells { get; set; }
        public Piece[][] Cells { get { return cells; } }

        public Board() {}

        public Board(Piece[][] cells)
        {
            this.cells = cells;
        }

        public abstract void ShowBoard();

        public virtual bool IsValidCoord(int row, int col)
        {
            if (cells == null) return false;
            int rowLength = cells.Length;
            if (row >= rowLength || row < 0) return false;
            if (cells[row] == null) return false;
            int colLength = cells[row].Length;
            if (col < 0 || col >= colLength) return false;
            return true;
        }
    }
}
