using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// line 元素
    /// </summary>
    public class DanceLine : DanceSVGDrawingElement
    {
        /// <summary>
        /// line 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceLine(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceLine(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry GetBaseGeometry()
        {
            double x1 = this.GetAttributeValue<DanceSVGDouble>("x1", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Width ?? 0) ?? 0;
            double y1 = this.GetAttributeValue<DanceSVGDouble>("y1", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Height ?? 0) ?? 0;
            double x2 = this.GetAttributeValue<DanceSVGDouble>("x2", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Width ?? 0) ?? 0;
            double y2 = this.GetAttributeValue<DanceSVGDouble>("y2", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Height ?? 0) ?? 0;

            LineGeometry geometry = new(new Point(x1, y1), new Point(x2, y2));

            return geometry;
        }
    }
}
