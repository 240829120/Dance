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
    public class DanceTimelineTrackHeaderPanel : Panel
    {
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
            if (this.Timeline == null || this.Timeline.PART_TrackScrollViewer == null)
                return new Size(this.ActualWidth, this.ActualHeight);

            double width = this.Timeline.PART_TrackScrollViewer.ActualWidth;
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
            double width = finalSize.Width;
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
    }
}
