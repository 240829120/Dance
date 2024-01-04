using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线修改元素宽度信息
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="resize">修改元素</param>
    /// <param name="wantTime">期望时间</param>
    public class DanceTimelineResizeElementInfo(DanceTimelineElement element, DanceTimelineElementResize resize, TimeSpan wantTime)
    {
        /// <summary>
        /// 元素
        /// </summary>
        public DanceTimelineElement Element { get; private set; } = element;

        /// <summary>
        /// 修改元素
        /// </summary>
        public DanceTimelineElementResize Resize { get; private set; } = resize;

        /// <summary>
        /// 期望时间
        /// </summary>
        public TimeSpan WantTime { get; private set; } = wantTime;

        /// <summary>
        /// 真实时间
        /// </summary>
        public TimeSpan RealTime { get; set; } = wantTime;
    }
}
