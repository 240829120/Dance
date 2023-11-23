using CommunityToolkit.Mvvm.Input;
using log4net;
using Microsoft.VisualBasic.Logging;
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
        internal DanceTimeline? OwnerTimeline;

        /// <summary>
        /// 所属轨道
        /// </summary>
        internal DanceTimelineTrack? OwnerTrack;

        /// <summary>
        /// 所属轨道容器
        /// </summary>
        internal DanceTimelineTrackPanel? OwnerTrackPanel;

        /// <summary>
        /// 鼠标左键点击坐标
        /// </summary>
        private Point? MouseLeftButtonDownPoint;

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
            this.OwnerTrackPanel = this.GetVisualTreeParent<DanceTimelineTrackPanel>();
        }

        ///// <summary>
        ///// 鼠标左键点击
        ///// </summary>
        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    base.OnMouseLeftButtonDown(e);

        //    e.Handled = true;

        //    if (this.OwnerTimeline == null)
        //        return;

        //    this.MouseLeftButtonDownPoint = e.GetPosition(this.OwnerTimeline);

        //    if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
        //    {
        //        this.OwnerTimeline.SelectElement(this);
        //    }
        //    else
        //    {
        //        if (!Keyboard.IsKeyDown(Key.LeftAlt) && !Keyboard.IsKeyDown(Key.RightAlt))
        //        {
        //            this.OwnerTimeline.ClearSelectElement();
        //        }
        //        this.OwnerTimeline.SelectElement(this);
        //    }

        //    this.OwnerTimeline.InvokeElementSelectionChanged();

        //    this.ElementMoveCacheDic.Clear();
        //    foreach (DanceTimelineElement item in this.OwnerTimeline.GetSelectedElements())
        //    {
        //        this.ElementMoveCacheDic[item] = new Tuple<TimeSpan, TimeSpan>(item.BeginTime, item.EndTime);
        //    }

        //    this.CaptureMouse();
        //}

        ///// <summary>
        ///// 鼠标抬起
        ///// </summary>
        //protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        //{
        //    base.OnMouseLeftButtonUp(e);

        //    e.Handled = true;

        //    this.ReleaseMouseCapture();
        //}

        ///// <summary>
        ///// 鼠标移动
        ///// </summary>
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);

        //    if (this.OwnerTimeline == null || this.OwnerTrack == null || this.OwnerTrackPanel == null)
        //        return;

        //    if (this.MouseLeftButtonDownPoint == null)
        //        return;

        //    if (!Keyboard.IsKeyDown(Key.LeftAlt) && !Keyboard.IsKeyDown(Key.RightAlt))
        //        return;

        //    e.Handled = true;

        //    if (this.ElementMoveCacheDic.Count == 0)
        //        return;

        //    Point endPoint = e.GetPosition(this.OwnerTimeline);
        //    double offset = endPoint.X - this.MouseLeftButtonDownPoint.Value.X;
        //    TimeSpan offsetTime = this.OwnerTimeline.GetTimeSpanFromPixel(offset);

        //    foreach (var kv in this.ElementMoveCacheDic)
        //    {
        //        kv.Key.TryMove(offsetTime, kv.Value.Item1, kv.Value.Item2);
        //    }

        //    this.OwnerTimeline.Update();
        //}


        // ==========================================================================================================================================
        // Internal

        /// <summary>
        /// 尝试移动
        /// </summary>
        /// <param name="offsetTime">偏移量时间</param>
        /// <returns>是否成功移动</returns>
        internal bool TryMove(TimeSpan offsetTime, TimeSpan cacheBeginTime, TimeSpan cacheEndTime)
        {
            if (this.OwnerTimeline == null || this.OwnerTrackPanel == null)
                return false;

            TimeSpan beginTime = cacheBeginTime + offsetTime;
            TimeSpan endTime = cacheEndTime + offsetTime;
            beginTime = this.OwnerTimeline.GetEffectiveTimeSpan(beginTime);
            endTime = this.OwnerTimeline.GetEffectiveTimeSpan(endTime);
            TimeSpan width = this.EndTime - this.BeginTime;

            if (!this.OwnerTrackPanel.GetEffectiveMoveTimeSpan(ref beginTime, ref endTime, width, this))
                return false;

            this.BeginTime = beginTime;
            this.EndTime = endTime;

            return true;
        }
    }
}
