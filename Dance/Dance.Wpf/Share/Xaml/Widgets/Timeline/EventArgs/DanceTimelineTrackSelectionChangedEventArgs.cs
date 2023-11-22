using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 轨道选择改变消息
    /// </summary>
    public class DanceTimelineTrackSelectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 时间线元素选择改变消息
        /// </summary>
        /// <param name="timeline">时间线</param>
        /// <param name="track">轨道</param>
        public DanceTimelineTrackSelectionChangedEventArgs(DanceTimeline timeline, DanceTimelineTrack? track)
        {
            this.Timeline = timeline;
            this.Track = track;
        }

        /// <summary>
        /// 时间线
        /// </summary>
        public DanceTimeline Timeline { get; private set; }

        /// <summary>
        /// 选中的轨道
        /// </summary>
        public DanceTimelineTrack? Track { get; private set; }
    }
}
