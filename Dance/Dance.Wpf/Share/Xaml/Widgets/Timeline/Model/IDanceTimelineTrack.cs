using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线轨道
    /// </summary>
    public interface IDanceTimelineTrack<TCollection, TElement> : INotifyPropertyChanged where TElement : IDanceTimelineTrackElement where TCollection : IList<TElement>
    {
        /// <summary>
        /// 是否被选中
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// 元素
        /// </summary>
        TCollection Items { get; }
    }
}
