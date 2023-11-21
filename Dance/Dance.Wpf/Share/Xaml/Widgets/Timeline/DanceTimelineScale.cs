using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? Timeline;

        /// <summary>
        /// 所属滚动条
        /// </summary>
        internal ScrollViewer? ScrollViewer;

        /// <summary>
        /// 画刷
        /// </summary>
        internal Brush Brush = Brushes.Black;

        /// <summary>
        /// 画笔
        /// </summary>
        internal Pen Pen = new(Brushes.Black, 1);

        #region ScaleHeight -- 刻度高度

        /// <summary>
        /// 刻度高度
        /// </summary>
        public double ScaleHeight
        {
            get { return (double)GetValue(ScaleHeightProperty); }
            set { SetValue(ScaleHeightProperty, value); }
        }

        /// <summary>
        /// 刻度高度
        /// </summary>
        public static readonly DependencyProperty ScaleHeightProperty =
            DependencyProperty.Register("ScaleHeight", typeof(double), typeof(DanceTimelineScale), new PropertyMetadata(40d));

        #endregion

        // ======================================================================================================================
        // Override

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            this.TryGetOwner();

            this.DrawScale(drawingContext);
            this.DrawScaleNumber(drawingContext);
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (this.Timeline == null)
                return;

            Point ponint = e.GetPosition(this);

            TimeSpan dest = TimeSpan.FromHours(ponint.X / (DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Zoom));
            dest = dest > this.Timeline.Duration ? this.Timeline.Duration : dest;
            dest = dest < TimeSpan.Zero ? TimeSpan.Zero : dest;

            this.Timeline.CurrentTime = dest;
        }

        // ======================================================================================================================
        // Private Function

        /// <summary>
        /// 绘制刻度
        /// </summary>
        /// <param name="drawingContext">绘制上下文</param>
        private void DrawScale(DrawingContext drawingContext)
        {
            if (this.Timeline == null || this.ScrollViewer == null)
                return;

            double beginX = this.ScrollViewer.HorizontalOffset;
            double endX = beginX + this.ScrollViewer.ViewportWidth;

            double hourWidth = (long)(DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Zoom);
            double minuteWidth = hourWidth / 60;
            double secondWidth = minuteWidth / 60;
            double millisecond100Width = secondWidth / 10;

            TimeSpan beginTime = TimeSpan.FromHours(beginX / hourWidth);
            TimeSpan endTime = TimeSpan.FromHours(endX / hourWidth);
            endTime = this.Timeline.Duration < endTime ? this.Timeline.Duration : endTime;

            // 小时
            if (hourWidth >= DanceTimeline.MIN_SCALE_WIDTH)
            {
                for (int i = (int)beginTime.TotalHours; i <= endTime.TotalHours; i++)
                {
                    double x = (i * hourWidth) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x + beginX, this.ScaleHeight - 15), new Point(x + beginX, this.ScaleHeight));
                }
            }

            // 分钟
            if (minuteWidth >= DanceTimeline.MIN_SCALE_WIDTH)
            {
                for (int i = (int)beginTime.TotalMinutes; i <= endTime.TotalMinutes; i++)
                {
                    double x = (i * minuteWidth) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x + beginX, this.ScaleHeight - 10), new Point(x + beginX, this.ScaleHeight));
                }
            }

            // 15秒
            if (secondWidth * 15 >= DanceTimeline.MIN_NUMBER_WIDTH)
            {
                for (int i = (int)beginTime.TotalSeconds - ((int)beginTime.TotalSeconds % 15); i <= endTime.TotalSeconds; i += 15)
                {
                    if (i % 60 == 0)
                        continue;

                    double x = (i * secondWidth) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x + beginX, this.ScaleHeight - 8), new Point(x + beginX, this.ScaleHeight));
                }
            }

            // 秒
            if (secondWidth >= DanceTimeline.MIN_SCALE_WIDTH)
            {
                for (int i = (int)beginTime.TotalSeconds; i <= endTime.TotalSeconds; i++)
                {
                    if (i % 60 == 0 || i % 15 == 0)
                        continue;

                    double x = (i * secondWidth) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x + beginX, this.ScaleHeight - 8), new Point(x + beginX, this.ScaleHeight));
                }
            }

            // 0.1秒
            if (millisecond100Width >= DanceTimeline.MIN_SCALE_WIDTH)
            {
                for (int i = (int)(beginTime.TotalMilliseconds / 100); i <= (endTime.TotalMilliseconds / 100); i++)
                {
                    double x = (i * millisecond100Width) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    drawingContext.DrawSnappedLinesBetweenPoints(this.Pen, 1, new Point(x + beginX, this.ScaleHeight - 6), new Point(x + beginX, this.ScaleHeight));
                }
            }
        }

        /// <summary>
        /// 绘制秒数字
        /// </summary>
        private void DrawScaleNumber(DrawingContext drawingContext)
        {
            if (this.Timeline == null || this.ScrollViewer == null)
                return;

            double beginX = this.ScrollViewer.HorizontalOffset;
            double endX = beginX + this.ScrollViewer.ViewportWidth;

            double hourWidth = (long)(DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Zoom);
            double minuteWidth = hourWidth / 60;
            double secondWidth = minuteWidth / 60;

            TimeSpan beginTime = TimeSpan.FromHours(beginX / hourWidth);
            TimeSpan endTime = TimeSpan.FromHours(endX / hourWidth);
            endTime = this.Timeline.Duration < endTime ? this.Timeline.Duration : endTime;

            // 小时
            if (hourWidth >= DanceTimeline.MIN_NUMBER_WIDTH)
            {
                for (int i = (int)beginTime.TotalHours; i <= endTime.TotalHours; i++)
                {
                    double x = (i * hourWidth) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    FormattedText txt = new($"{TimeSpan.FromHours(i):hh\\:mm\\:ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 10, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x + beginX - txt.Width / 2d, this.ScaleHeight - 15 - txt.Height - 5));
                }
            }

            // 分钟
            if (minuteWidth >= DanceTimeline.MIN_NUMBER_WIDTH)
            {
                for (int i = (int)beginTime.TotalMinutes; i <= endTime.TotalMinutes; i++)
                {
                    if (i % 60 == 0)
                        continue;

                    double x = (i * minuteWidth) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    FormattedText txt = new($"{TimeSpan.FromMinutes(i):mm\\:ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 10, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x + beginX - txt.Width / 2d, this.ScaleHeight - 10 - txt.Height - 5));
                }
            }

            // 15秒
            if (secondWidth * 15 >= DanceTimeline.MIN_NUMBER_WIDTH)
            {
                for (int i = (int)beginTime.TotalSeconds - ((int)beginTime.TotalSeconds % 15); i <= endTime.TotalSeconds; i += 15)
                {
                    if (i % 60 == 0)
                        continue;

                    double x = (i * secondWidth) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    FormattedText txt = new($"{TimeSpan.FromSeconds(i):ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 10, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x + beginX - txt.Width / 2d, this.ScaleHeight - 8 - txt.Height - 5));
                }
            }

            // 秒
            if (secondWidth >= DanceTimeline.MIN_NUMBER_WIDTH)
            {
                for (int i = (int)beginTime.TotalSeconds; i <= endTime.TotalSeconds; i++)
                {
                    if (i % 60 == 0 || i % 15 == 0)
                        continue;

                    double x = (i * secondWidth) - beginX;
                    if (x < 0 || x > this.ScrollViewer.ViewportWidth)
                        continue;

                    FormattedText txt = new($"{TimeSpan.FromSeconds(i):ss}", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(DanceTimeline.FONT_FAMILY), 10, this.Brush, DanceXamlExpansion.DpiScale.DpiScaleX);
                    drawingContext.DrawText(txt, new Point(x + beginX - txt.Width / 2d, this.ScaleHeight - 8 - txt.Height - 5));
                }
            }
        }

        /// <summary>
        /// 尝试获取所属控件
        /// </summary>
        private void TryGetOwner()
        {
            this.Timeline ??= DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);

            if (this.ScrollViewer == null)
            {
                this.ScrollViewer = DanceXamlExpansion.GetVisualTreeParent<ScrollViewer>(this);
                if (this.ScrollViewer != null)
                {
                    this.ScrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
                    this.ScrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                }
            }
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0)
            {
                this.Timeline?.PART_Scale?.InvalidateVisual();
            }
        }

    }
}
