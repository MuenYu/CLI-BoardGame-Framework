using Newtonsoft.Json;


namespace Game
{
    class RectBoard : Board
    {
        [JsonConstructor]
        public RectBoard(Piece[][] cells) :base(cells){ }

        public RectBoard(int row, int col)
        {
            row = row > 0 ? row : 1;
            col = col > 0 ? col : 1;
            this.cells = new Piece[row][];
            for (int i = 0; i < this.cells.Length; ++i)
            {
                this.cells[i] = new Piece[col];
            }
        }

        public override void ShowBoard()
        {
            int rows = this.cells.Length;
            int cols = this.cells[0].Length;

            Console.WriteLine(new string('-', 4 * cols + 1));
            for (int i = 0; i < rows; ++i)
            {
                Console.Write("|");
                for (int j = 0; j < cols; ++j)
                {
                    if (this.cells[i][j] == null)
                    {
                        Console.Write("   |");
                    }
                    else
                    {
                        Console.Write(" ");
                        this.cells[i][j].ShowPiece();
                        Console.Write(" |");
                    }
                }
                Console.WriteLine();
                Console.WriteLine(new string('-', 4 * cols + 1));
            }
        }
    }
}
