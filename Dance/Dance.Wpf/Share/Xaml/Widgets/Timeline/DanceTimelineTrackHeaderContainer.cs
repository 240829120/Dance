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
    /// 时间线头部容器
    /// </summary>
    public class DanceTimelineTrackHeaderContainer : ListBox
    {
        static DanceTimelineTrackHeaderContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrackHeaderContainer), new FrameworkPropertyMetadata(typeof(DanceTimelineTrackHeaderContainer)));
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceTimelineTrackHeader;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTimelineTrackHeader();
        }
    }
}
