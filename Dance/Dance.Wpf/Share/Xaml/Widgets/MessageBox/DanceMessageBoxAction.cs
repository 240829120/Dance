using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 消息框行为
    /// </summary>
    [Flags]
    public enum DanceMessageBoxAction
    {
        /// <summary>
        /// 确定
        /// </summary>
        YES,

        /// <summary>
        /// 否定
        /// </summary>
        NO,

        /// <summary>
        /// 取消
        /// </summary>
        CANCEL
    }
}
