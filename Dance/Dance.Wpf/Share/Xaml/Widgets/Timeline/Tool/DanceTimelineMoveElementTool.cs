using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线元素移动工具
    /// </summary>
    public class DanceTimelineMoveElementTool : DanceTimelineToolBase
    {
        /// <summary>
        /// 时间线元素移动工具
        /// </summary>
        /// <param name="timeline">时间线</param>
        public DanceTimelineMoveElementTool(DanceTimeline timeline) : base(timeline)
        {
            timeline.KeyDown -= KeyDown;
            timeline.KeyDown += KeyDown;

            timeline.MouseLeftButtonDown -= MouseLeftButtonDown;
            timeline.MouseLeftButtonDown += MouseLeftButtonDown;

            timeline.MouseLeftButtonUp -= MouseLeftButtonUp;
            timeline.MouseLeftButtonUp += MouseLeftButtonUp;

            timeline.MouseMove -= MouseMove;
            timeline.MouseMove += MouseMove;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            if (this.Timeline == null)
                return;

            this.Timeline.KeyDown -= KeyDown;
            this.Timeline.MouseLeftButtonDown += MouseLeftButtonDown;
            this.Timeline.MouseLeftButtonUp -= MouseLeftButtonUp;
            this.Timeline.MouseMove -= MouseMove;
        }

        // ===================================================================================================
        // Field

        /// <summary>
        /// 开始坐标
        /// </summary>
        private Point? BeginPoint;

        /// <summary>
        /// 开始X轴滚动值
        /// </summary>
        private double? BeginScrollX;

        // ===================================================================================================
        // Public Function

        /// <summary>
        /// 尝试移动元素
        /// </summary>
        /// <param name="moveInfo">移动信息</param>
        /// <returns>是否可以移动</returns>
        public bool TryMoveElement(DanceTimelineMoveElementInfo moveInfo)
        {
            TimeSpan srcWidth = moveInfo.Element.EndTime - moveInfo.Element.BeginTime;
            TimeSpan destWidth = moveInfo.WantEndTime - moveInfo.WantBeginTime;

            if (srcWidth != destWidth || destWidth <= TimeSpan.Zero)
                return false;

            DanceTimelineTrackPanel? destPanel = moveInfo.DestTrack.GetVisualTreeDescendants<DanceTimelineTrackPanel>().FirstOrDefault();
            if (destPanel == null)
                return false;

            if (moveInfo.WantBeginTime < TimeSpan.Zero)
            {
                moveInfo.RealBeginTime = TimeSpan.Zero;
                moveInfo.RealEndTime = destWidth;
            }
            if (moveInfo.WantEndTime > this.Timeline.Duration)
            {
                moveInfo.RealEndTime = this.Timeline.Duration;
                moveInfo.RealBeginTime = moveInfo.RealEndTime - destWidth;
            }

            foreach (DanceTimelineElement item in destPanel.Children)
            {
                if (item == moveInfo.Element)
                    continue;

                if (moveInfo.RealBeginTime <= item.EndTime && moveInfo.RealEndTime >= item.EndTime)
                {
                    moveInfo.RealBeginTime = item.EndTime;
                    moveInfo.RealEndTime = moveInfo.RealBeginTime + destWidth;

                    break;
                }

                if (moveInfo.RealEndTime >= item.BeginTime && moveInfo.RealBeginTime <= item.BeginTime)
                {
                    moveInfo.RealEndTime = item.BeginTime;
                    moveInfo.RealBeginTime = moveInfo.RealEndTime - destWidth;

                    break;
                }
            }

            foreach (DanceTimelineElement item in destPanel.Children)
            {
                if (item == moveInfo.Element)
                    continue;

                if (!(moveInfo.RealEndTime <= item.BeginTime || moveInfo.RealBeginTime >= item.EndTime))
                {
                    return false;
                }
            }

            if (moveInfo.RealBeginTime < TimeSpan.Zero || moveInfo.RealEndTime > this.Timeline.Duration)
                return false;

            return true;
        }

        // ===================================================================================================
        // Private Function

        /// <summary>
        /// 键盘按下
        /// </summary>
        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Alt)
            {
                this.Timeline.ToolStatus = DanceTimelineToolStatus.MoveElement;
            }
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Timeline.ToolStatus != DanceTimelineToolStatus.MoveElement)
                return;

            if (this.Timeline.IsPlaying)
                return;

            if (this.Timeline.PART_FrameSelect == null || this.Timeline.PART_HorizontalScrollBar == null)
                return;

            e.Handled = true;

            DanceTimelineFrameSelectTool? selectionTool = this.Timeline.GetTool<DanceTimelineFrameSelectTool>();
            if (selectionTool == null || selectionTool.Selection.Count == 0)
                return;

            selectionTool.CacheCurrentTime();

            this.BeginPoint = e.GetPosition(this.Timeline.PART_FrameSelect);
            this.BeginScrollX = this.Timeline.PART_HorizontalScrollBar.Value;

            this.Timeline.CaptureMouse();
        }

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.Timeline.ToolStatus != DanceTimelineToolStatus.MoveElement)
                return;

            if (this.Timeline.IsPlaying)
                return;

            if (this.Timeline.PART_FrameSelect == null)
                return;

            e.Handled = true;

            this.BeginPoint = null;
            this.BeginScrollX = null;

            this.Timeline.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Timeline.ToolStatus != DanceTimelineToolStatus.MoveElement)
                return;

            if (this.Timeline.IsPlaying)
                return;

            if (this.Timeline.PART_FrameSelect == null)
                return;

            if (this.BeginPoint == null || this.BeginScrollX == null)
                return;

            e.Handled = true;

            DanceTimelineFrameSelectTool? selectionTool = this.Timeline.GetTool<DanceTimelineFrameSelectTool>();
            if (selectionTool == null || selectionTool.Selection.Count == 0)
                return;

            Point endPoint = e.GetPosition(this.Timeline.PART_FrameSelect);
            double offset = endPoint.X - this.BeginPoint.Value.X;
            TimeSpan offsetTime = this.Timeline.GetTimeSpanFromPixel(offset);

            foreach (DanceTimelineSelectionInfo item in selectionTool.Selection)
            {
                if (item.Element.OwnerTrack == null)
                    continue;

                DanceTimelineMoveElementInfo moveInfo = new(item.Element.OwnerTrack, item.Element.OwnerTrack, item.Element, item.SelectedBeginTime + offsetTime, item.SelectedEndTime + offsetTime);

                if (!this.TryMoveElement(moveInfo))
                    continue;

                moveInfo.Element.BeginTime = moveInfo.RealBeginTime;
                moveInfo.Element.EndTime = moveInfo.RealEndTime;
            }

            this.Timeline.Update();
        }
    }
}
