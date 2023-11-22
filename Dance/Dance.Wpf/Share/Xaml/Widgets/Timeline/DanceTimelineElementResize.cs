using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线元素改变时间方式
    /// </summary>
    public enum DanceTimelineElementResizeType
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        BeginTime,

        /// <summary>
        /// 结束时间
        /// </summary>
        EndTime
    }

    /// <summary>
    /// 时间线元素改变时间
    /// </summary>
    public class DanceTimelineElementResize : Control
    {
        static DanceTimelineElementResize()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineElementResize), new FrameworkPropertyMetadata(typeof(DanceTimelineElementResize)));
        }

        // ==================================================================================================
        // Field

        /// <summary>
        /// 所属元素
        /// </summary>
        private DanceTimelineElement? Element;

        /// <summary>
        /// 鼠标左键按下时的坐标
        /// </summary>
        private Point? MouseLeftButtonDownPoint;

        /// <summary>
        /// 鼠标左键按下时的时间
        /// </summary>
        private TimeSpan? MouseLeftButtonDownTime;

        // ==================================================================================================
        // Property

        #region ResizeType -- 改变方式

        /// <summary>
        /// 改变方式
        /// </summary>
        public DanceTimelineElementResizeType ResizeType
        {
            get { return (DanceTimelineElementResizeType)GetValue(ResizeTypeProperty); }
            set { SetValue(ResizeTypeProperty, value); }
        }

        /// <summary>
        /// 改变方式
        /// </summary>
        public static readonly DependencyProperty ResizeTypeProperty =
            DependencyProperty.Register("ResizeType", typeof(DanceTimelineElementResizeType), typeof(DanceTimelineElementResize), new PropertyMetadata(DanceTimelineElementResizeType.BeginTime));

        #endregion

        // ==================================================================================================
        // Override

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Element = this.GetVisualTreeParent<DanceTimelineElement>();
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (this.Element == null || this.Element.OwnerTimeline == null)
                return;

            this.MouseLeftButtonDownPoint = e.GetPosition(this.Element.OwnerTimeline);
            this.MouseLeftButtonDownTime = this.ResizeType == DanceTimelineElementResizeType.BeginTime ? this.Element.BeginTime : this.Element.EndTime;
            e.Handled = true;
            this.CaptureMouse();
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            this.MouseLeftButtonDownPoint = null;
            this.MouseLeftButtonDownTime = null;
            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.Element == null || this.Element.OwnerTimeline == null || this.MouseLeftButtonDownPoint == null || this.MouseLeftButtonDownTime == null)
                return;

            Point endPoint = e.GetPosition(this.Element.OwnerTimeline);

            double offset = endPoint.X - this.MouseLeftButtonDownPoint.Value.X;
            TimeSpan offsetTime = this.Element.OwnerTimeline.GetTimeSpanFromPixel(offset);
            TimeSpan destTime = this.MouseLeftButtonDownTime.Value + offsetTime;

            if (this.ResizeType == DanceTimelineElementResizeType.BeginTime)
            {
                destTime = TimeSpan.FromSeconds(Math.Min(destTime.TotalSeconds, this.Element.EndTime.TotalSeconds - 1d));
                destTime = this.Element.OwnerTimeline.GetEffectiveTimeSpan(destTime);

                this.Element.BeginTime = destTime;
                this.Element.OwnerTimeline.Update();
            }
            else if (this.ResizeType == DanceTimelineElementResizeType.EndTime)
            {
                destTime = TimeSpan.FromSeconds(Math.Max(destTime.TotalSeconds, this.Element.BeginTime.TotalSeconds + 1d));
                destTime = this.Element.OwnerTimeline.GetEffectiveTimeSpan(destTime);

                this.Element.EndTime = destTime;
                this.Element.OwnerTimeline.Update();
            }
        }
    }
}
