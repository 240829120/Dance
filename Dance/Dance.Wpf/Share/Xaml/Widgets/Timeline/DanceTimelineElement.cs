using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线元素
    /// </summary>
    public class DanceTimelineElement : ContentControl
    {
        static DanceTimelineElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineElement), new FrameworkPropertyMetadata(typeof(DanceTimelineElement)));
        }

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? OwnerTimeline = null;

        /// <summary>
        /// 所属轨道
        /// </summary>
        internal DanceTimelineTrack? OwnerTrack = null;

        /// <summary>
        /// 鼠标左键点击坐标
        /// </summary>
        private Point? MouseLeftButtonDownPoint;

        /// <summary>
        /// 鼠标左键点击时的开始时间
        /// </summary>
        private TimeSpan? MouseLeftButtonDownBeginTime;

        /// <summary>
        /// 鼠标左键点击时的结束时间
        /// </summary>
        private TimeSpan? MouseLeftButtonDownEndTime;

        // ==========================================================================================================================================
        // Property

        #region IsSelected -- 是否被选中

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(DanceTimelineElement), new PropertyMetadata(false));

        #endregion

        #region BeginTime -- 开始时间

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan BeginTime
        {
            get { return (TimeSpan)GetValue(BeginTimeProperty); }
            set { SetValue(BeginTimeProperty, value); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public static readonly DependencyProperty BeginTimeProperty =
            DependencyProperty.Register("BeginTime", typeof(TimeSpan), typeof(DanceTimelineElement), new PropertyMetadata(TimeSpan.Zero));

        #endregion

        #region EndTime -- 结束时间

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan EndTime
        {
            get { return (TimeSpan)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(TimeSpan), typeof(DanceTimelineElement), new PropertyMetadata(TimeSpan.Zero));

        #endregion

        // ==========================================================================================================================================
        // Override

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.OwnerTimeline = this.GetVisualTreeParent<DanceTimeline>();
            this.OwnerTrack = this.GetVisualTreeParent<DanceTimelineTrack>();
        }

        /// <summary>
        /// 鼠标左键点击
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            e.Handled = true;

            if (this.OwnerTimeline == null)
                return;

            this.MouseLeftButtonDownPoint = e.GetPosition(this.OwnerTimeline);
            this.MouseLeftButtonDownBeginTime = this.BeginTime;
            this.MouseLeftButtonDownEndTime = this.EndTime;

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                this.OwnerTimeline.SelectElement(this);
            }
            else
            {
                this.OwnerTimeline.ClearSelectElement();
                this.OwnerTimeline.SelectElement(this);
            }

            this.OwnerTimeline.InvokeElementSelectionChanged();

            this.CaptureMouse();
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            e.Handled = true;

            this.MouseLeftButtonDownPoint = null;
            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.OwnerTimeline == null || this.OwnerTrack == null || this.OwnerTimeline.PART_HorizontalScrollBar == null || this.OwnerTimeline.PART_VerticalScrollBar == null)
                return;

            if (this.MouseLeftButtonDownPoint == null || this.MouseLeftButtonDownBeginTime == null || this.MouseLeftButtonDownEndTime == null)
                return;

            if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.LeftAlt))
            {
                e.Handled = true;

                Point endPoint = e.GetPosition(this.OwnerTimeline);

                double offset = endPoint.X - this.MouseLeftButtonDownPoint.Value.X;
                TimeSpan offsetTime = this.OwnerTimeline.GetTimeSpanFromPixel(offset);
                TimeSpan beginTime = this.MouseLeftButtonDownBeginTime.Value + offsetTime;
                TimeSpan endTime = this.MouseLeftButtonDownEndTime.Value + offsetTime;

                this.BeginTime = this.OwnerTimeline.GetEffectiveTimeSpan(beginTime);
                this.EndTime = this.OwnerTimeline.GetEffectiveTimeSpan(endTime);

                this.OwnerTimeline.Update();
            }
        }
    }
}
