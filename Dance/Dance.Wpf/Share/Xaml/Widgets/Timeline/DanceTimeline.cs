using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

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
    [TemplatePart(Name = nameof(PART_FrameSelect), Type = typeof(DanceFrameSelect))]
    public class DanceTimeline : ItemsControl
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(DanceTimeline));

        static DanceTimeline()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimeline), new FrameworkPropertyMetadata(typeof(DanceTimeline)));
        }

        public DanceTimeline()
        {
            this.Loaded += DanceTimeline_Loaded;
            this.Unloaded += DanceTimeline_Unloaded;
            this.IsVisibleChanged += DanceTimeline_IsVisibleChanged;

            this.Tools.Add(new DanceTimelineTrackTool(this));
            this.Tools.Add(new DanceTimelineZoomTool(this));
            this.Tools.Add(new DanceTimelineMoveTool(this));
            this.Tools.Add(new DanceTimelineMoveElementTool(this));
            this.Tools.Add(new DanceTimelineResizeElementTool(this));
            this.Tools.Add(new DanceTimelineFrameSelectTool(this));
            this.Tools.Add(new DanceTimelineCopyMoveTool(this));
        }

        // ==========================================================================================================================================
        // Field

        // -----------------------------------------------------------------------------
        // PART

        /// <summary>
        /// 垂直滚动条
        /// </summary>
        internal ScrollBar? PART_VerticalScrollBar;

        /// <summary>
        /// 水平滚动条
        /// </summary>
        internal ScrollBar? PART_HorizontalScrollBar;

        /// <summary>
        /// 刻度
        /// </summary>
        internal DanceTimelineScale? PART_Scale;

        /// <summary>
        /// 进度
        /// </summary>
        internal DanceTimelineProgress? PART_Progress;

        /// <summary>
        /// 头部集合
        /// </summary>
        internal DanceTimelineTrackHeaderItems? PART_HeaderItems;

        /// <summary>
        /// 轨道集合
        /// </summary>
        internal ItemsPresenter? PART_TrackItems;

        /// <summary>
        /// 框选控件
        /// </summary>
        internal DanceFrameSelect? PART_FrameSelect;

        // -----------------------------------------------------------------------------
        // Tool

        /// <summary>
        /// 工具集合
        /// </summary>
        internal List<DanceTimelineToolBase> Tools = new();

        // -----------------------------------------------------------------------------
        // Field

        /// <summary>
        /// 上一次更新时间
        /// </summary>
        private DateTime? LastUpdateTime;

        // ==========================================================================================================================================
        // Event

        /// <summary>
        /// 轨道选择改变时触发
        /// </summary>
        public event EventHandler<DanceTimelineTrackSelectionChangedEventArgs>? TrackSelectionChanged;

        /// <summary>
        /// 元素选择改变时触发
        /// </summary>
        public event EventHandler<DanceTimelineElementSelectionChangedEventArgs>? ElementSelectionChanged;

        /// <summary>
        /// 元素开始拖拽时触发
        /// </summary>
        public event EventHandler<DanceTimelineElementDragBeginEventArgs>? ElementDragBegin;

        /// <summary>
        /// 元素拖拽经过时触发
        /// </summary>
        public event EventHandler<DanceTimelineElementDragEventArgs>? ElementDragOver;

        /// <summary>
        /// 元素拖拽放置时触发
        /// </summary>
        public event EventHandler<DanceTimelineElementDragEventArgs>? ElementDrop;

        // ==========================================================================================================================================
        // Property

        // ---------------------------------------------------------------------
        // Style

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

        #region ElementTemplate -- 元素模板

        /// <summary>
        /// 元素模板
        /// </summary>
        public DataTemplate ElementTemplate
        {
            get { return (DataTemplate)GetValue(ElementTemplateProperty); }
            set { SetValue(ElementTemplateProperty, value); }
        }

        /// <summary>
        /// 元素模板
        /// </summary>
        public static readonly DependencyProperty ElementTemplateProperty =
            DependencyProperty.Register("ElementTemplate", typeof(DataTemplate), typeof(DanceTimeline), new PropertyMetadata(null));

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
            DependencyProperty.Register("ProgressWidth", typeof(double), typeof(DanceTimeline), new PropertyMetadata(10d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimeline timeline)
                    return;

                timeline.Update();
            })));

        #endregion

        #region ScaleBrush -- 刻度画刷

        /// <summary>
        /// 刻度画刷
        /// </summary>
        public SolidColorBrush ScaleBrush
        {
            get { return (SolidColorBrush)GetValue(ScaleBrushProperty); }
            set { SetValue(ScaleBrushProperty, value); }
        }

        /// <summary>
        /// 刻度画刷
        /// </summary>
        public static readonly DependencyProperty ScaleBrushProperty =
            DependencyProperty.Register("ScaleBrush", typeof(SolidColorBrush), typeof(DanceTimeline), new PropertyMetadata(Brushes.Black));

        #endregion

        #region ScaleValueBrush -- 刻度值画刷

        /// <summary>
        /// 刻度值画刷
        /// </summary>
        public SolidColorBrush ScaleValueBrush
        {
            get { return (SolidColorBrush)GetValue(ScaleValueBrushProperty); }
            set { SetValue(ScaleValueBrushProperty, value); }
        }

        /// <summary>
        /// 刻度值画刷
        /// </summary>
        public static readonly DependencyProperty ScaleValueBrushProperty =
            DependencyProperty.Register("ScaleValueBrush", typeof(SolidColorBrush), typeof(DanceTimeline), new PropertyMetadata(Brushes.Black));

        #endregion

        #region HighlightBrush -- 高亮画刷

        /// <summary>
        /// 高亮画刷
        /// </summary>
        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }

        /// <summary>
        /// 高亮画刷
        /// </summary>
        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(DanceTimeline), new PropertyMetadata(Brushes.Red));

        #endregion

        // ---------------------------------------------------------------------
        // Controller

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
                    TimeSpan destTime = timeline.CurrentTime - timeline.GetTimeSpanFromPixel(timeline.PART_TrackItems.ActualWidth / 2d);
                    destTime = timeline.GetEffectiveTimeSpan(destTime);

                    timeline.PART_HorizontalScrollBar.Value = timeline.GetPixelFromTimeSpan(destTime);
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
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(DanceTimeline), new PropertyMetadata(TimeSpan.FromHours(1), new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimeline timeline)
                    return;

                timeline.Update();
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
                if (s is not DanceTimeline timeline)
                    return;

                timeline.Update();
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
            DependencyProperty.Register("TrackHeight", typeof(double), typeof(DanceTimeline), new PropertyMetadata(40d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimeline timeline)
                    return;

                timeline.Update();
            })));

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

        #region IsReadOnly -- 是否只读

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DanceTimeline), new PropertyMetadata(false));

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

        #region Status -- 状态

        /// <summary>
        /// 状态
        /// </summary>
        public DanceTimelineStatus Status
        {
            get { return (DanceTimelineStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(DanceTimelineStatus), typeof(DanceTimeline), new PropertyMetadata(DanceTimelineStatus.FrameSelect));

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
            this.PART_FrameSelect = this.Template.FindName(nameof(PART_FrameSelect), this) as DanceFrameSelect;

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
        // Public Function

        /// <summary>
        /// 获取选中的元素集合
        /// </summary>
        /// <returns>选中的元素集合</returns>
        public List<DanceTimelineElement> GetSelectedElements()
        {
            List<DanceTimelineElement> result = new();

            DanceTimelineFrameSelectTool? tool = this.GetTool<DanceTimelineFrameSelectTool>();
            if (tool == null)
                return result;

            result.AddRange(tool.Selection.Select(p => p.Element));

            return result;
        }

        /// <summary>
        /// 获取工具
        /// </summary>
        /// <typeparam name="T">工具类型</typeparam>
        /// <returns>工具</returns>
        public T? GetTool<T>() where T : DanceTimelineToolBase
        {
            return this.Tools.FirstOrDefault(p => p is T) as T;
        }

        /// <summary>
        /// 更新选择区域，在修改元素选择之后调用更新选择区域的状态
        /// </summary>
        public void UpdateElementSelection()
        {
            DanceTimelineFrameSelectTool? selectTool = this.GetTool<DanceTimelineFrameSelectTool>();
            if (selectTool == null)
                return;

            selectTool.UpdateSelection();
        }

        /// <summary>
        /// 清理轨道选择
        /// </summary>
        public void ClearTrackSelection()
        {
            DanceTimelineTrackTool? trackTool = this.GetTool<DanceTimelineTrackTool>();
            if (trackTool == null)
                return;

            trackTool.SelectTrack(-1);
        }

        /// <summary>
        /// 清理元素选择
        /// </summary>
        public void ClearElementSelection()
        {
            DanceTimelineFrameSelectTool? selectTool = this.GetTool<DanceTimelineFrameSelectTool>();
            if (selectTool == null)
                return;

            selectTool.ClearSelection();
        }

        /// <summary>
        /// 获取视窗宽度
        /// </summary>
        /// <returns>视窗宽度</returns>
        public TimeSpan GetViewportWidth()
        {
            if (this.PART_FrameSelect == null)
                return TimeSpan.Zero;

            return this.GetTimeSpanFromPixel(this.PART_FrameSelect.ActualWidth);
        }

        /// <summary>
        /// 滚动到时间刻度
        /// </summary>
        /// <param name="time">时间</param>
        public void ScrollTo(TimeSpan time)
        {
            if (this.PART_HorizontalScrollBar == null || this.PART_FrameSelect == null)
                return;

            time = time < TimeSpan.Zero ? TimeSpan.Zero : time;
            time = time > this.Duration ? this.Duration : time;

            double x = this.GetPixelFromTimeSpan(time);
            double dest = x - this.PART_FrameSelect.ActualWidth / 2d;
            this.PART_HorizontalScrollBar.Value = Math.Max(0, dest);
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
            return timeSpan.TotalSeconds * DanceTimelineConstants.ONE_SECOND_DEFAULT_WIDTH * this.Zoom;
        }

        /// <summary>
        /// 根据像素获取时间间隔
        /// </summary>
        /// <param name="pixel">像素</param>
        /// <returns>时间间隔</returns>
        internal TimeSpan GetTimeSpanFromPixel(double pixel)
        {
            return TimeSpan.FromSeconds(pixel / (DanceTimelineConstants.ONE_SECOND_DEFAULT_WIDTH * this.Zoom));
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
            dest = TimeSpan.FromSeconds((int)(dest.TotalSeconds * 10) / 10d);

            return dest;
        }

        /// <summary>
        /// 触发轨道选择改变事件
        /// </summary>
        /// <param name="selectedTrack">当前选中的轨道</param>
        internal void InvokeTrackSelectionChanged(DanceTimelineTrack? selectedTrack)
        {
            try
            {
                this.TrackSelectionChanged?.Invoke(this, new DanceTimelineTrackSelectionChangedEventArgs(this, selectedTrack));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 触发元素选择改变事件
        /// </summary>
        internal void InvokeElementSelectionChanged()
        {
            try
            {
                this.ElementSelectionChanged?.Invoke(this, new DanceTimelineElementSelectionChangedEventArgs(this, this.GetSelectedElements()));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 执行元素拖拽开始事件
        /// </summary>
        internal void InvokeElementDragBegin(DanceTimelineElementDragBeginEventArgs e)
        {
            try
            {
                this.ElementDragBegin?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 执行元素拖拽经过事件
        /// </summary>
        internal void InvokeElementDragOver(DanceTimelineElementDragEventArgs e)
        {
            try
            {
                this.ElementDragOver?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 执行元素拖拽结束事件
        /// </summary>
        internal void InvokeElementDrop(DanceTimelineElementDragEventArgs e)
        {
            try
            {
                this.ElementDrop?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        internal void Update()
        {
            if (this.PART_HorizontalScrollBar == null || this.PART_VerticalScrollBar == null || this.PART_Scale == null || this.PART_HeaderItems == null || this.PART_Progress == null)
                return;

            this.PART_VerticalScrollBar.Maximum = this.Items.Count * this.TrackHeight;
            this.PART_HorizontalScrollBar.Maximum = this.Duration.TotalSeconds * DanceTimelineConstants.ONE_SECOND_DEFAULT_WIDTH * this.Zoom;
            this.PART_HorizontalScrollBar.ViewportSize = this.PART_HorizontalScrollBar.Maximum * 0.1d;
            double left = this.PART_HorizontalScrollBar.Value - this.GetPixelFromTimeSpan(this.CurrentTime);
            this.PART_Progress.Margin = new Thickness(-left - this.ProgressWidth / 2d, 0, 0, 0);
            this.PART_Progress.Visibility = (-left < 0 || left > this.ProgressWidth) ? Visibility.Collapsed : Visibility.Visible;
            this.PART_Scale.Update();
            DanceXamlExpansion.GetVisualTreeDescendants<DanceTimelineTrackPanel>(this)?.ForEach(p => p.InvalidateVisual());
            DanceXamlExpansion.GetVisualTreeDescendants<DanceTimelineTrackHeaderPanel>(this)?.ForEach(p => p.InvalidateVisual());
        }

        // ==========================================================================================================================================
        // Override

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            this.Focus();

            base.OnMouseDown(e);
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 按键抬起
        /// </summary>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            this.Status = DanceTimelineStatus.FrameSelect;
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            this.Status = DanceTimelineStatus.FrameSelect;
            this.Cursor = Cursors.Arrow;
        }

        // ==========================================================================================================================================
        // Private Function

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
        /// 可见性改变
        /// </summary>
        private void DanceTimeline_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Status = DanceTimelineStatus.FrameSelect;
            this.Cursor = Cursors.Arrow;
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

            TimeSpan dest = this.CurrentTime + offset;
            dest = dest < TimeSpan.Zero ? TimeSpan.Zero : dest;
            dest = dest > this.Duration ? this.Duration : dest;

            this.CurrentTime = dest;
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
            this.Update();
        }
    }
}