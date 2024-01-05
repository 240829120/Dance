using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.UnitTest.UndoRedo
{
    public class UndoRedoTestObject
    {
        public int X { get; set; }
    }

    public class UndoRedoModel(IDanceHistoryManager historyManager) : DanceHistoryModel(historyManager)
    {
        #region Name -- 名称

        private string? name;

        public string? Name
        {
            get { return name; }
            set
            {
                string? oldValue = name;
                string? newValue = value;

                name = value;

                this.OnHistoryPropertyChanged(oldValue, newValue);
            }
        }

        #endregion

        #region Age -- 年龄

        private int age;

        public int Age
        {
            get { return age; }
            set
            {
                int oldValue = age;
                int newValue = value;

                age = value;

                this.OnHistoryPropertyChanged(oldValue, newValue);
            }
        }

        #endregion
    }

    [TestClass]
    public class UndoRedoTest
    {
        [TestMethod]
        public void ActionStepTest()
        {
            DanceHistoryManager historyManager = new();

            int x = 0;

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

            DanceHistoryManager historyManager = new();

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

        [TestMethod]
        public void CollectionChangedTest()
        {
            DanceHistoryManager historyManager = new();

            DanceHistoryObservableCollection<int> list = new(historyManager) { 1, 2, 3, 4, 5 };

            list.Add(6);

            historyManager.Undo();
            Assert.IsTrue(list.Count == 5);

            historyManager.Redo();
            Assert.IsTrue(list.Count == 6);

            list[0] = 10;
            historyManager.Undo();
            Assert.IsTrue(list[0] == 1);

            historyManager.Redo();
            Assert.IsTrue(list[0] == 10);

            for (int i = 0; i < 30; i++)
            {
                historyManager.Undo();
            }
            Assert.IsTrue(list.Count == 0);

            for (int i = 0; i < 30; i++)
            {
                historyManager.Redo();
            }

            Assert.IsTrue(list.Count == 6);
            Assert.IsTrue(list[0] == 10);
        }

        [TestMethod]
        public void MoreTest()
        {
            DanceHistoryManager historyManager = new();

            DanceHistoryObservableCollection<UndoRedoModel> list = new(historyManager)
            {
                new UndoRedoModel(historyManager) { Name = "zhangsan", Age = 17 },
                new UndoRedoModel(historyManager) { Name = "lisi", Age = 16 },
                new UndoRedoModel(historyManager) { Name = "wangwu", Age = 18 }
            };

            list[0].Age = 20;
            list[1].Name = "lisi2";

            historyManager.Undo();
            Assert.IsTrue(list[1].Name == "lisi");

            historyManager.Undo();
            Assert.IsTrue(list[0].Age == 17);

            historyManager.Redo();
            Assert.IsTrue(list[0].Age == 20);

            historyManager.Redo();
            Assert.IsTrue(list[1].Name == "lisi2");

            for (int i = 0; i < 30; i++)
            {
                historyManager.Undo();
            }
            Assert.IsTrue(list.Count == 0);

            for (int i = 0; i < 30; i++)
            {
                historyManager.Redo();
            }

            Assert.IsTrue(list[1].Name == "lisi2");
        }
    }
}
