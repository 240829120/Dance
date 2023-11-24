using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线轨道
    /// </summary>
    public interface IDanceTimelineTrack : INotifyPropertyChanged
    {
        /// <summary>
        /// 是否被选中
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// 元素
        /// </summary>
        IList<IDanceTimelineTrackElement> Items { get; }
    }
}
