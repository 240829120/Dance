using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// use 元素
    /// </summary>
    public class DanceUse : DanceSVGDrawingContainerElement
    {
        /// <summary>
        /// use 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceUse(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceUse(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础渲染
        /// </summary>
        /// <returns>基础渲染</returns>
        protected override Drawing? GetBaseDrawing()
        {
            if (base.GetBaseDrawing() is not DrawingGroup drawing_group)
                return null;

            double x = this.GetAttributeValue<DanceSVGDouble>("x")?.GetValue(this.SVG?.Width ?? 0) ?? 0;
            double y = this.GetAttributeValue<DanceSVGDouble>("y")?.GetValue(this.SVG?.Height ?? 0) ?? 0;

            TransformGroup transform_group = GetTransformGroup(drawing_group);

            TranslateTransform tt = new()
            {
                X = x,
                Y = y
            };
            transform_group.Children.Add(tt);

            return drawing_group;
        }
    }
}
