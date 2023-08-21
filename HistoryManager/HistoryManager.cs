using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class HistoryManager
    {
        protected Stack<string> histories;

        public HistoryManager()
        {
            this.histories = new Stack<string>();
        }

        public void SnapShot(Game g)
        {
            string json = SerUtil.ToJson(g);
            histories.Push(json);
        }

        public Game Recover()
        {
            if (histories.Count < 3) throw new Exception("you have no available history to undo");
            histories.Pop();
            histories.Pop(); 
            return SerUtil.ToObj<Game>(histories.Peek());
        }

    }
}
