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
    public interface IDanceHistoryManager
    {
        /// <summary>
        /// 是否正在执行
        /// </summary>
        bool IsExecuting { get; set; }

        /// <summary>
        /// 是否可以重做
        /// </summary>
        /// <returns>是否可以重做</returns>
        bool CanRedo();

        /// <summary>
        /// 是否可以撤销
        /// </summary>
        /// <returns>是否可以撤销</returns>
        bool CanUndo();

        /// <summary>
        /// 重做
        /// </summary>
        void Redo();

        /// <summary>
        /// 撤销
        /// </summary>
        void Undo();

        /// <summary>
        /// 添加步骤
        /// </summary>
        /// <param name="step">步骤</param>
        void Append(IDanceHistoryStep step);
    }
}
