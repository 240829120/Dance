using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线轨道
    /// </summary>
    public interface IDanceTimelineTrack
    {
        /// <summary>
        /// 子项集合
        /// </summary>
        IEnumerable Items { get; }
    }
}
