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
    /// 时间线轨道面板
    /// </summary>
    public class DanceTimelineTrackPanel : Panel
    {
        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? Timeline;

        /// <summary>
        /// 所属轨道
        /// </summary>
        internal DanceTimelineTrack? TimelineTrack;

        /// <summary>
        /// 测量
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            this.TryGetOwner();
            if (this.Timeline == null)
                return availableSize;

            double width = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Duration.TotalHours * this.Timeline.Zoom;
            double height = this.Timeline.TrackHeight;

            foreach (DanceTimelineTrackItem item in this.Children)
            {
                if (item == null)
                    continue;

                double itemWidth = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * ((item.EndTime ?? TimeSpan.Zero) - (item.BeginTime ?? TimeSpan.Zero)).TotalHours * this.Timeline.Zoom;

                item.Measure(new Size(itemWidth, height));
            }

            return new Size(width, height);
        }

        /// <summary>
        /// 布局
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            this.TryGetOwner();
            if (this.Timeline == null)
                return finalSize;

            double width = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Duration.TotalHours * this.Timeline.Zoom;
            double height = this.Timeline.TrackHeight;

            foreach (DanceTimelineTrackItem item in this.Children)
            {

                if (item == null)
                    continue;

                double x = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * (item.BeginTime ?? TimeSpan.Zero).TotalHours * this.Timeline.Zoom;
                double itemWidth = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * ((item.EndTime ?? TimeSpan.Zero) - (item.BeginTime ?? TimeSpan.Zero)).TotalHours * this.Timeline.Zoom;

                item.Arrange(new Rect(x, 0, itemWidth, height));
            }

            return new Size(width, height);
        }

        /// <summary>
        /// 尝试获取所属
        /// </summary>
        private void TryGetOwner()
        {
            this.Timeline ??= DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);
            this.TimelineTrack ??= DanceXamlExpansion.GetVisualTreeParent<DanceTimelineTrack>(this);
        }
    }
}
