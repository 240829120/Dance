using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线移动元素信息
    /// </summary>
    /// <param name="destTrack">目标轨道</param>
    /// <param name="beginTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <param name="wantBeginTime">想要移动到的开始时间</param>
    /// <param name="wantEndTime">想要移动到的结束时间</param>
    public class DanceTimelineMoveElementInfo(DanceTimelineTrack destTrack, TimeSpan beginTime, TimeSpan endTime, TimeSpan wantBeginTime, TimeSpan wantEndTime)
    {
        /// <summary>
        /// 目标轨道
        /// </summary>
        public DanceTimelineTrack DestTrack { get; } = destTrack;

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan BeginTime { get; } = beginTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan EndTime { get; } = endTime;

        /// <summary>
        /// 想要移动到的开始时间
        /// </summary>
        public TimeSpan WantBeginTime { get; } = wantBeginTime;

        /// <summary>
        /// 想要移动到的结束时间
        /// </summary>
        public TimeSpan WantEndTime { get; } = wantEndTime;

        /// <summary>
        /// 真是的开始时间
        /// </summary>
        public TimeSpan RealBeginTime { get; set; } = wantBeginTime;

        /// <summary>
        /// 真实的结束时间
        /// </summary>
        public TimeSpan RealEndTime { get; set; } = wantEndTime;
    }
}
