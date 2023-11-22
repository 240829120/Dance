using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线控件 -- 操作部分
    /// </summary>
    public partial class DanceTimeline
    {
        /// <summary>
        /// 鼠标左键按下坐标
        /// </summary>
        private Point? MouseLeftButtonDownPoint;

        /// <summary>
        /// 鼠标左键按下时的滚动Y值
        /// </summary>
        private double? MouseLeftButtonDownScrollY;

        /// <summary>
        /// 鼠标左键按下时的滚动X值
        /// </summary>
        private double? MouseLeftButtonDownScrollX;

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.Focus();

            if (this.IsPlaying && this.IsFollowProgress || this.PART_HorizontalScrollBar == null || this.PART_VerticalScrollBar == null)
                return;

            this.MouseLeftButtonDownPoint = e.GetPosition(this);
            this.MouseLeftButtonDownScrollX = this.PART_HorizontalScrollBar.Value;
            this.MouseLeftButtonDownScrollY = this.PART_VerticalScrollBar.Value;
            this.CaptureMouse();
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (this.IsPlaying && this.IsFollowProgress)
                return;

            this.MouseLeftButtonDownPoint = null;
            this.MouseLeftButtonDownScrollX = null;
            this.MouseLeftButtonDownScrollY = null;
            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.IsPlaying && this.IsFollowProgress || this.MouseLeftButtonDownPoint == null || this.MouseLeftButtonDownScrollX == null || this.MouseLeftButtonDownScrollY == null)
                return;

            Point endPoint = e.GetPosition(this);

            if (Keyboard.IsKeyDown(Key.Space))
            {
                this.ExecuteMove(this.MouseLeftButtonDownPoint.Value, endPoint);
            }
        }

        /// <summary>
        /// 滚轮滑动
        /// </summary>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
                return;

            double dest = this.Zoom + (e.Delta > 0 ? 1 : -1);
            if (dest < MIN_ZOOM || dest > MAX_ZOOM)
                return;

            this.Zoom = dest;
        }

        /// <summary>
        /// 执行移动
        /// </summary>
        /// <param name="begin">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteMove(Point begin, Point end)
        {
            if (this.IsPlaying && this.IsFollowProgress || this.MouseLeftButtonDownPoint == null || this.MouseLeftButtonDownScrollX == null || this.MouseLeftButtonDownScrollY == null)
                return;

            if (this.PART_VerticalScrollBar == null || this.PART_HorizontalScrollBar == null)
                return;

            double destY = this.MouseLeftButtonDownScrollY.Value - (end.Y - begin.Y);
            destY = Math.Clamp(destY, 0, this.PART_VerticalScrollBar.Maximum);

            TimeSpan destEndTime = this.GetTimeSpanFromPixel(this.MouseLeftButtonDownScrollX.Value - (end.X - begin.X));
            destEndTime = this.GetEffectiveTimeSpan(destEndTime);

            this.PART_VerticalScrollBar.Value = destY;
            this.PART_HorizontalScrollBar.Value = this.GetPixelFromTimeSpan(destEndTime);
        }
    }
}
