using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 虚线
    /// </summary>
    public class DanceDashedLine : DanceDashedLineBase
    {
        #region Orientation -- 方向

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// 方向
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DanceDashedLine), new PropertyMetadata(Orientation.Horizontal));

        #endregion

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="drawingContext">绘制上下文</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            switch (this.Orientation)
            {
                case Orientation.Horizontal:
                    drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new(0, this.RenderSize.Height / 2d), new(this.RenderSize.Width, this.RenderSize.Height / 2d));

                    break;

                case Orientation.Vertical:
                    drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new(this.RenderSize.Width / 2d, 0), new(this.RenderSize.Width / 2d, this.RenderSize.Height));
                    break;

                default: break;
            }

        }
    }
}
