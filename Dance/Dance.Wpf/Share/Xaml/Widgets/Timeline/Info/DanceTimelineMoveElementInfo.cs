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
        /// <param name="srcTrack">源轨道</param>
        /// <param name="destTrack">目标轨道</param>
        /// <param name="element">移动元素</param>
        /// <param name="wantBeginTime">想要移动到的开始时间</param>
        /// <param name="wantEndTime">想要移动到的结束时间</param>
        public DanceTimelineMoveElementInfo(DanceTimelineTrack srcTrack, DanceTimelineTrack destTrack, DanceTimelineElement element, TimeSpan wantBeginTime, TimeSpan wantEndTime)
        {
            this.SrcTrack = srcTrack;
            this.DestTrack = destTrack;
            this.Element = element;
            this.WantBeginTime = wantBeginTime;
            this.WantEndTime = wantEndTime;
            this.RealBeginTime = wantBeginTime;
            this.RealEndTime = wantEndTime;
        }

        /// <summary>
        /// 源轨道
        /// </summary>
        public DanceTimelineTrack SrcTrack { get; }

        /// <summary>
        /// 目标轨道
        /// </summary>
        public DanceTimelineTrack DestTrack { get; }

        /// <summary>
        /// 移动元素
        /// </summary>
        public DanceTimelineElement Element { get; }

        /// <summary>
        /// 想要移动到的开始时间
        /// </summary>
        public TimeSpan WantBeginTime { get; private set; }

        /// <summary>
        /// 想要移动到的结束时间
        /// </summary>
        public TimeSpan WantEndTime { get; private set; }

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
