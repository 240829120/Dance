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
        public DanceTimelineTrackPanel()
        {
            this.Loaded += DanceTimelineTrackPanel_Loaded;
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
            if (this.OwnerTimeline == null || this.OwnerTimeline.PART_HorizontalScrollBar == null || !this.IsVisible)
                return availableSize;

            foreach (UIElement item in this.Children)
            {
                if (item is not DanceTimelineElement element)
                    continue;

                double beginX = this.OwnerTimeline.GetPixelFromTimeSpan(element.BeginTime) - this.OwnerTimeline.PART_HorizontalScrollBar.Value;
                double endX = this.OwnerTimeline.GetPixelFromTimeSpan(element.EndTime) - this.OwnerTimeline.PART_HorizontalScrollBar.Value;

                if (endX <= 0 || beginX >= this.ActualWidth)
                {
                    continue;
                }

                element.Measure(new Size(endX - beginX, this.ActualHeight));
            }

            return availableSize;
        }

        /// <summary>
        /// 布局
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.OwnerTimeline == null || this.OwnerTimeline.PART_HorizontalScrollBar == null || !this.IsVisible)
                return finalSize;

            foreach (UIElement item in this.Children)
            {
                if (item is not DanceTimelineElement element || element.BeginTime > element.EndTime)
                    continue;

                double beginX = this.OwnerTimeline.GetPixelFromTimeSpan(element.BeginTime) - this.OwnerTimeline.PART_HorizontalScrollBar.Value;
                double endX = this.OwnerTimeline.GetPixelFromTimeSpan(element.EndTime) - this.OwnerTimeline.PART_HorizontalScrollBar.Value;

                if (endX <= 0 || beginX >= this.ActualWidth)
                {
                    element.Visibility = Visibility.Collapsed;
                    continue;
                }

                element.Visibility = Visibility.Visible;
                element.Width = endX - beginX;
                element.Arrange(new Rect(beginX, 0, endX - beginX, this.ActualHeight));
            }

            return finalSize;
        }

        // ==========================================================================================================================================
        // Private Function

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceTimelineTrackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.OwnerTimeline = DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);
            this.InvalidateVisual();
        }
    }
}
