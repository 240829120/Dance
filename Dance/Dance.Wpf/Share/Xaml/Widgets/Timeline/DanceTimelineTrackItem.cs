using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线轨道项
    /// </summary>
    public class DanceTimelineTrackItem : ContentControl
    {
        static DanceTimelineTrackItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrackItem), new FrameworkPropertyMetadata(typeof(DanceTimelineTrackItem)));
        }

        // =============================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? Timeline;

        /// <summary>
        /// 所属轨道
        /// </summary>
        internal DanceTimelineTrack? TimelineTrack;

        // =============================================================================================
        // Property

        #region BeginTime -- 开始时间

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan? BeginTime
        {
            get { return (TimeSpan?)GetValue(BeginTimeProperty); }
            set { SetValue(BeginTimeProperty, value); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public static readonly DependencyProperty BeginTimeProperty =
            DependencyProperty.Register("BeginTime", typeof(TimeSpan), typeof(DanceTimelineTrackItem), new PropertyMetadata(TimeSpan.Zero));

        #endregion

        #region EndTime -- 结束时间

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan? EndTime
        {
            get { return (TimeSpan?)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(TimeSpan), typeof(DanceTimelineTrackItem), new PropertyMetadata(TimeSpan.Zero));

        #endregion
    }
}
