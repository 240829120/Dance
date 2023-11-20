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
    /// 时间线轨道
    /// </summary>
    public class DanceTimelineTrack : ItemsControl
    {
        static DanceTimelineTrack()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrack), new FrameworkPropertyMetadata(typeof(DanceTimelineTrack)));
        }

        // =============================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? Timeline;

        // =============================================================================================
        // Override

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceTimelineTrackItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTimelineTrackItem() { Timeline = this.Timeline, TimelineTrack = this };
        }
    }
}
