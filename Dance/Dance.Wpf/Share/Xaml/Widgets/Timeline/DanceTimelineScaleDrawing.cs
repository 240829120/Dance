﻿using System;
using System.Collections.Generic;
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
    /// 时间线绘制
    /// </summary>
    public class DanceTimelineScaleDrawing : Panel
    {
        /// <summary>
        /// 刻度线绘制上下文
        /// </summary>
        private class DanceTimelineScaleDrawingContext
        {
            /// <summary>
            /// 开始坐标
            /// </summary>
            public double BeginX;

            /// <summary>
            /// 结束坐标
            /// </summary>
            public double EndX;

            /// <summary>
            /// 开始时间
            /// </summary>
            public TimeSpan BeginTime;

            /// <summary>
            /// 结束时间
            /// </summary>
            public TimeSpan EndTime;

            /// <summary>
            /// 100毫秒宽度
            /// </summary>
            public double Millisecond100Width;

            /// <summary>
            /// 1秒宽度
            /// </summary>
            public double SecondWidth;

            /// <summary>
            /// 15秒宽度
            /// </summary>
            public double Second15Width;

            /// <summary>
            /// 1分钟宽度
            /// </summary>
            public double MinuteWidth;

            /// <summary>
            /// 15分钟宽度
            /// </summary>
            public double Minute15Width;

            /// <summary>
            /// 1小时宽度
            /// </summary>
            public double HourWidth;
        }

        public DanceTimelineScaleDrawing()
        {
            this.Loaded += DanceTimelineScaleDrawing_Loaded;
        }

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? OwnerTimeline = null;

        /// <summary>
        /// 刻度画笔
        /// </summary>
        private readonly Pen ScalePen = new(Brushes.Black, 1);

        /// <summary>
        /// 刻度值画笔
        /// </summary>
        private readonly Pen ScaleValuePen = new(Brushes.Black, 1);

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
            DependencyProperty.Register("ScaleValueBrush", typeof(SolidColorBrush), typeof(DanceTimelineScaleDrawing), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimelineScaleDrawing scale)
                    return;

                scale.ScaleValuePen.Brush = scale.ScaleValueBrush;
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
            DependencyProperty.Register("ScaleBrush", typeof(SolidColorBrush), typeof(DanceTimelineScaleDrawing), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimelineScaleDrawing scale)
                    return;

                scale.ScalePen.Brush = scale.ScaleBrush;
            })));

        #endregion

        // ======================================================================================================================
        // Override

        /// <summary>
        /// 渲染大小改变
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            this.InvalidateVisual();
        }

        /// <summary>
        /// 绘制
        /// </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.OwnerTimeline == null || this.OwnerTimeline.PART_HorizontalScrollBar == null || this.OwnerTimeline.ActualHeight <= 0)
                return;

            DanceTimelineScaleDrawingContext context = new()
            {
                BeginX = this.OwnerTimeline.PART_HorizontalScrollBar.Value
            };
            context.EndX = context.BeginX + this.ActualWidth;

            context.SecondWidth = DanceTimelineConstants.ONE_SECOND_DEFAULT_WIDTH * this.OwnerTimeline.Zoom;
            context.Millisecond100Width = 0.1d * context.SecondWidth;
            context.Second15Width = 15d * context.SecondWidth;
            context.MinuteWidth = 60d * context.SecondWidth;
            context.Minute15Width = 15d * context.MinuteWidth;
            context.HourWidth = 60d * context.MinuteWidth;

            context.BeginTime = TimeSpan.FromSeconds(context.BeginX / context.SecondWidth);
            context.EndTime = TimeSpan.FromSeconds(context.EndX / context.SecondWidth);
            context.EndTime = this.OwnerTimeline.Duration < context.EndTime ? this.OwnerTimeline.Duration : context.EndTime;

            this.DrawScaleHour(drawingContext, context);
            this.DrawScaleMinute15(drawingContext, context);
            this.DrawScaleMinute(drawingContext, context);
            this.DrawScaleSecond15(drawingContext, context);
            this.DrawScaleSecond(drawingContext, context);
            this.DrawScaleMillisecond100(drawingContext, context);
        }

        // ======================================================================================================================
        // Private Function

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceTimelineScaleDrawing_Loaded(object sender, RoutedEventArgs e)
        {
            this.OwnerTimeline = this.GetVisualTreeParent<DanceTimeline>();
            this.InvalidateVisual();
        }

        /// <summary>
        /// 绘制1小时
        /// </summary>
        private void DrawScaleHour(DrawingContext drawingContext, DanceTimelineScaleDrawingContext context)
        {
            if (context.HourWidth < DanceTimelineConstants.MIN_SCALE_WIDTH || this.OwnerTimeline == null)
                return;

            int lengthOffset = 15;

            for (int i = (int)context.BeginTime.TotalHours; i <= context.EndTime.TotalHours; i++)
            {
                double x = (i * context.HourWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.HourWidth > DanceTimelineConstants.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.HourWidth > DanceTimelineConstants.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromHours(i):hh\\:mm\\:ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimelineConstants.FONT_FAMILY), 12, this.ScaleValueBrush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制15分钟
        /// </summary>
        private void DrawScaleMinute15(DrawingContext drawingContext, DanceTimelineScaleDrawingContext context)
        {
            if (this.OwnerTimeline == null || context.Minute15Width < DanceTimelineConstants.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 12;

            for (int i = (int)context.BeginTime.TotalMinutes - (int)(context.BeginTime.TotalMinutes % 15); i <= context.EndTime.TotalMinutes; i += 15)
            {
                if (i % 60 == 0)
                    continue;

                double x = (i * context.MinuteWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.Minute15Width > DanceTimelineConstants.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.Minute15Width > DanceTimelineConstants.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromMinutes(i):mm\\:ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimelineConstants.FONT_FAMILY), 12, this.ScaleValueBrush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制1分钟
        /// </summary>
        private void DrawScaleMinute(DrawingContext drawingContext, DanceTimelineScaleDrawingContext context)
        {
            if (this.OwnerTimeline == null || context.Minute15Width < DanceTimelineConstants.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 10;

            for (int i = (int)context.BeginTime.TotalMinutes; i <= context.EndTime.TotalMinutes; i++)
            {
                if (i % 15 == 0)
                    continue;

                double x = (i * context.MinuteWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.Minute15Width > DanceTimelineConstants.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.Minute15Width > DanceTimelineConstants.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromMinutes(i):mm\\:ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimelineConstants.FONT_FAMILY), 12, this.ScaleValueBrush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制15秒
        /// </summary>
        private void DrawScaleSecond15(DrawingContext drawingContext, DanceTimelineScaleDrawingContext context)
        {
            if (this.OwnerTimeline == null || context.Second15Width < DanceTimelineConstants.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 8;

            for (int i = (int)context.BeginTime.TotalSeconds - (int)(context.BeginTime.TotalSeconds % 15); i <= context.EndTime.TotalSeconds; i += 15)
            {
                if (i % 60 == 0)
                    continue;

                double x = (i * context.SecondWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.Second15Width > DanceTimelineConstants.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.Second15Width > DanceTimelineConstants.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromSeconds(i):ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimelineConstants.FONT_FAMILY), 10, this.ScaleValueBrush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制1秒
        /// </summary>
        private void DrawScaleSecond(DrawingContext drawingContext, DanceTimelineScaleDrawingContext context)
        {
            if (this.OwnerTimeline == null || context.SecondWidth < DanceTimelineConstants.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 8;

            for (int i = (int)context.BeginTime.TotalSeconds; i <= context.EndTime.TotalSeconds; i++)
            {
                if (i % 15 == 0)
                    continue;

                double x = (i * context.SecondWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.SecondWidth > DanceTimelineConstants.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.SecondWidth > DanceTimelineConstants.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromSeconds(i):ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimelineConstants.FONT_FAMILY), 10, this.ScaleValueBrush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制100毫秒
        /// </summary>
        private void DrawScaleMillisecond100(DrawingContext drawingContext, DanceTimelineScaleDrawingContext context)
        {
            if (context.Millisecond100Width < DanceTimelineConstants.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 5;

            for (double i = (int)context.BeginTime.TotalSeconds; i <= context.EndTime.TotalSeconds; i += 0.1d)
            {
                if (i % 1 == 0)
                    continue;

                double x = (i * context.SecondWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                drawingContext.DrawSnappedLinesBetweenPoints(this.ScalePen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
            }
        }
    }
}