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
    public class DanceTimelineMoveElementInfo
    {
        /// <summary>
        /// 时间线移动元素信息
        /// </summary>
        /// <param name="destTrack">目标轨道</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="wantBeginTime">想要移动到的开始时间</param>
        /// <param name="wantEndTime">想要移动到的结束时间</param>
        public DanceTimelineMoveElementInfo(DanceTimelineTrack destTrack, TimeSpan beginTime, TimeSpan endTime, TimeSpan wantBeginTime, TimeSpan wantEndTime)
        {
            this.DestTrack = destTrack;
            this.BeginTime = beginTime;
            this.EndTime = endTime;
            this.WantBeginTime = wantBeginTime;
            this.WantEndTime = wantEndTime;
            this.RealBeginTime = wantBeginTime;
            this.RealEndTime = wantEndTime;
        }

        /// <summary>
        /// 目标轨道
        /// </summary>
        public DanceTimelineTrack DestTrack { get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan BeginTime { get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan EndTime { get; }

        /// <summary>
        /// 想要移动到的开始时间
        /// </summary>
        public TimeSpan WantBeginTime { get; }

        /// <summary>
        /// 想要移动到的结束时间
        /// </summary>
        public TimeSpan WantEndTime { get; }

        /// <summary>
        /// 真是的开始时间
        /// </summary>
        public TimeSpan RealBeginTime { get; set; }

        /// <summary>
        /// 真实的结束时间
        /// </summary>
        public TimeSpan RealEndTime { get; set; }
    }
}
