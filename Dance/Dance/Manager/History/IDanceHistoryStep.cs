using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 历史记录步骤
    /// </summary>
    public interface IDanceHistoryStep
    {
        /// <summary>
        /// 描述
        /// </summary>
        string Detail { get; }

        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="manager">历史管理器</param>
        void Redo(IDanceHistoryManager manager);

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="manager">历史管理器</param>
        void Undo(IDanceHistoryManager manager);
    }
}
