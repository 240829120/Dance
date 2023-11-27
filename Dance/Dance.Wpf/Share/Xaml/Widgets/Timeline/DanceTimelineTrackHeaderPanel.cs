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
    /// 轨道头面板
    /// </summary>
    public class DanceTimelineTrackHeaderPanel : Panel
    {
        public DanceTimelineTrackHeaderPanel()
        {
            this.Loaded += DanceTimelineTrackHeaderPanel_Loaded;
        }

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? OwnerTimeline;

        // ==========================================================================================================================================
        // Override

        /// <summary>
        /// 测量
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.OwnerTimeline == null)
            {
                double destWidth = availableSize.Width == double.PositiveInfinity ? 0 : availableSize.Width;
                double destHeight = availableSize.Height == double.PositiveInfinity ? 0 : availableSize.Height;

                return new Size(destWidth, destHeight);
            }

            foreach (UIElement item in this.Children)
            {
                item.Measure(new Size(availableSize.Width, this.OwnerTimeline.TrackHeight));
            }

            return availableSize;
        }

        /// <summary>
        /// 布局
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.OwnerTimeline == null || this.OwnerTimeline.PART_VerticalScrollBar == null)
            {
                double destWidth = finalSize.Width == double.PositiveInfinity ? 0 : finalSize.Width;
                double destHeight = finalSize.Height == double.PositiveInfinity ? 0 : finalSize.Height;

                return new Size(destWidth, destHeight);
            }

            double beginY = this.OwnerTimeline.PART_VerticalScrollBar.Value;
            double trackHeight = this.OwnerTimeline.TrackHeight;
            int index = 0;

            foreach (UIElement item in this.Children)
            {
                if (item is not FrameworkElement element)
                    continue;

                double shouldBeginY = index++ * trackHeight;
                double shouldEndY = shouldBeginY + trackHeight;

                if (shouldEndY < beginY || shouldBeginY >= (beginY + this.ActualHeight))
                {
                    element.Visibility = Visibility.Collapsed;
                    continue;
                }

                element.Visibility = Visibility.Visible;

                element.Arrange(new Rect(0, shouldBeginY - beginY, this.ActualWidth, trackHeight));
            }

            return finalSize;
        }

        // ==========================================================================================================================================
        // Private Function

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceTimelineTrackHeaderPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.OwnerTimeline = DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);
            this.InvalidateVisual();
        }
    }
}
