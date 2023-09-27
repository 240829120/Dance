using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// rect 元素
    /// </summary>
    public class DanceRect : DanceSVGDrawingElement
    {
        /// <summary>
        /// rect 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceRect(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceRect(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry? GetBaseGeometry()
        {
            double width = this.GetAttributeValue<DanceSVGDouble>("width", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Width ?? 0) ?? 0;
            double height = this.GetAttributeValue<DanceSVGDouble>("height", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Height ?? 0) ?? 0;

            if (width <= 0 || height <= 0)
                return null;

            double x = this.GetAttributeValue<DanceSVGDouble>("x", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Width ?? 0) ?? 0;
            double y = this.GetAttributeValue<DanceSVGDouble>("y", false, DanceSVGDouble.Zero)?.GetValue(this.SVG?.Height ?? 0) ?? 0;
            DanceSVGDouble? rx = this.GetAttributeValue<DanceSVGDouble>("rx");
            DanceSVGDouble? ry = this.GetAttributeValue<DanceSVGDouble>("ry");

            RectangleGeometry geometry = new()
            {
                Rect = new System.Windows.Rect(x, y, width, height)
            };
            if (rx != null && ry != null)
            {
                geometry.RadiusX = rx.GetValue(width);
                geometry.RadiusY = ry.GetValue(height);
            }
            else if (rx != null && ry == null)
            {
                geometry.RadiusX = rx.GetValue(width);
                geometry.RadiusY = geometry.RadiusX;
            }
            else if (rx == null && ry != null)
            {
                geometry.RadiusX = ry.GetValue(height);
                geometry.RadiusY = geometry.RadiusX;
            }

            return geometry;
        }
    }
}
