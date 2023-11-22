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
    /// 框选
    /// </summary>
    public class DanceFrameSelect : DanceDashedLineBase
    {
        // ===========================================================================================================================
        // Field

        // ===========================================================================================================================
        // Property

        #region BeginPoint -- 开始坐标点

        /// <summary>
        /// 开始坐标点
        /// </summary>
        public Point? BeginPoint
        {
            get { return (Point?)GetValue(BeginPointProperty); }
            set { SetValue(BeginPointProperty, value); }
        }

        /// <summary>
        /// 开始坐标点
        /// </summary>
        public static readonly DependencyProperty BeginPointProperty =
            DependencyProperty.Register("BeginPoint", typeof(Point?), typeof(DanceFrameSelect), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceFrameSelect select)
                    return;

                select.InvalidateVisual();
            })));

        #endregion

        #region EndPoint -- 结束坐标点

        /// <summary>
        /// 结束坐标点
        /// </summary>
        public Point? EndPoint
        {
            get { return (Point?)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        /// <summary>
        /// 结束坐标点
        /// </summary>
        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register("EndPoint", typeof(Point?), typeof(DanceFrameSelect), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceFrameSelect select)
                    return;

                select.InvalidateVisual();
            })));

        #endregion

        // ===========================================================================================================================
        // Public Function

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.BeginPoint == null || this.EndPoint == null)
                return;

            double left = Math.Min(this.BeginPoint.Value.X, this.EndPoint.Value.X);
            double right = Math.Max(this.BeginPoint.Value.X, this.EndPoint.Value.X);
            double top = Math.Min(this.BeginPoint.Value.Y, this.EndPoint.Value.Y);
            double bottom = Math.Max(this.BeginPoint.Value.Y, this.EndPoint.Value.Y);

            drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new Point(left, top), new Point(right, top));
            drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new Point(right, top), new Point(right, bottom));
            drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new Point(right, bottom), new Point(left, bottom));
            drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new Point(left, bottom), new Point(left, top));
        }
    }
}
