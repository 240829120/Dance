using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线轨道头部容器
    /// </summary>
    public class DanceTimelineTrackHeaderItems : ItemsControl
    {
        static DanceTimelineTrackHeaderItems()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrackHeaderItems), new FrameworkPropertyMetadata(typeof(DanceTimelineTrackHeaderItems)));
        }

        /// <summary>
        /// 是否是子元素容器
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceTimelineTrackHeader;
        }

        /// <summary>
        /// 获取子元素容器
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTimelineTrackHeader();
        }
    }
}
