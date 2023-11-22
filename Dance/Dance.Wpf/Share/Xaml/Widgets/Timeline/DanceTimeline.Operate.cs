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
        /// 时间线操作
        /// </summary>
        private enum DanceTimelineOperate
        {
            /// <summary>
            /// 空
            /// </summary>
            None,

            /// <summary>
            /// 移动
            /// </summary>
            Move,

            /// <summary>
            /// 框选
            /// </summary>
            FrameSelect
        }

        // ==============================================================================================================
        // Field

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
        /// 当前操作状态
        /// </summary>
        private DanceTimelineOperate OperateStatus = DanceTimelineOperate.None;

        // ==============================================================================================================
        // Override

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.Focus();

            if (this.IsPlaying && this.IsFollowProgress)
                return;

            if (this.PART_FrameSelect == null || this.PART_HorizontalScrollBar == null || this.PART_VerticalScrollBar == null)
                return;

            this.MouseLeftButtonDownPoint = e.GetPosition(this.PART_FrameSelect);
            this.MouseLeftButtonDownScrollX = this.PART_HorizontalScrollBar.Value;
            this.MouseLeftButtonDownScrollY = this.PART_VerticalScrollBar.Value;
            if (this.OperateStatus != DanceTimelineOperate.Move)
            {
                this.OperateStatus = DanceTimelineOperate.FrameSelect;
            }
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
            this.OperateStatus = DanceTimelineOperate.None;
            this.SelectElementFromFrameSelect();
            this.ClearFrameSelect();
            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.IsPlaying && this.IsFollowProgress)
                return;

            if (this.PART_FrameSelect == null || this.MouseLeftButtonDownPoint == null || this.MouseLeftButtonDownScrollX == null || this.MouseLeftButtonDownScrollY == null)
                return;

            Point endPoint = e.GetPosition(this.PART_FrameSelect);

            if (this.OperateStatus == DanceTimelineOperate.Move)
            {
                this.ExecuteMove(this.MouseLeftButtonDownPoint.Value, endPoint);
            }
            else if (this.OperateStatus == DanceTimelineOperate.FrameSelect)
            {
                this.ExecuteFrameSelect(this.MouseLeftButtonDownPoint.Value, endPoint);
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
        /// 按键按下
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Space)
            {
                this.OperateStatus = DanceTimelineOperate.Move;
                this.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// 按键抬起
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            this.OperateStatus = DanceTimelineOperate.None;
            this.Cursor = Cursors.Arrow;
            this.ClearFrameSelect();
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            this.OperateStatus = DanceTimelineOperate.None;
            this.Cursor = Cursors.Arrow;
            this.ClearFrameSelect();
        }

        // ==============================================================================================================
        // Private Function

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

        /// <summary>
        /// 执行框选
        /// </summary>
        /// <param name="begin">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteFrameSelect(Point begin, Point end)
        {
            if (this.IsPlaying && this.IsFollowProgress || this.MouseLeftButtonDownPoint == null || this.MouseLeftButtonDownScrollX == null || this.MouseLeftButtonDownScrollY == null)
                return;

            if (this.PART_FrameSelect == null)
                return;

            this.PART_FrameSelect.BeginPoint = begin;
            this.PART_FrameSelect.EndPoint = end;
        }

        /// <summary>
        /// 清理框选
        /// </summary>
        private void ClearFrameSelect()
        {
            if (this.PART_FrameSelect == null)
                return;

            this.PART_FrameSelect.BeginPoint = null;
            this.PART_FrameSelect.EndPoint = null;
        }

        /// <summary>
        /// 根据款选选择元素
        /// </summary>
        private void SelectElementFromFrameSelect()
        {
            if (this.IsPlaying && this.IsFollowProgress)
                return;

            if (this.PART_VerticalScrollBar == null || this.PART_HorizontalScrollBar == null || this.PART_TrackItems == null)
                return;

            Rect? selection = this.PART_FrameSelect?.GetSelection();
            if (selection == null)
                return;

            TimeSpan left = this.GetTimeSpanFromPixel(selection.Value.Left + this.PART_HorizontalScrollBar.Value);
            TimeSpan right = this.GetTimeSpanFromPixel(selection.Value.Right + this.PART_HorizontalScrollBar.Value);

            List<DanceTimelineTrackPanel> tracks = this.GetVisualTreeDescendants<DanceTimelineTrackPanel>();

            int trackIndex = 0;
            bool isAppendSelected = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

            if (!isAppendSelected)
            {
                this.ClearSelectElement();
            }

            foreach (DanceTimelineTrackPanel track in tracks)
            {
                double realBeginY = trackIndex * this.TrackHeight - this.PART_VerticalScrollBar.Value;
                double realEndY = realBeginY + this.TrackHeight;

                foreach (DanceTimelineElement element in track.Children)
                {
                    if (!element.IsVisible)
                        continue;

                    if (realBeginY > selection.Value.Bottom || realEndY < selection.Value.Top)
                        continue;

                    if (element.EndTime < left || element.BeginTime > right)
                        continue;

                    this.SelectElement(element);
                }

                ++trackIndex;
            }

            this.InvokeElementSelectionChanged();
        }
    }
}