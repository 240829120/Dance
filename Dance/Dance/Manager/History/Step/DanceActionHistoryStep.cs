using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 行为历史步骤
    /// </summary>
    /// <param name="redo">重做</param>
    /// <param name="undo">撤销</param>
    /// <param name="detail">描述</param>
    public class DanceActionHistoryStep(Action<IDanceHistoryManager> redo, Action<IDanceHistoryManager> undo, string detail) : DanceObject, IDanceHistoryStep
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Detail { get; } = detail;

        /// <summary>
        /// 重做
        /// </summary>
        public Action<IDanceHistoryManager> RedoAction { get; } = redo;

        /// <summary>
        /// 撤销
        /// </summary>
        public Action<IDanceHistoryManager> UndoAction { get; } = undo;

        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Redo(IDanceHistoryManager manager)
        {
            this.RedoAction(manager);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Undo(IDanceHistoryManager manager)
        {
            this.UndoAction(manager);
        }
    }
}
