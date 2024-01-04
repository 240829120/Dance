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
    /// <param name="timeline">时间线</param>
    /// <param name="track">轨道</param>
    public class DanceTimelineTrackSelectionChangedEventArgs(DanceTimeline timeline, DanceTimelineTrack? track) : EventArgs
    {
        /// <summary>
        /// 时间线
        /// </summary>
        public DanceTimeline Timeline { get; private set; } = timeline;

        /// <summary>
        /// 选中的轨道
        /// </summary>
        public DanceTimelineTrack? Track { get; private set; } = track;
    }
}
