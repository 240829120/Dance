using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// SVG可渲染容器元素
    /// </summary>
    public abstract class DanceSVGDrawingContainerElement : DanceSVGDrawingElement
    {
        /// <summary>
        /// 元素
        /// </summary>
        /// <param name="svg">根元素</param>
        /// <param name="parent">父级元素</param>
        /// <param name="element">当前元素</param>
        public DanceSVGDrawingContainerElement(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry? GetBaseGeometry()
        {
            return null;
        }

        /// <summary>
        /// 获取基础渲染
        /// </summary>
        /// <returns>基础渲染</returns>
        protected override Drawing? GetBaseDrawing()
        {
            double opacity = this.GetAttributeValue<DanceSVGDouble>("opacity", false, DanceSVGDouble.One)?.GetValue(1) ?? 1;
            DanceSVGTransform? transform = this.GetAttributeValue<DanceSVGTransform>("transform");

            DrawingGroup result = new()
            {
                Opacity = opacity
            };

            if (transform != null && transform.Value != null && transform.Value.Count > 0)
            {
                TransformGroup group = new();

                foreach (Transform item in transform.Value)
                {
                    group.Children.Add(item);
                }

                result.Transform = group;
            }

            foreach (DanceSVGElement element in this.Children)
            {
                Drawing? drawing = null;

                if (element is DanceSVGDrawingContainerElement drawingContainerElement)
                {
                    drawing = drawingContainerElement.GetDrawing();
                }
                else if (element is DanceSVGDrawingElement drawingElement)
                {
                    drawing = drawingElement.GetDrawing();
                }

                if (drawing != null)
                {
                    result.Children.Add(drawing);
                }
            }

            return result;
        }
    }
}
