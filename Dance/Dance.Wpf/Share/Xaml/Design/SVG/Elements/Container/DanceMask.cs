using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// marsk 元素
    /// </summary>
    public class DanceMask : DanceSVGElement, IDanceSVGMask
    {
        /// <summary>
        /// marsk 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceMask(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceMask(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取蒙版
        /// </summary>
        /// <returns>蒙版</returns>
        private DrawingBrush? GetOpacityMask()
        {
            if (this.Children.Count == 0)
                return null;

            DrawingGroup group = new();

            foreach (DanceSVGElement item in this.Children)
            {
                Drawing? drawing = null;

                if (item is DanceSVGDrawingContainerElement drawingContainerElement)
                {
                    drawing = drawingContainerElement.GetDrawing();
                }
                else if (item is DanceSVGDrawingElement drawingElement)
                {
                    drawing = drawingElement.GetDrawing();
                }

                if (drawing != null)
                {
                    group.Children.Add(drawing);
                }
            }

            if (group.Children.Count == 0)
                return null;

            foreach (Drawing drawing in group.Children)
            {
                ConvertColors(drawing);
            }

            DrawingBrush result = new(group);

            DanceSVGGradientUnits? mask_units = this.GetAttributeValue<DanceSVGGradientUnits>("maskUnits");

            if (mask_units != null && mask_units == DanceSVGGradientUnits.UserSpaceOnUse)
            {
                result.ViewportUnits = mask_units.Value;
                result.Viewport = group.Bounds;
            }

            return result;
        }

        private DrawingBrush? _value;

        /// <summary>
        /// 剪切路径
        /// </summary>
        public DrawingBrush? Value
        {
            get
            {
                this._value ??= this.GetOpacityMask();

                return this._value;
            }
        }

        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="color">颜色</param>
        private static Color ConvertColor(Color color)
        {
            float a = (float)(0.2125 * color.ScR + 0.7154 * color.ScG + 0.0721 * color.ScB) * color.ScA;

            return Color.FromScRgb(a, 0, 0, 0);
        }

        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="brush">画刷</param>
        private static void ConvertColors(Brush brush)
        {
            if (brush == null)
                return;

            if (brush is SolidColorBrush solidColorBrush)
            {
                solidColorBrush.Color = ConvertColor(solidColorBrush.Color);
            }
            else if (brush is GradientBrush gradientBrush)
            {
                foreach (GradientStop stop in gradientBrush.GradientStops)
                {
                    stop.Color = ConvertColor(stop.Color);
                }
            }
            else if (brush is DrawingBrush drawingBrush)
            {
                ConvertColors(drawingBrush.Drawing);
            }
        }

        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="pen">画笔</param>
        private static void ConvertColors(Pen pen)
        {
            if (pen == null)
                return;

            ConvertColors(pen.Brush);
        }

        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="drawing">画刷</param>
        private static void ConvertColors(Drawing drawing)
        {
            if (drawing is DrawingGroup drawing_group)
            {
                foreach (Drawing item in drawing_group.Children)
                {
                    ConvertColors(item);
                }
            }
            else if (drawing is GeometryDrawing geometry_drawing)
            {
                ConvertColors(geometry_drawing.Brush);
                ConvertColors(geometry_drawing.Pen);
            }
        }
    }
}
