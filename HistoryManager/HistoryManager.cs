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
        protected Stack<string> undoStack;

        public HistoryManager()
        {
            this.histories = new Stack<string>();
            this.undoStack = new Stack<string>();

        }

        public void SnapShot(Game g)
        {
            string json = SerUtil.ToJson(g);
            histories.Push(json);
        }

        public Game Undo()
        {
            if (histories.Count < 3) throw new Exception("you have no available history to undo");
            undoStack.Push(histories.Pop());
            undoStack.Push(histories.Pop());
            return SerUtil.ToObj<Game>(histories.Peek());
        }

        public Game Redo()
        {
            // exception
            if (undoStack.Count < 2) throw new Exception("you have no available history to redo");
            histories.Push(undoStack.Pop());
            histories.Push(undoStack.Pop());
            return SerUtil.ToObj<Game>(histories.Peek());
        }

        public void EmptyUndoStack()
        {
            this.undoStack.Clear();
        }

    }
}
