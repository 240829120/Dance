using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 元素拖拽开始事件参数
    /// </summary>
    public class DanceTimelineElementDragBeginEventArgs : EventArgs
    {
        /// <summary>
        /// 元素拖拽开始事件参数
        /// </summary>
        /// <param name="timeline">时间线</param>
        /// <param name="track">轨道</param>
        /// <param name="element">元素</param>
        public DanceTimelineElementDragBeginEventArgs(DanceTimeline timeline, DanceTimelineTrack track, DanceTimelineElement element)
        {
            this.Timeline = timeline;
            this.Track = track;
            this.Element = element;
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
        /// 元素
        /// </summary>
        public DanceTimelineElement Element { get; private set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }
    }
}
