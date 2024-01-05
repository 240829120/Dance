using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 历史记录管理器
    /// </summary>
    /// <param name="maximumUndo">最大撤销次数</param>
    public class DanceHistoryManager(int maximumUndo = 50) : IDanceHistoryManager
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object lock_object = new();

        /// <summary>
        /// 是否正在执行
        /// </summary>
        public bool IsExecuting { get; set; }

        /// <summary>
        /// 最大撤销次数
        /// </summary>
        public int MaximumUndo { get; set; } = maximumUndo;

        /// <summary>
        /// 重做栈
        /// </summary>
        public readonly List<IDanceHistoryStep> RedoStack = new();

        /// <summary>
        /// 撤销栈
        /// </summary>
        public readonly List<IDanceHistoryStep> UndoStack = new();

        /// <summary>
        /// 是否可以重做
        /// </summary>
        /// <returns>是否可以重做</returns>
        public bool CanRedo()
        {
            return this.RedoStack.Count > 0;
        }

        /// <summary>
        /// 是否可以撤销
        /// </summary>
        /// <returns>是否可以撤销</returns>
        public bool CanUndo()
        {
            return this.UndoStack.Count > 0;
        }

        /// <summary>
        /// 重做
        /// </summary>
        public void Redo()
        {
            if (this.IsExecuting)
                return;

            lock (this.lock_object)
            {
                if (this.RedoStack.Count <= 0)
                    return;

                IDanceHistoryStep step = this.RedoStack[^1];
                this.RedoStack.RemoveAt(this.RedoStack.Count - 1);

                this.IsExecuting = true;
                step.Redo(this);
                this.IsExecuting = false;

                this.UndoStack.Add(step);
            }
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public void Undo()
        {
            if (this.IsExecuting)
                return;

            lock (this.lock_object)
            {
                if (this.UndoStack.Count <= 0)
                    return;

                IDanceHistoryStep step = this.UndoStack[^1];
                this.UndoStack.RemoveAt(this.UndoStack.Count - 1);

                this.IsExecuting = true;
                step.Undo(this);
                this.IsExecuting = false;

                this.RedoStack.Add(step);
            }
        }

        /// <summary>
        /// 添加步骤
        /// </summary>
        /// <param name="step">步骤</param>
        public void Append(IDanceHistoryStep step)
        {
            if (this.IsExecuting)
                return;

            lock (this.lock_object)
            {
                this.UndoStack.Add(step);
                if (this.UndoStack.Count > this.MaximumUndo)
                {
                    this.UndoStack.RemoveAt(0);
                }

                this.RedoStack.Clear();
            }
        }
    }
}
