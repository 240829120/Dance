using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线进度
    /// </summary>
    public class DanceTimelineProgress : Border
    {
        public DanceTimelineProgress()
        {
            this.Loaded += DanceTimelineProgress_Loaded;
        }

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 画笔
        /// </summary>
        private readonly Pen Pen = new(Brushes.Red, 1);

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? OwnerTimeline;

        /// <summary>
        /// 开始拖拽坐标点
        /// </summary>
        private Point? BeginDragPoint;

        /// <summary>
        /// 开始拖拽时间
        /// </summary>
        private TimeSpan? BeginDragTime;

        // ==========================================================================================================================================
        // Override

        /// <summary>
        /// 绘制
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            dc.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(this.ActualWidth / 2d, 0), new Point(this.ActualWidth / 2d, this.ActualHeight));
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            this.TryBeginDrag(e);
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            if (this.OwnerTimeline == null)
                return;

            this.BeginDragPoint = null;
            this.BeginDragTime = null;
            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (this.OwnerTimeline == null || this.OwnerTimeline.PART_HorizontalScrollBar == null || this.BeginDragPoint == null || this.BeginDragTime == null)
                return;

            Point endPoint = e.GetPosition(this.OwnerTimeline);
            TimeSpan offset = this.OwnerTimeline.GetTimeSpanFromPixel(endPoint.X - this.BeginDragPoint.Value.X);
            TimeSpan dest = this.BeginDragTime.Value + offset;
            dest = this.OwnerTimeline.GetEffectiveTimeSpan(dest);

            this.OwnerTimeline.CurrentTime = dest;
        }

        // ==========================================================================================================================================
        // Private Function

        /// <summary>
        /// 尝试开始拖拽
        /// </summary>
        internal void TryBeginDrag(MouseButtonEventArgs e)
        {
            if (this.OwnerTimeline == null || this.OwnerTimeline.IsPlaying)
                return;

            this.BeginDragPoint = e.GetPosition(this.OwnerTimeline);
            this.BeginDragTime = this.OwnerTimeline.CurrentTime;
            this.CaptureMouse();
            e.Handled = true;
        }

        // ==========================================================================================================================================
        // Private Function

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceTimelineProgress_Loaded(object sender, RoutedEventArgs e)
        {
            this.OwnerTimeline = DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);
        }

    }
}
