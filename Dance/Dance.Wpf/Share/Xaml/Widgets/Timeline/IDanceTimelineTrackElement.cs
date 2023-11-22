using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线元素
    /// </summary>
    public interface IDanceTimelineTrackElement : INotifyPropertyChanged, IDanceJsonObject
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        TimeSpan BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        TimeSpan EndTime { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        bool IsSelected { get; set; }
    }
}
