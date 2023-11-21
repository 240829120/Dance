using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线进度
    /// </summary>
    public class DanceTimelineProgress : Control
    {
        static DanceTimelineProgress()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineProgress), new FrameworkPropertyMetadata(typeof(DanceTimelineProgress)));
        }

        // ============================================================================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? Timeline;

        /// <summary>
        /// 开始拖拽点
        /// </summary>
        private Point? BeginDragPoint;

        // ============================================================================================================================================
        // Property

        #region CurrentTime -- 当前时间

        /// <summary>
        /// 当前时间
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return (TimeSpan)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(TimeSpan), typeof(DanceTimelineProgress), new PropertyMetadata(TimeSpan.Zero, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimelineProgress progress)
                    return;

                progress.UpdateMargin();
            })));

        #endregion

        // ============================================================================================================================================
        // Public Function

        /// <summary>
        /// 更新边距
        /// </summary>
        public void UpdateMargin()
        {
            this.TryGetOwner();
            if (this.Timeline == null)
                return;

            this.Margin = new Thickness((int)(this.CurrentTime.TotalHours * DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Zoom - this.ActualWidth / 2d), 0, 0, 0);
        }

        // ============================================================================================================================================
        // Override

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            this.BeginDragPoint = e.GetPosition(this);
            this.CaptureMouse();
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            this.BeginDragPoint = null;
            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (this.Timeline == null || this.BeginDragPoint == null)
                return;

            Point endPoint = e.GetPosition(this);

            TimeSpan offset = TimeSpan.FromHours((endPoint.X - this.BeginDragPoint.Value.X) / (DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Zoom));
            TimeSpan dest = this.Timeline.CurrentTime + TimeSpan.FromSeconds(Math.Round(offset.TotalSeconds, 1));

            dest = dest > this.Timeline.Duration ? this.Timeline.Duration : dest;
            dest = dest < TimeSpan.Zero ? TimeSpan.Zero : dest;

            this.Timeline.CurrentTime = dest;
        }

        // ============================================================================================================================================
        // Private Function

        private void TryGetOwner()
        {
            this.Timeline ??= DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);
        }


    }
}
