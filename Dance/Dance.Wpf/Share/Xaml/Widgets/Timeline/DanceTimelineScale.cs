using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线刻度
    /// </summary>
    [TemplatePart(Name = nameof(PART_Drawing), Type = typeof(DanceTimelineScaleDrawing))]
    public class DanceTimelineScale : Control
    {
        static DanceTimelineScale()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineScale), new FrameworkPropertyMetadata(typeof(DanceTimelineScale)));
        }

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? OwnerTimeline = null;

        /// <summary>
        /// 绘制
        /// </summary>
        private DanceTimelineScaleDrawing? PART_Drawing;

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
            DependencyProperty.Register("ScaleValueBrush", typeof(SolidColorBrush), typeof(DanceTimelineScale), new PropertyMetadata(Brushes.Black));

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
            DependencyProperty.Register("ScaleBrush", typeof(SolidColorBrush), typeof(DanceTimelineScale), new PropertyMetadata(Brushes.Black));

        #endregion

        // ======================================================================================================================
        // Override

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Drawing = this.Template.FindName(nameof(PART_Drawing), this) as DanceTimelineScaleDrawing;
            this.OwnerTimeline = this.GetVisualTreeParent<DanceTimeline>();
        }

        /// <summary>
        /// 鼠标左键点击
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (this.OwnerTimeline == null || this.OwnerTimeline.IsPlaying || this.OwnerTimeline.PART_HorizontalScrollBar == null)
                return;

            Point point = e.GetPosition(this);
            if (point.X < 0 || point.X > this.ActualWidth || point.Y < 0 || point.Y > this.ActualHeight)
                return;

            TimeSpan offset = this.OwnerTimeline.GetTimeSpanFromPixel(point.X);
            TimeSpan begin = this.OwnerTimeline.GetTimeSpanFromPixel(this.OwnerTimeline.PART_HorizontalScrollBar.Value);
            TimeSpan dest = begin + offset;
            this.OwnerTimeline.CurrentTime = this.OwnerTimeline.GetEffectiveTimeSpan(dest);
            this.OwnerTimeline.PART_Progress?.TryBeginDrag(e);
        }

        // ======================================================================================================================
        // Public Function

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            this.PART_Drawing?.InvalidateVisual();
        }
    }
}