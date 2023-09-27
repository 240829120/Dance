using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// text 元素
    /// </summary>
    public class DanceText : DanceSVGDrawingElement
    {
        /// <summary>
        /// text 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceText(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {
            if (element == null)
                return;

            foreach (XNode node in element.Nodes())
            {
                this.Read(node, this);
            }
        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceText(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 读取节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="parent">父级元素</param>
        private void Read(XNode node, DanceSVGElement parent)
        {
            if (node.NodeType == XmlNodeType.Text)
            {
                DanceTspan span = new(this.SVG, parent, null)
                {
                    Node = node
                };

                this.Children.Add(span);
            }
            else if (node.NodeType == XmlNodeType.Element && node is XElement element)
            {
                DanceTspan span = new(this.SVG, parent, element)
                {
                    Node = node
                };

                this.Children.Add(span);

                foreach (XNode n in element.Nodes())
                {
                    this.Read(n, span);
                }
            }
        }

        /// <summary>
        /// 获取基础渲染
        /// </summary>
        /// <returns>基础渲染</returns>
        protected override Geometry GetBaseGeometry()
        {
            GeometryGroup result = new();

            double x = this.GetAttributeValue<DanceSVGDouble>("x")?.GetValue(this.SVG?.Width ?? 0) ?? 0;
            double y = this.GetAttributeValue<DanceSVGDouble>("y")?.GetValue(this.SVG?.Height ?? 0) ?? 0;
            double totalwidth = 0;

            foreach (DanceSVGElement element in this.Children)
            {
                if (element is not DanceTspan item)
                    continue;

                item.BuildTextSpan(result, ref x, ref y, ref totalwidth);
            }

            return result;
        }
    }
}
