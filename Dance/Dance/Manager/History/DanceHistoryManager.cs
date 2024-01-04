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
    public class DanceHistoryManager : IDanceHistoryManager
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object lock_object = new();

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 最大撤销次数
        /// </summary>
        public int MaximumUndo { get; set; } = 50;

        /// <summary>
        /// 重做栈
        /// </summary>
        public readonly Stack<IDanceHistoryStep> RedoStack = new();

        /// <summary>
        /// 撤销栈
        /// </summary>
        public readonly Stack<IDanceHistoryStep> UndoStack = new();

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
            if (!this.IsEnabled)
                return;

            lock (this.lock_object)
            {
                if (!this.RedoStack.TryPop(out IDanceHistoryStep? step))
                    return;

                step.Redo(this);

                this.UndoStack.Push(step);
            }
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public void Undo()
        {
            if (!this.IsEnabled)
                return;

            lock (this.lock_object)
            {
                if (!this.UndoStack.TryPop(out IDanceHistoryStep? step))
                    return;

                step.Undo(this);

                this.RedoStack.Push(step);
            }
        }

        /// <summary>
        /// 添加步骤
        /// </summary>
        /// <param name="step">步骤</param>
        public void Append(IDanceHistoryStep step)
        {
            if (!this.IsEnabled)
                return;

            lock (this.lock_object)
            {
                this.UndoStack.Push(step);
                this.RedoStack.Clear();
            }
        }
    }
}
