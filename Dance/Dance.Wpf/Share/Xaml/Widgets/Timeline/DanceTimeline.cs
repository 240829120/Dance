using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线控件
    /// </summary>
    public class DanceTimeline : ItemsControl
    {
        static DanceTimeline()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimeline), new FrameworkPropertyMetadata(typeof(DanceTimeline)));
        }

        public DanceTimeline()
        {
            this.Loaded += DanceTimeline_Loaded;
        }

        /// <summary>
        /// ZOOM 值为1 时1秒绘制宽度
        /// </summary>
        public const int ONE_HOUR_DEFAULT_WIDTH = 10000;

        /// <summary>
        /// 最小刻度宽度，小于该宽度不绘制刻度线
        /// </summary>
        public const int MIN_SCALE_WIDTH = 5;

        /// <summary>
        /// 最小刻度值宽度，小于该宽度不绘制刻度值
        /// </summary>
        public const int MIN_NUMBER_WIDTH = 30;

        // =============================================================================================
        // Field

        /// <summary>
        /// 根元素
        /// </summary>
        internal FrameworkElement? PART_Root;

        /// <summary>
        /// 刻度尺
        /// </summary>
        internal DanceTimelineScale? PART_Scale;

        /// <summary>
        /// 时间线面板
        /// </summary>
        internal DanceTimelinePanel? TimeLinePanel;

        // =============================================================================================
        // Property

        #region Duration -- 持续时间

        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(DanceTimeline), new PropertyMetadata(TimeSpan.FromMinutes(1), new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimeline timeline || timeline.PART_Root == null)
                    return;

                timeline.PART_Root.Width = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * timeline.Duration.TotalHours * timeline.Zoom;
            })));

        #endregion

        #region Zoom -- 缩放

        /// <summary>
        /// 缩放
        /// </summary>
        public double Zoom
        {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        /// <summary>
        /// 缩放
        /// </summary>
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(double), typeof(DanceTimeline), new PropertyMetadata(1d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimeline timeline || timeline.PART_Root == null)
                    return;

                timeline.PART_Root.Width = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * timeline.Duration.TotalHours * timeline.Zoom;
            })));

        #endregion

        #region TrackHeight -- 轨道高度

        /// <summary>
        /// 轨道高度
        /// </summary>
        public double TrackHeight
        {
            get { return (double)GetValue(TrackHeightProperty); }
            set { SetValue(TrackHeightProperty, value); }
        }

        /// <summary>
        /// 轨道高度
        /// </summary>
        public static readonly DependencyProperty TrackHeightProperty =
            DependencyProperty.Register("TrackHeight", typeof(double), typeof(DanceTimeline), new PropertyMetadata(25d));

        #endregion

        #region TrackItemDataTemplate -- 轨道项数据模板

        /// <summary>
        /// 轨道项数据模板
        /// </summary>
        public DataTemplate TrackItemDataTemplate
        {
            get { return (DataTemplate)GetValue(TrackItemDataTemplateProperty); }
            set { SetValue(TrackItemDataTemplateProperty, value); }
        }

        /// <summary>
        /// 轨道项数据模板
        /// </summary>
        public static readonly DependencyProperty TrackItemDataTemplateProperty =
            DependencyProperty.Register("TrackItemDataTemplate", typeof(DataTemplate), typeof(DanceTimeline), new PropertyMetadata(null));

        #endregion

        #region TrackHeaderDataTemplate -- 轨道头部数据模板

        /// <summary>
        /// 轨道头部数据模板
        /// </summary>
        public DataTemplate TrackHeaderDataTemplate
        {
            get { return (DataTemplate)GetValue(TrackHeaderDataTemplateProperty); }
            set { SetValue(TrackHeaderDataTemplateProperty, value); }
        }

        /// <summary>
        /// 轨道头部数据模板
        /// </summary>
        public static readonly DependencyProperty TrackHeaderDataTemplateProperty =
            DependencyProperty.Register("TrackHeaderDataTemplate", typeof(DataTemplate), typeof(DanceTimeline), new PropertyMetadata(null));

        #endregion

        // =============================================================================================
        // Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Root = this.Template.FindName(nameof(PART_Root), this) as FrameworkElement;
            this.PART_Scale = this.Template.FindName(nameof(PART_Scale), this) as DanceTimelineScale;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceTimelineTrack;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTimelineTrack() { Timeline = this };
        }

        // =============================================================================================
        // Private Function

        private void DanceTimeline_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.PART_Root == null)
                return;

            this.PART_Root.Width = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Duration.TotalHours * this.Zoom;
            this.PART_Scale?.InvalidateVisual();
        }

    }
}
