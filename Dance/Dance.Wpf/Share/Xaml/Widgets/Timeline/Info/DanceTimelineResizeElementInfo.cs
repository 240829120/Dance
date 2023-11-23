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
    public class DanceTimelineResizeElementInfo
    {
        /// <summary>
        /// 时间线修改元素宽度信息
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="resize">修改元素</param>
        /// <param name="wantTime">期望时间</param>
        public DanceTimelineResizeElementInfo(DanceTimelineElement element, DanceTimelineElementResize resize, TimeSpan wantTime)
        {
            this.Element = element;
            this.Resize = resize;
            this.WantTime = wantTime;
            this.RealTime = wantTime;
        }

        /// <summary>
        /// 元素
        /// </summary>
        public DanceTimelineElement Element { get; private set; }

        /// <summary>
        /// 修改元素
        /// </summary>
        public DanceTimelineElementResize Resize { get; private set; }

        /// <summary>
        /// 期望时间
        /// </summary>
        public TimeSpan WantTime { get; private set; }

        /// <summary>
        /// 真实时间
        /// </summary>
        public TimeSpan RealTime { get; set; }
    }
}
