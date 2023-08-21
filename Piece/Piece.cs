using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Piece) return false;
            Piece p = (Piece)obj;
            return p.sign == sign;
        }


    }
}
