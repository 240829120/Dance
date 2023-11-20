using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            this.Unloaded += DanceTimeline_Unloaded;
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

        /// <summary>
        /// 刻度字体
        /// </summary>
        public const string FONT_FAMILY = "Microsoft Yahei UI";

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
        /// 进度
        /// </summary>
        internal DanceTimelineProgress? PART_Progress;

        /// <summary>
        /// 滚动条
        /// </summary>
        internal ScrollViewer? PART_ScrollViewer;

        /// <summary>
        /// 时间线面板
        /// </summary>
        internal DanceTimelinePanel? TimeLinePanel;

        /// <summary>
        /// 播放开始时间
        /// </summary>
        private DateTime? PlayTime;

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

        #region IsFollowingProgress -- 是否跟踪进度

        /// <summary>
        /// 是否跟踪进度
        /// </summary>
        public bool IsFollowingProgress
        {
            get { return (bool)GetValue(IsFollowingProgressProperty); }
            set { SetValue(IsFollowingProgressProperty, value); }
        }

        /// <summary>
        /// 是否跟踪进度
        /// </summary>
        public static readonly DependencyProperty IsFollowingProgressProperty =
            DependencyProperty.Register("IsFollowingProgress", typeof(bool), typeof(DanceTimeline), new PropertyMetadata(false));

        #endregion

        #region CurrentTime -- 当前时间

        /// <summary>
        /// 当前时间
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return (TimeSpan)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(TimeSpan), typeof(DanceTimeline), new PropertyMetadata(TimeSpan.Zero, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimeline timeline || timeline.PART_ScrollViewer == null)
                    return;

                if (!timeline.IsFollowingProgress)
                    return;

                timeline.PART_ScrollViewer.ScrollToHorizontalOffset(timeline.CurrentTime.TotalHours * DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * timeline.Zoom - timeline.ActualWidth / 2d);
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
                timeline.PART_Progress?.UpdateMargin();
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

        #region IsPlaying -- 是否正在播放

        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        /// <summary>
        /// 是否正在播放
        /// </summary>
        public static readonly DependencyProperty IsPlayingProperty =
            DependencyProperty.Register("IsPlaying", typeof(bool), typeof(DanceTimeline), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimeline timeline)
                    return;

                timeline.PlayTime = null;
            })));

        #endregion

        // =============================================================================================
        // Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Root = this.Template.FindName(nameof(PART_Root), this) as FrameworkElement;
            this.PART_Scale = this.Template.FindName(nameof(PART_Scale), this) as DanceTimelineScale;
            this.PART_Progress = this.Template.FindName(nameof(PART_Progress), this) as DanceTimelineProgress;
            this.PART_ScrollViewer = this.Template.FindName(nameof(PART_ScrollViewer), this) as ScrollViewer;
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
        // Public Function

        // =============================================================================================
        // Private Function

        /// <summary>
        /// 控件加载
        /// </summary>
        private void DanceTimeline_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.PART_Root == null)
                return;

            this.PART_Root.Width = DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Duration.TotalHours * this.Zoom;
            this.PART_Scale?.InvalidateVisual();


            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        /// <summary>
        /// 卸载
        /// </summary>
        private void DanceTimeline_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            if (!this.IsVisible || !this.IsPlaying)
                return;

            this.PlayTime ??= DateTime.Now;
            this.CurrentTime = DateTime.Now - this.PlayTime.Value;
        }
    }
}
