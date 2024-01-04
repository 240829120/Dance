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
    /// <param name="timeline">时间线</param>
    /// <param name="elements">选中的元素</param>
    public class DanceTimelineElementSelectionChangedEventArgs(DanceTimeline timeline, List<DanceTimelineElement> elements) : EventArgs
    {
        /// <summary>
        /// 时间线
        /// </summary>
        public DanceTimeline Timeline { get; private set; } = timeline;

        /// <summary>
        /// 选中的元素
        /// </summary>
        public List<DanceTimelineElement> Elements { get; private set; } = elements;
    }
}
