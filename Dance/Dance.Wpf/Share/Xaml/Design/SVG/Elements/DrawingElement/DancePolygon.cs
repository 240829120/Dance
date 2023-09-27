using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// polygon 元素
    /// </summary>
    public class DancePolygon : DanceSVGDrawingElement
    {
        /// <summary>
        /// polygon 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DancePolygon(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DancePolygon(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry? GetBaseGeometry()
        {
            DanceSVGPointArray? points = this.GetAttributeValue<DanceSVGPointArray>("points");

            if (points == null || points.Value == null || points.Value.Count == 0)
                return null;

            PathFigure figure = new()
            {
                StartPoint = points.Value[0],
                IsClosed = true,
                IsFilled = true
            };

            for (int i = 1; i < points.Value.Count; ++i)
            {
                figure.Segments.Add(new LineSegment(points.Value[i], true));
            }

            PathGeometry geometry = new();
            geometry.Figures.Add(figure);

            return geometry;
        }
    }
}
