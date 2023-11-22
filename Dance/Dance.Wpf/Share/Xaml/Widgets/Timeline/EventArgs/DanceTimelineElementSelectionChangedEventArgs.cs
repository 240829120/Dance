using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线元素选择改变消息
    /// </summary>
    public class DanceTimelineElementSelectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 时间线元素选择改变消息
        /// </summary>
        /// <param name="timeline">时间线</param>
        /// <param name="elements">选中的元素</param>
        public DanceTimelineElementSelectionChangedEventArgs(DanceTimeline timeline, List<DanceTimelineElement> elements)
        {
            this.Timeline = timeline;
            this.Elements = elements;
        }

        /// <summary>
        /// 时间线
        /// </summary>
        public DanceTimeline Timeline { get; private set; }

        /// <summary>
        /// 选中的元素
        /// </summary>
        public List<DanceTimelineElement> Elements { get; private set; }
    }
}
