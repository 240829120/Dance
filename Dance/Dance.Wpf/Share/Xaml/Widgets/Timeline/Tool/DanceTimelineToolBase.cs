using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线工具
    /// </summary>
    public abstract class DanceTimelineToolBase
    {
        /// <summary>
        /// 时间线工具
        /// </summary>
        /// <param name="timeline">时间线</param>
        public DanceTimelineToolBase(DanceTimeline timeline)
        {
            this.Timeline = timeline;
        }

        /// <summary>
        /// 时间线
        /// </summary>
        public DanceTimeline Timeline { get; private set; }
    }
}
