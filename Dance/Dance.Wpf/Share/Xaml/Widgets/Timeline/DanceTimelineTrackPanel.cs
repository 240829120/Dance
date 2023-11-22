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
            if (this.OwnerTimeline == null || !this.IsVisible)
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
            if (this.OwnerTimeline == null || !this.IsVisible)
                return finalSize;

            foreach (UIElement item in this.Children)
            {
                if (item is not DanceTimelineElement element)
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
        /// 获取有效的时间移动
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="element">元素</param>
        /// <param name="width">时间宽度</param>
        /// <returns>是否可以移动</returns>
        internal bool GetEffectiveMoveTimeSpan(ref TimeSpan beginTime, ref TimeSpan endTime, TimeSpan width, DanceTimelineElement element)
        {
            if (this.OwnerTimeline == null)
                return false;

            TimeSpan destWidth = endTime - beginTime;
            if (destWidth <= TimeSpan.Zero || beginTime < TimeSpan.Zero || endTime > this.OwnerTimeline.Duration)
                return false;

            foreach (DanceTimelineElement item in this.Children)
            {
                if (item == element)
                    continue;

                if (beginTime <= item.EndTime && endTime >= item.EndTime)
                {
                    beginTime = item.EndTime;
                    endTime = beginTime + destWidth;

                    break;
                }

                if (endTime >= item.BeginTime && beginTime <= item.BeginTime)
                {
                    endTime = item.BeginTime;
                    beginTime = endTime - destWidth;

                    break;
                }
            }

            if (destWidth != width)
                return false;

            foreach (DanceTimelineElement item in this.Children)
            {
                if (item == element)
                    continue;

                if (!(endTime <= item.BeginTime || beginTime >= item.EndTime))
                {
                    return false;
                }
            }

            return true;
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
