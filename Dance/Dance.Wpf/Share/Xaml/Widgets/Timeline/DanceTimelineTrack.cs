using SharpVectors.Dom;
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
    /// 时间线轨道
    /// </summary>
    [TemplatePart(Name = nameof(PART_Highlight), Type = typeof(DanceTimelineTrackHighlight))]
    public class DanceTimelineTrack : ItemsControl
    {
        static DanceTimelineTrack()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrack), new FrameworkPropertyMetadata(typeof(DanceTimelineTrack)));
        }

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 高亮
        /// </summary>
        internal DanceTimelineTrackHighlight? PART_Highlight;

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? OwnerTimeline;

        /// <summary>
        /// 所属容器
        /// </summary>
        internal DanceTimelineTrackHeaderPanel? OwnerPanel;

        // ==========================================================================================================================================
        // Override

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.OwnerTimeline = this.GetVisualTreeParent<DanceTimeline>();
            this.OwnerPanel = this.GetVisualTreeParent<DanceTimelineTrackHeaderPanel>();
            this.PART_Highlight = this.Template.FindName(nameof(PART_Highlight), this) as DanceTimelineTrackHighlight;
        }

        /// <summary>
        /// 是否是子元素容器
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceTimelineElement;
        }

        /// <summary>
        /// 获取子元素容器
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTimelineElement();
        }

        /// <summary>
        /// 拖拽经过
        /// </summary>
        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            if (this.OwnerTimeline == null || this.PART_Highlight == null || this.OwnerTimeline.PART_FrameSelect == null || this.OwnerTimeline.PART_HorizontalScrollBar == null)
                return;

            if (this.OwnerTimeline.IsPlaying || this.OwnerTimeline.IsReadOnly)
                return;

            if (this.OwnerTimeline.GetTool<DanceTimelineCopyMoveTool>() is not DanceTimelineCopyMoveTool tool)
                return;

            DanceTimelineElementDragEventArgs args = new(this.OwnerTimeline, this, e);
            this.OwnerTimeline.InvokeElementDragOver(args);

            if (args.BeginTime == null || args.EndTime == null)
                return;

            Point point = e.GetPosition(this.OwnerTimeline.PART_FrameSelect);
            TimeSpan wantBeginTime = this.OwnerTimeline.GetTimeSpanFromPixel(point.X + this.OwnerTimeline.PART_HorizontalScrollBar.Value);
            TimeSpan wantEndTime = wantBeginTime + (args.EndTime.Value - args.BeginTime.Value);

            DanceTimelineMoveElementInfo moveInfo = new(this, args.BeginTime.Value, args.EndTime.Value, wantBeginTime, wantEndTime);
            if (!tool.TryCopyMove(moveInfo))
            {
                this.PART_Highlight.BeginX = null;
                this.PART_Highlight.EndX = null;

                return;
            }

            this.PART_Highlight.BeginX = this.OwnerTimeline.GetPixelFromTimeSpan(moveInfo.RealBeginTime) - this.OwnerTimeline.PART_HorizontalScrollBar.Value;
            this.PART_Highlight.EndX = this.OwnerTimeline.GetPixelFromTimeSpan(moveInfo.RealEndTime) - this.OwnerTimeline.PART_HorizontalScrollBar.Value;

            this.PART_Highlight.InvalidateVisual();
        }

        /// <summary>
        /// 拖拽离开
        /// </summary>
        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);

            if (this.PART_Highlight == null)
                return;

            this.PART_Highlight.BeginX = null;
            this.PART_Highlight.EndX = null;

            this.PART_Highlight.InvalidateVisual();
        }

        /// <summary>
        /// 拖拽释放
        /// </summary>
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (this.OwnerTimeline == null || this.PART_Highlight == null || this.OwnerTimeline.PART_FrameSelect == null || this.OwnerTimeline.PART_HorizontalScrollBar == null)
                return;

            if (this.OwnerTimeline.IsPlaying || this.OwnerTimeline.IsReadOnly)
                return;

            if (this.OwnerTimeline.GetTool<DanceTimelineCopyMoveTool>() is not DanceTimelineCopyMoveTool tool)
                return;

            if (this.PART_Highlight.BeginX == null || this.PART_Highlight.EndX == null)
                return;

            TimeSpan beginTime = this.OwnerTimeline.GetTimeSpanFromPixel(this.OwnerTimeline.PART_HorizontalScrollBar.Value + this.PART_Highlight.BeginX.Value);
            TimeSpan endTime = this.OwnerTimeline.GetTimeSpanFromPixel(this.OwnerTimeline.PART_HorizontalScrollBar.Value + this.PART_Highlight.EndX.Value);

            DanceTimelineMoveElementInfo moveInfo = new(this, beginTime, endTime, beginTime, endTime);
            if (tool.TryCopyMove(moveInfo))
            {
                DanceTimelineElementDragEventArgs args = new(this.OwnerTimeline, this, e);
                args.BeginTime = moveInfo.RealBeginTime;
                args.EndTime = moveInfo.RealEndTime;

                this.OwnerTimeline.InvokeElementDrop(args);
            }

            this.PART_Highlight.BeginX = null;
            this.PART_Highlight.EndX = null;

            this.PART_Highlight.InvalidateVisual();
        }
    }
}
