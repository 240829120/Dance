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
    /// circle 元素
    /// </summary>
    public class DanceCircle : DanceSVGDrawingElement
    {
        /// <summary>
        /// circle 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceCircle(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceCircle(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry GetBaseGeometry()
        {
            double cx = this.GetAttributeValue<DanceSVGDouble>("cx", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Width ?? 0) ?? 0;
            double cy = this.GetAttributeValue<DanceSVGDouble>("cy", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Height ?? 0) ?? 0;
            double r = this.GetAttributeValue<DanceSVGDouble>("r", false, DanceSVGDouble.Zero)?.GetValue(Math.Min(this.SVG?.Width ?? 0, this.SVG?.Height ?? 0)) ?? 0;

            EllipseGeometry geometry = new(new Point(cx, cy), r, r);

            return geometry;
        }
    }
}
