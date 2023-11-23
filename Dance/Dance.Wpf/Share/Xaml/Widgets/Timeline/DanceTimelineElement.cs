using CommunityToolkit.Mvvm.Input;
using log4net;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
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

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            if (this.OwnerTimeline == null || this.OwnerTimeline.ToolStatus != DanceTimelineToolStatus.CopyMoveElement)
                return;

            if (this.OwnerTimeline.IsPlaying)
                return;

            DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
        }
    }
}
