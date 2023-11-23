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
    /// 时间线移动工具
    /// </summary>
    public class DanceTimelineMoveTool : DanceTimelineToolBase
    {
        /// <summary>
        /// 时间线工具
        /// </summary>
        /// <param name="timeline">时间线</param>
        public DanceTimelineMoveTool(DanceTimeline timeline) : base(timeline)
        {
            timeline.KeyDown -= KeyDown;
            timeline.KeyDown += KeyDown;

            timeline.MouseLeftButtonDown += MouseLeftButtonDown;
            timeline.MouseLeftButtonDown += MouseLeftButtonDown;

            timeline.MouseLeftButtonUp -= MouseLeftButtonUp;
            timeline.MouseLeftButtonUp += MouseLeftButtonUp;

            timeline.MouseMove -= MouseMove;
            timeline.MouseMove += MouseMove;
        }

        /// <summary>
        /// 开始坐标
        /// </summary>
        private Point? BeginPoint;

        /// <summary>
        /// 开始X轴滚动值
        /// </summary>
        private double? BeginScrollX;

        /// <summary>
        /// 开始Y轴滚动值
        /// </summary>
        private double? BeginScrollY;

        /// <summary>
        /// 键盘按下
        /// </summary>
        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Space))
            {
                this.Timeline.ToolStatus = DanceTimelineToolStatus.Move;
                this.Timeline.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Timeline.ToolStatus != DanceTimelineToolStatus.Move)
                return;

            if (this.Timeline.IsPlaying && this.Timeline.IsFollowProgress)
                return;

            if (this.Timeline.PART_HorizontalScrollBar == null || this.Timeline.PART_VerticalScrollBar == null)
                return;

            e.Handled = true;

            this.BeginPoint = e.GetPosition(this.Timeline);
            this.BeginScrollX = this.Timeline.PART_HorizontalScrollBar.Value;
            this.BeginScrollY = this.Timeline.PART_VerticalScrollBar.Value;

            this.Timeline.CaptureMouse();
        }

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.Timeline.ToolStatus != DanceTimelineToolStatus.Move)
                return;

            if (this.Timeline.IsPlaying && this.Timeline.IsFollowProgress)
                return;

            e.Handled = true;

            this.BeginPoint = null;
            this.BeginScrollX = null;
            this.BeginScrollY = null;

            this.Timeline.Cursor = Cursors.Arrow;
            this.Timeline.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Timeline.ToolStatus != DanceTimelineToolStatus.Move)
                return;

            if (this.Timeline.IsPlaying && this.Timeline.IsFollowProgress)
                return;

            if (this.Timeline.PART_HorizontalScrollBar == null || this.Timeline.PART_VerticalScrollBar == null)
                return;

            if (this.BeginPoint == null || this.BeginScrollX == null || this.BeginScrollY == null)
                return;

            e.Handled = true;

            Point endPoint = e.GetPosition(this.Timeline);

            double destY = this.BeginScrollY.Value - (endPoint.Y - this.BeginPoint.Value.Y);
            destY = Math.Clamp(destY, 0, this.Timeline.PART_VerticalScrollBar.Maximum);

            TimeSpan destEndTime = this.Timeline.GetTimeSpanFromPixel(this.BeginScrollX.Value - (endPoint.X - this.BeginPoint.Value.X));
            destEndTime = this.Timeline.GetEffectiveTimeSpan(destEndTime);

            this.Timeline.PART_VerticalScrollBar.Value = destY;
            this.Timeline.PART_HorizontalScrollBar.Value = this.Timeline.GetPixelFromTimeSpan(destEndTime);
        }
    }
}