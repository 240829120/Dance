using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线
    /// </summary>
    [TemplatePart(Name = nameof(PART_VerticalScrollBar), Type = typeof(ScrollBar))]
    [TemplatePart(Name = nameof(PART_HorizontalScrollBar), Type = typeof(ScrollBar))]
    [TemplatePart(Name = nameof(PART_Scale), Type = typeof(DanceTimelineScale))]
    [TemplatePart(Name = nameof(PART_HeaderItems), Type = typeof(DanceTimelineTrackHeaderItems))]
    [TemplatePart(Name = nameof(PART_TrackItems), Type = typeof(ItemsPresenter))]
    [TemplatePart(Name = nameof(PART_Progress), Type = typeof(DanceTimelineProgress))]
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

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 默认Zoom为1时的1秒宽度
        /// </summary>
        public const double ONE_SECOND_DEFAULT_WIDTH = 1d;

        /// <summary>
        /// 刻度最小宽度
        /// </summary>
        public const double MIN_SCALE_WIDTH = 5d;

        /// <summary>
        /// 刻度间隔超过该值则绘制长刻度
        /// </summary>
        public const double MIN_LARGE_SCALE_WIDTH = 60d;

        /// <summary>
        /// 数字最小宽度
        /// </summary>
        public const double MIN_NUMBER_WIDTH = 30d;

        /// <summary>
        /// 字体
        /// </summary>
        public const string FONT_FAMILY = "Microsoft Yahei UI";

        /// <summary>
        /// 垂直滚动条
        /// </summary>
        [NotNull]
        internal ScrollBar? PART_VerticalScrollBar = null;

        /// <summary>
        /// 水平滚动条
        /// </summary>
        [NotNull]
        internal ScrollBar? PART_HorizontalScrollBar = null;

        /// <summary>
        /// 刻度
        /// </summary>
        [NotNull]
        internal DanceTimelineScale? PART_Scale = null;

        /// <summary>
        /// 进度
        /// </summary>
        [NotNull]
        internal DanceTimelineProgress? PART_Progress = null;

        /// <summary>
        /// 头部集合
        /// </summary>
        [NotNull]
        internal DanceTimelineTrackHeaderItems? PART_HeaderItems = null;

        /// <summary>
        /// 轨道集合
        /// </summary>
        [NotNull]
        internal ItemsPresenter? PART_TrackItems = null;

        /// <summary>
        /// 上一次更新时间
        /// </summary>
        private DateTime? LastUpdateTime;

        // ==========================================================================================================================================
        // Property

        #region TitleTemplate -- 标题模板

        /// <summary>
        /// 标题模板
        /// </summary>
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        /// <summary>
        /// 标题模板
        /// </summary>
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(DanceTimeline), new PropertyMetadata(null));

        #endregion

        #region HeaderTemplate -- 头部模板

        /// <summary>
        /// 头部模板
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        /// <summary>
        /// 头部模板
        /// </summary>
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(DataTemplate), typeof(DanceTimeline), new PropertyMetadata(null));

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
                if (s is not DanceTimeline timeline)
                    return;

                if (timeline.IsPlaying && timeline.IsFollowProgress && timeline.PART_HorizontalScrollBar != null && timeline.PART_TrackItems != null)
                {
                    timeline.PART_HorizontalScrollBar.Value = timeline.GetPixelFromTimeSpan(timeline.CurrentTime + timeline.GetTimeSpanFromPixel(timeline.PART_TrackItems.ActualWidth / 2d));
                    return;
                }

                timeline.Update();

            })));

        #endregion

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
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(DanceTimeline), new PropertyMetadata(TimeSpan.FromHours(1)));

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
                if (s is not DanceTimeline timeline)
                    return;

                timeline.Update();
            })));

        #endregion

        #region ProgressWidth -- 进度条宽度

        /// <summary>
        /// 进度条宽度
        /// </summary>
        public double ProgressWidth
        {
            get { return (double)GetValue(ProgressWidthProperty); }
            set { SetValue(ProgressWidthProperty, value); }
        }

        /// <summary>
        /// 进度条宽度
        /// </summary>
        public static readonly DependencyProperty ProgressWidthProperty =
            DependencyProperty.Register("ProgressWidth", typeof(double), typeof(DanceTimeline), new PropertyMetadata(5d));

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

                timeline.LastUpdateTime = null;

            })));

        #endregion

        #region IsFollowProgress -- 是否跟随进度

        /// <summary>
        /// 是否跟随进度
        /// </summary>
        public bool IsFollowProgress
        {
            get { return (bool)GetValue(IsFollowProgressProperty); }
            set { SetValue(IsFollowProgressProperty, value); }
        }

        /// <summary>
        /// 是否跟随进度
        /// </summary>
        public static readonly DependencyProperty IsFollowProgressProperty =
            DependencyProperty.Register("IsFollowProgress", typeof(bool), typeof(DanceTimeline), new PropertyMetadata(false));

        #endregion

        // ==========================================================================================================================================
        // Override

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Scale = this.Template.FindName(nameof(PART_Scale), this) as DanceTimelineScale;
            this.PART_HeaderItems = this.Template.FindName(nameof(PART_HeaderItems), this) as DanceTimelineTrackHeaderItems;
            this.PART_TrackItems = this.Template.FindName(nameof(PART_TrackItems), this) as ItemsPresenter;
            this.PART_Progress = this.Template.FindName(nameof(PART_Progress), this) as DanceTimelineProgress;

            if (this.PART_Scale != null)
            {
                this.PART_Scale.OwnerTimeline = this;
            }

            this.PART_VerticalScrollBar = this.Template.FindName(nameof(PART_VerticalScrollBar), this) as ScrollBar;
            if (this.PART_VerticalScrollBar != null)
            {
                this.PART_VerticalScrollBar.ValueChanged -= PART_VerticalScrollBar_ValueChanged;
                this.PART_VerticalScrollBar.ValueChanged += PART_VerticalScrollBar_ValueChanged;
            }

            this.PART_HorizontalScrollBar = this.Template.FindName(nameof(PART_HorizontalScrollBar), this) as ScrollBar;
            if (this.PART_HorizontalScrollBar != null)
            {
                this.PART_HorizontalScrollBar.ValueChanged -= PART_HorizontalScrollBar_ValueChanged;
                this.PART_HorizontalScrollBar.ValueChanged += PART_HorizontalScrollBar_ValueChanged;
            }

            this.Update();
        }

        /// <summary>
        /// 是否是子元素容器
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceTimelineTrack;
        }

        /// <summary>
        /// 获取子元素容器
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTimelineTrack() { OwnerTimeline = this };
        }

        /// <summary>
        /// 渲染大小改变
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            this.Update();
        }

        // ==========================================================================================================================================
        // Internal Function

        /// <summary>
        /// 根据时间获取像素
        /// </summary>
        /// <param name="timeSpan">时间间隔</param>
        /// <returns>像素</returns>
        internal double GetPixelFromTimeSpan(TimeSpan timeSpan)
        {
            return timeSpan.TotalSeconds * ONE_SECOND_DEFAULT_WIDTH * this.Zoom;
        }

        /// <summary>
        /// 根据像素获取时间间隔
        /// </summary>
        /// <param name="pixel">像素</param>
        /// <returns>时间间隔</returns>
        internal TimeSpan GetTimeSpanFromPixel(double pixel)
        {
            return TimeSpan.FromSeconds(pixel / (ONE_SECOND_DEFAULT_WIDTH * this.Zoom));
        }

        /// <summary>
        /// 获取有效的时间间隔
        /// </summary>
        /// <param name="timeSpan">时间间隔</param>
        /// <returns>有效的时间间隔</returns>
        internal TimeSpan GetEffectiveTimeSpan(TimeSpan timeSpan)
        {
            TimeSpan dest = timeSpan < TimeSpan.Zero ? TimeSpan.Zero : timeSpan;
            dest = dest > this.Duration ? this.Duration : dest;
            dest = TimeSpan.FromSeconds(Math.Round(dest.TotalSeconds, 1));

            return dest;
        }

        // ==========================================================================================================================================
        // Private Function

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            if (this.PART_HorizontalScrollBar == null || this.PART_VerticalScrollBar == null || this.PART_Scale == null || this.PART_HeaderItems == null || this.PART_Progress == null)
                return;

            this.PART_VerticalScrollBar.Maximum = this.PART_TrackItems.ActualHeight;
            this.PART_HorizontalScrollBar.Maximum = this.Duration.TotalSeconds * ONE_SECOND_DEFAULT_WIDTH * this.Zoom;
            this.PART_HorizontalScrollBar.ViewportSize = this.PART_HorizontalScrollBar.Maximum * 0.1d;
            double left = this.PART_HorizontalScrollBar.Value - this.GetPixelFromTimeSpan(this.CurrentTime);
            this.PART_Progress.Margin = new Thickness(-left - this.ProgressWidth / 2d, 0, 0, 0);
            this.PART_Progress.Visibility = (-left < 0 || left > this.ProgressWidth) ? Visibility.Collapsed : Visibility.Visible;
            this.PART_Scale.InvalidateVisual();
            DanceXamlExpansion.GetVisualTreeDescendants<DanceTimelineTrackPanel>(this)?.ForEach(p => p.InvalidateVisual());
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceTimeline_Loaded(object sender, RoutedEventArgs e)
        {
            this.Update();

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

            this.LastUpdateTime ??= DateTime.Now;
            TimeSpan offset = DateTime.Now - this.LastUpdateTime.Value;
            offset = this.GetEffectiveTimeSpan(offset);
            if (offset <= TimeSpan.Zero)
                return;

            this.CurrentTime += offset;
            this.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 水平滚动条值改变
        /// </summary>
        private void PART_HorizontalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Update();
        }

        /// <summary>
        /// 垂直滚动条值改变
        /// </summary>
        private void PART_VerticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.PART_HeaderItems == null || this.PART_TrackItems == null)
                return;

            this.PART_HeaderItems.Margin = new Thickness(0, -this.PART_VerticalScrollBar.Value, 0, 0);
            this.PART_TrackItems.Margin = new Thickness(0, -this.PART_VerticalScrollBar.Value, 0, 0);
        }
    }
}
