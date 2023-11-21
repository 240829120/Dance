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
    public class DanceTimelineScale : Border
    {
        /// <summary>
        /// 刻度线绘制上下文
        /// </summary>
        private class DanceTimelineScaleDrawContext
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

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        [NotNull]
        internal DanceTimeline? OwnerTimeline = null;

        /// <summary>
        /// 画笔
        /// </summary>
        private readonly Pen Pen = new(Brushes.Black, 1);

        /// <summary>
        /// 画刷
        /// </summary>
        private readonly Brush Brush = Brushes.Black;

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

            DanceTimelineScaleDrawContext context = new();

            context.BeginX = this.OwnerTimeline.PART_HorizontalScrollBar.Value;
            context.EndX = context.BeginX + this.ActualWidth;

            context.SecondWidth = DanceTimeline.ONE_SECOND_DEFAULT_WIDTH * this.OwnerTimeline.Zoom;
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

        /// <summary>
        /// 鼠标左键点击
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (this.OwnerTimeline == null || this.OwnerTimeline.IsPlaying || this.OwnerTimeline.PART_HorizontalScrollBar == null)
                return;

            Point point = e.GetPosition(this);
            if (point.X < 0 || point.X > this.ActualWidth)
                return;

            TimeSpan offset = this.OwnerTimeline.GetTimeSpanFromPixel(point.X);
            TimeSpan begin = this.OwnerTimeline.GetTimeSpanFromPixel(this.OwnerTimeline.PART_HorizontalScrollBar.Value);
            TimeSpan dest = begin + offset;
            this.OwnerTimeline.CurrentTime = this.OwnerTimeline.GetEffectiveTimeSpan(dest);
            this.OwnerTimeline.PART_Progress?.TryBeginDrag(e);
        }

        // ======================================================================================================================
        // Private Function

        /// <summary>
        /// 绘制1小时
        /// </summary>
        private void DrawScaleHour(DrawingContext drawingContext, DanceTimelineScaleDrawContext context)
        {
            if (context.HourWidth < DanceTimeline.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 15;

            for (int i = (int)context.BeginTime.TotalHours; i <= context.EndTime.TotalHours; i++)
            {
                double x = (i * context.HourWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.HourWidth > DanceTimeline.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.HourWidth > DanceTimeline.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromHours(i):hh\\:mm\\:ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 12, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制15分钟
        /// </summary>
        private void DrawScaleMinute15(DrawingContext drawingContext, DanceTimelineScaleDrawContext context)
        {
            if (context.Minute15Width < DanceTimeline.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 12;

            for (int i = (int)context.BeginTime.TotalMinutes - (int)(context.BeginTime.TotalMinutes % 15); i <= context.EndTime.TotalMinutes; i += 15)
            {
                if (i % 60 == 0)
                    continue;

                double x = (i * context.MinuteWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.Minute15Width > DanceTimeline.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.Minute15Width > DanceTimeline.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromMinutes(i):mm\\:ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 12, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制1分钟
        /// </summary>
        private void DrawScaleMinute(DrawingContext drawingContext, DanceTimelineScaleDrawContext context)
        {
            if (context.Minute15Width < DanceTimeline.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 10;

            for (int i = (int)context.BeginTime.TotalMinutes; i <= context.EndTime.TotalMinutes; i++)
            {
                if (i % 15 == 0)
                    continue;

                double x = (i * context.MinuteWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.Minute15Width > DanceTimeline.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.Minute15Width > DanceTimeline.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromMinutes(i):mm\\:ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 12, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制15秒
        /// </summary>
        private void DrawScaleSecond15(DrawingContext drawingContext, DanceTimelineScaleDrawContext context)
        {
            if (context.Second15Width < DanceTimeline.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 8;

            for (int i = (int)context.BeginTime.TotalSeconds - (int)(context.BeginTime.TotalSeconds % 15); i <= context.EndTime.TotalSeconds; i += 15)
            {
                if (i % 60 == 0)
                    continue;

                double x = (i * context.SecondWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.Second15Width > DanceTimeline.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.Second15Width > DanceTimeline.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromSeconds(i):ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 10, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制1秒
        /// </summary>
        private void DrawScaleSecond(DrawingContext drawingContext, DanceTimelineScaleDrawContext context)
        {
            if (context.SecondWidth < DanceTimeline.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 8;

            for (int i = (int)context.BeginTime.TotalSeconds; i <= context.EndTime.TotalSeconds; i++)
            {
                if (i % 15 == 0)
                    continue;

                double x = (i * context.SecondWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                if (context.SecondWidth > DanceTimeline.MIN_LARGE_SCALE_WIDTH)
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.OwnerTimeline.ActualHeight));
                }
                else
                {
                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
                }

                if (context.SecondWidth > DanceTimeline.MIN_NUMBER_WIDTH)
                {
                    FormattedText txt = new($"{TimeSpan.FromSeconds(i):ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 10, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x - txt.Width / 2d, this.ActualHeight - lengthOffset - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 绘制100毫秒
        /// </summary>
        private void DrawScaleMillisecond100(DrawingContext drawingContext, DanceTimelineScaleDrawContext context)
        {
            if (context.Millisecond100Width < DanceTimeline.MIN_SCALE_WIDTH)
                return;

            int lengthOffset = 5;

            for (double i = (int)context.BeginTime.TotalSeconds; i <= context.EndTime.TotalSeconds; i += 0.1d)
            {
                if (i % 1 == 0)
                    continue;

                double x = (i * context.SecondWidth) - context.BeginX;
                if (x < 0 || x > this.ActualWidth)
                    continue;

                drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x, this.ActualHeight - lengthOffset), new Point(x, this.ActualHeight));
            }
        }
    }
}
