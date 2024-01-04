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
        /// 是否可以重做
        /// </summary>
        /// <returns>是否可以重做</returns>
        public bool CanRedo()
        {
            return false;
        }

        /// <summary>
        /// 是否可以撤销
        /// </summary>
        /// <returns>是否可以撤销</returns>
        public bool CanUndo()
        {
            return false;
        }

        /// <summary>
        /// 重做
        /// </summary>
        public void Redo()
        {

        }

        /// <summary>
        /// 撤销
        /// </summary>
        public void Undo()
        {

        }
    }
}
