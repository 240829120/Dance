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
        /// 缩放
        /// </summary>
        Zoom,

        /// <summary>
        /// 移动
        /// </summary>
        Move,

        /// <summary>
        /// 更新元素
        /// </summary>
        UpdateElement
    }
}
