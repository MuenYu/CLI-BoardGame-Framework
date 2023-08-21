using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Player
    {
        protected string name;

        public string Name { get { return this.name; } }

        public Player(string name)
        {
            this.name = name;
        }

        public abstract bool TakeMove(Game g);
    }
}
