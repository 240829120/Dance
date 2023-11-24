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
    public class DanceTimelineElementDragEventArgs : EventArgs
    {
        /// <summary>
        /// 元素拖拽事件参数
        /// </summary>
        /// <param name="timeline">时间线</param>
        /// <param name="track">轨道</param>
        /// <param name="eventArgs">拖拽事件参数</param>
        public DanceTimelineElementDragEventArgs(DanceTimeline timeline, DanceTimelineTrack track, DragEventArgs eventArgs)
        {
            this.Timeline = timeline;
            this.Track = track;
            this.EventArgs = eventArgs;
        }

        /// <summary>
        /// 时间线
        /// </summary>
        public DanceTimeline Timeline { get; private set; }

        /// <summary>
        /// 轨道
        /// </summary>
        public DanceTimelineTrack Track { get; private set; }

        /// <summary>
        /// 拖拽事件参数
        /// </summary>
        public DragEventArgs EventArgs { get; private set; }

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
