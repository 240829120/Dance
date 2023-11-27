using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线轨道高亮
    /// </summary>
    public class DanceTimelineTrackHighlight : Control
    {
        static DanceTimelineTrackHighlight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrackHighlight), new FrameworkPropertyMetadata(typeof(DanceTimelineTrackHighlight)));
        }

        // ===========================================================================================================
        // Field

        /// <summary>
        /// 画笔
        /// </summary>
        private readonly Pen Pen = new(Brushes.Transparent, 0);

        // ===========================================================================================================
        // Property

        #region Brush -- 画刷

        /// <summary>
        /// 画刷
        /// </summary>
        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        /// <summary>
        /// 画刷
        /// </summary>
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(DanceTimelineTrackHighlight), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

        #endregion

        #region BeginX -- 开始坐标

        /// <summary>
        /// 开始坐标
        /// </summary>
        public double? BeginX
        {
            get { return (double?)GetValue(BeginXProperty); }
            set { SetValue(BeginXProperty, value); }
        }

        /// <summary>
        /// 开始坐标
        /// </summary>
        public static readonly DependencyProperty BeginXProperty =
            DependencyProperty.Register("BeginX", typeof(double?), typeof(DanceTimelineTrackHighlight), new PropertyMetadata(null));

        #endregion

        #region EndX -- 结束坐标

        /// <summary>
        /// 结束坐标
        /// </summary>
        public double? EndX
        {
            get { return (double?)GetValue(EndXProperty); }
            set { SetValue(EndXProperty, value); }
        }

        /// <summary>
        /// 结束坐标
        /// </summary>
        public static readonly DependencyProperty EndXProperty =
            DependencyProperty.Register("EndX", typeof(double?), typeof(DanceTimelineTrackHighlight), new PropertyMetadata(null));

        #endregion

        // ===========================================================================================================
        // Override

        /// <summary>
        /// 绘制
        /// </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.BeginX == null || this.EndX == null)
                return;

            drawingContext.DrawRectangle(this.Brush, this.Pen, new Rect(this.BeginX.Value, 0, this.EndX.Value - this.BeginX.Value, this.ActualHeight));
        }
    }
}
