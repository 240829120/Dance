using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线面板
    /// </summary>
    public class DanceTimelinePanel : Panel
    {
        public DanceTimelinePanel()
        {
            this.Loaded += DanceTimelinePanel_Loaded;
        }

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? Timeline;

        // =============================================================================================
        // Override

        /// <summary>
        /// 测量
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            this.TryGetOwner();
            if (this.Timeline == null)
                return new Size(this.ActualWidth, this.ActualHeight);

            double width = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Duration.TotalHours * this.Timeline.Zoom;
            double height = this.Timeline.TrackHeight;

            foreach (UIElement item in this.Children)
            {
                item.Measure(new Size(width, height));
            }

            return new Size(width, Math.Max(height * this.Children.Count, this.ActualHeight));
        }

        /// <summary>
        /// 布局
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            this.TryGetOwner();
            if (this.Timeline == null)
                return new Size(this.ActualWidth, this.ActualHeight);

            double y = 0;
            double width = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Duration.TotalHours * this.Timeline.Zoom;
            double height = this.Timeline.TrackHeight;

            foreach (UIElement item in this.Children)
            {
                item.Arrange(new Rect(0, y, width, height));
                y += height;
            }

            return new Size(width, Math.Max(height * this.Children.Count, finalSize.Height));
        }

        /// <summary>
        /// 尝试获取所属
        /// </summary>
        private void TryGetOwner()
        {
            this.Timeline ??= DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);
        }

        /// <summary>
        /// 控件加载
        /// </summary>
        private void DanceTimelinePanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.TryGetOwner();
            if (this.Timeline == null)
                return;

            this.Timeline.TimeLinePanel = this;
        }

    }
}
