using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线框选工具
    /// </summary>
    public class DanceTimelineFrameSelectTool : DanceTimelineToolBase
    {
        /// <summary>
        /// 时间线框选工具
        /// </summary>
        /// <param name="timeline">时间线</param>
        public DanceTimelineFrameSelectTool(DanceTimeline timeline) : base(timeline)
        {
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

            this.Timeline.MouseLeftButtonDown += MouseLeftButtonDown;
            this.Timeline.MouseLeftButtonUp -= MouseLeftButtonUp;
            this.Timeline.MouseMove -= MouseMove;
        }

        /// <summary>
        /// 开始坐标点
        /// </summary>
        private Point? BeginPoint;

        /// <summary>
        /// 选择
        /// </summary>
        public List<DanceTimelineSelectionInfo> Selection { get; private set; } = [];

        /// <summary>
        /// 清理框选
        /// </summary>
        public void ClearFrameSelect()
        {
            if (this.Timeline.PART_FrameSelect == null)
                return;

            this.Timeline.PART_FrameSelect.BeginPoint = null;
            this.Timeline.PART_FrameSelect.EndPoint = null;
        }

        /// <summary>
        /// 清理选择
        /// </summary>
        public void ClearSelection()
        {
            lock (this.Selection)
            {
                this.Selection.ForEach(p => p.Element.IsSelected = false);
                this.Selection.Clear();
            }
        }

        /// <summary>
        /// 更新选择区域
        /// </summary>
        public void UpdateSelection()
        {
            if (this.Timeline.Status != DanceTimelineStatus.FrameSelect)
                return;

            if (this.Timeline.IsPlaying && this.Timeline.IsFollowProgress)
                return;

            if (this.Timeline.PART_HorizontalScrollBar == null || this.Timeline.PART_VerticalScrollBar == null || this.Timeline.PART_FrameSelect == null)
                return;

            if (this.BeginPoint == null)
                return;

            List<DanceTimelineTrackPanel> tracks = this.Timeline.GetVisualTreeDescendants<DanceTimelineTrackPanel>();

            lock (this.Selection)
            {
                this.Selection.Clear();

                foreach (DanceTimelineTrackPanel track in tracks)
                {
                    foreach (DanceTimelineElement element in track.Children)
                    {
                        if (!element.IsSelected)
                            continue;

                        this.Selection.Add(new DanceTimelineSelectionInfo(element));
                    }
                }
            }

            this.Timeline.InvokeElementSelectionChanged();
        }

        /// <summary>
        /// 缓存当前时间
        /// </summary>
        public void CacheCurrentTime()
        {
            lock (this.Selection)
            {
                this.Selection.ForEach(p =>
                {
                    p.SelectedBeginTime = p.Element.BeginTime;
                    p.SelectedEndTime = p.Element.EndTime;
                });
            }
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Timeline.Status != DanceTimelineStatus.FrameSelect)
                return;

            if (this.Timeline.IsPlaying && this.Timeline.IsFollowProgress || this.Timeline.IsReadOnly)
                return;

            if (this.Timeline.PART_FrameSelect == null)
                return;

            e.Handled = true;

            this.BeginPoint = e.GetPosition(this.Timeline.PART_FrameSelect);

            this.Timeline.CaptureMouse();
        }

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.Timeline.Status != DanceTimelineStatus.FrameSelect)
                return;

            if (this.Timeline.IsPlaying && this.Timeline.IsFollowProgress || this.Timeline.IsReadOnly)
                return;

            if (this.Timeline.PART_HorizontalScrollBar == null || this.Timeline.PART_VerticalScrollBar == null)
                return;

            e.Handled = true;

            this.SelectElementFromFrameSelect();
            this.BeginPoint = null;
            this.ClearFrameSelect();
            this.Timeline.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Timeline.Status != DanceTimelineStatus.FrameSelect)
                return;

            if (this.Timeline.IsPlaying && this.Timeline.IsFollowProgress || this.Timeline.IsReadOnly)
                return;

            if (this.Timeline.PART_HorizontalScrollBar == null || this.Timeline.PART_VerticalScrollBar == null || this.Timeline.PART_FrameSelect == null)
                return;

            if (this.BeginPoint == null)
                return;

            e.Handled = true;

            Point endPoint = e.GetPosition(this.Timeline.PART_FrameSelect);

            this.Timeline.PART_FrameSelect.BeginPoint = this.BeginPoint;
            this.Timeline.PART_FrameSelect.EndPoint = endPoint;
        }

        /// <summary>
        /// 根据款选选择元素
        /// </summary>
        private void SelectElementFromFrameSelect()
        {
            if (this.Timeline.Status != DanceTimelineStatus.FrameSelect)
                return;

            if (this.Timeline.IsPlaying && this.Timeline.IsFollowProgress)
                return;

            if (this.Timeline.PART_HorizontalScrollBar == null || this.Timeline.PART_VerticalScrollBar == null || this.Timeline.PART_FrameSelect == null)
                return;

            if (this.BeginPoint == null)
                return;

            Rect? selection = this.Timeline.PART_FrameSelect.GetSelection();
            if (selection == null)
                return;

            TimeSpan left = this.Timeline.GetTimeSpanFromPixel(selection.Value.Left + this.Timeline.PART_HorizontalScrollBar.Value);
            TimeSpan right = this.Timeline.GetTimeSpanFromPixel(selection.Value.Right + this.Timeline.PART_HorizontalScrollBar.Value);

            List<DanceTimelineTrackPanel> tracks = this.Timeline.GetVisualTreeDescendants<DanceTimelineTrackPanel>();

            int trackIndex = 0;
            bool isCtrlDown = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

            if (!isCtrlDown)
            {
                this.ClearSelection();
            }

            lock (this.Selection)
            {
                foreach (DanceTimelineTrackPanel track in tracks)
                {
                    double realBeginY = trackIndex * this.Timeline.TrackHeight - this.Timeline.PART_VerticalScrollBar.Value;
                    double realEndY = realBeginY + this.Timeline.TrackHeight;

                    foreach (DanceTimelineElement element in track.Children)
                    {
                        if (!element.IsVisible)
                            continue;

                        if (realBeginY > selection.Value.Bottom || realEndY < selection.Value.Top)
                            continue;

                        if (element.EndTime < left || element.BeginTime > right)
                            continue;

                        if (element.IsSelected)
                        {
                            element.IsSelected = false;
                            DanceTimelineSelectionInfo? info = this.Selection.FirstOrDefault(p => p.Element == element);
                            if (info != null)
                            {
                                this.Selection.Remove(info);
                            }
                        }
                        else
                        {
                            element.IsSelected = true;
                            this.Selection.Add(new DanceTimelineSelectionInfo(element));
                        }
                    }

                    ++trackIndex;
                }
            }

            this.Timeline.InvokeElementSelectionChanged();
        }
    }
}