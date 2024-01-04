using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.UnitTest.UndoRedo
{
    public class UndoRedoTestObject
    {
        public int X { get; set; }
    }

    [TestClass]
    public class UndoRedoTest
    {
        [TestMethod]
        public void ActionStepTest()
        {
            IDanceHistoryManager historyManager = new DanceHistoryManager();

            int x = 0;

            historyManager.IsEnabled = true;

            ++x;
            historyManager.Append(new DanceActionHistoryStep(p => x++, p => x--, "值改变+1"));
            x += 2;
            historyManager.Append(new DanceActionHistoryStep(p => x += 2, p => x -= 2, "值改变+2"));

            Assert.IsTrue(x == 3);

            historyManager.Undo();
            Assert.IsTrue(x == 1);

            historyManager.Redo();
            Assert.IsTrue(x == 3);

            historyManager.Undo();
            Assert.IsTrue(x == 1);

            x += 3;
            historyManager.Append(new DanceActionHistoryStep(p => x += 3, p => x -= 3, "值改变+3"));
            Assert.IsTrue(x == 4);

            historyManager.Redo();
            Assert.IsTrue(x == 4);
        }

        [TestMethod]
        public void PropertyChangedStepTest()
        {
            UndoRedoTestObject obj = new();

            IDanceHistoryManager historyManager = new DanceHistoryManager();
            historyManager.IsEnabled = true;

            obj.X = 1;
            historyManager.Append(new DancePropertyChangedHistoryStep(obj, "X", 0, 1, "=1"));
            obj.X = 2;
            historyManager.Append(new DancePropertyChangedHistoryStep(obj, "X", 1, 2, "=2"));
            obj.X = 3;
            historyManager.Append(new DancePropertyChangedHistoryStep(obj, "X", 2, 3, "=3"));

            Assert.IsTrue(obj.X == 3);
            historyManager.Undo();
            Assert.IsTrue(obj.X == 2);
            historyManager.Undo();
            Assert.IsTrue(obj.X == 1);

            historyManager.Redo();
            Assert.IsTrue(obj.X == 2);
            historyManager.Redo();
            Assert.IsTrue(obj.X == 3);

            historyManager.Undo();
            Assert.IsTrue(obj.X == 2);
        }
    }
}
