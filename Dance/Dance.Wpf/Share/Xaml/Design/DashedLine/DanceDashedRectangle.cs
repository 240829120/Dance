using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 虚线矩形
    /// </summary>
    public class DanceDashedRectangle : DanceDashedLineBase
    {
        #region EdgeMark -- 边掩码

        /// <summary>
        /// 边掩码
        /// </summary>
        public Thickness EdgeMark
        {
            get { return (Thickness)GetValue(EdgeMarkProperty); }
            set { SetValue(EdgeMarkProperty, value); }
        }

        /// <summary>
        /// 边掩码
        /// </summary>
        public static readonly DependencyProperty EdgeMarkProperty =
            DependencyProperty.Register("EdgeMark", typeof(Thickness), typeof(DanceDashedRectangle), new PropertyMetadata(new Thickness(1)));

        #endregion

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="drawingContext">绘制上下文</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.EdgeMark.Top > 0)
            {
                drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new(0, 0), new(this.RenderSize.Width, 0));
            }

            if (this.EdgeMark.Bottom > 0)
            {
                drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new(0, this.RenderSize.Height), new(this.RenderSize.Width, this.RenderSize.Height));
            }

            if (this.EdgeMark.Left > 0)
            {
                drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new(0, 0), new(0, this.RenderSize.Height));
            }

            if (this.EdgeMark.Right > 0)
            {
                drawingContext.DrawSnappedLinesBetweenPoints(this.StrokePen, this.StrokeThickness, new(this.RenderSize.Width, 0), new(this.RenderSize.Width, this.RenderSize.Height));
            }
        }
    }
}