using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线状态
    /// </summary>
    public enum DanceTimelineToolStatus
    {
        /// <summary>
        /// 框选
        /// </summary>
        FrameSelect,

        /// <summary>
        /// 移动
        /// </summary>
        Move,

        /// <summary>
        /// 移动元素
        /// </summary>
        MoveElement,

        /// <summary>
        /// 拷贝移动元素
        /// </summary>
        CopyMoveElement
    }
}
