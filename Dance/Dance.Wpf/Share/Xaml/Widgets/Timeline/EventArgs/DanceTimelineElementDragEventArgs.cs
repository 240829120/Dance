using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 元素拖拽事件参数
    /// </summary>
    /// <param name="timeline">时间线</param>
    /// <param name="track">轨道</param>
    /// <param name="eventArgs">拖拽事件参数</param>
    public class DanceTimelineElementDragEventArgs(DanceTimeline timeline, DanceTimelineTrack track, DragEventArgs eventArgs) : EventArgs
    {
        /// <summary>
        /// 时间线
        /// </summary>
        public DanceTimeline Timeline { get; private set; } = timeline;

        /// <summary>
        /// 轨道
        /// </summary>
        public DanceTimelineTrack Track { get; private set; } = track;

        /// <summary>
        /// 拖拽事件参数
        /// </summary>
        public DragEventArgs EventArgs { get; private set; } = eventArgs;

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan? EndTime { get; set; }
    }
}
