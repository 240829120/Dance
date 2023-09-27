using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// svg 元素
    /// </summary>
    public class DanceSVG : DanceSVGDrawingContainerElement
    {

        // =============================================================================================
        // ================== Property ==================

        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get; private set; }

        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// 资源
        /// </summary>
        internal Dictionary<string, DanceSVGElement> Resource { get; set; } = new Dictionary<string, DanceSVGElement>();

        // =============================================================================================

        /// <summary>
        /// svg 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceSVG(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {
            this.Width = this.GetAttributeValue<DanceSVGDouble>("width")?.GetValue() ?? 0;
            this.Height = this.GetAttributeValue<DanceSVGDouble>("height")?.GetValue() ?? 0;
        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceSVG(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 根据ID获取SVG元素
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>SVG元素</returns>
        public DanceSVGElement? GetSVGElementByID(string id)
        {
            if (!this.Resource.ContainsKey(id))
                return null;

            return this.Resource[id];
        }
    }
}
