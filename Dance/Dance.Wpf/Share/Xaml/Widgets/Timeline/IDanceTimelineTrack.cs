using System;
using System.Collections;
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
        /// 是否选中
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// 子项集合
        /// </summary>
        IList Items { get; }
    }
}
