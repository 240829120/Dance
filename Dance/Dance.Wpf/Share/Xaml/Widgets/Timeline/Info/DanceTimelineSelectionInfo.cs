using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线选择信息
    /// </summary>
    public class DanceTimelineSelectionInfo
    {
        /// <summary>
        /// 时间线选择信息
        /// </summary>
        /// <param name="element">元素</param>
        public DanceTimelineSelectionInfo(DanceTimelineElement element)
        {
            this.Element = element;
            this.SelectedBeginTime = element.BeginTime;
            this.SelectedEndTime = element.EndTime;
        }

        /// <summary>
        /// 元素
        /// </summary>
        public DanceTimelineElement Element { get; private set; }

        /// <summary>
        /// 选择时的开始时间
        /// </summary>
        public TimeSpan SelectedBeginTime { get; set; }

        /// <summary>
        /// 选择时的结束时间
        /// </summary>
        public TimeSpan SelectedEndTime { get; set; }
    }
}
