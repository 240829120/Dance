using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// 读取器
    /// </summary>
    public static class DanceSVGReader
    {
        /// <summary>
        /// 读取SVG文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>SVG元素</returns>
        public static DanceSVG Read(string path)
        {
            using System.IO.FileStream fs = new(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            return Read(fs);
        }

        /// <summary>
        /// 读取SVG文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns>SVG元素</returns>
        public static DanceSVG Read(System.IO.Stream stream)
        {
            XElement element = XElement.Load(stream);

            DanceSVG svg = new(null, null, element);

            foreach (XElement item in element.Elements())
            {
                Read(svg, svg, item);
            }

            svg.InitHref();

            return svg;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="root">根</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        private static void Read(DanceSVG root, DanceSVGElement parent, XElement element)
        {
            string name = element.Name.LocalName.Trim();

            DanceSVGElement? svg_element;
            switch (name)
            {
                // container elements
                case "clipPath": svg_element = new DanceClipPath(root, parent, element); break;
                case "defs": svg_element = new DanceDefs(root, parent, element); break;
                case "linearGradient": svg_element = new DanceLinearGradient(root, parent, element); break;
                case "mask": svg_element = new DanceMask(root, parent, element); break;
                case "radialGradient": svg_element = new DanceRadialGradient(root, parent, element); break;

                // drawing container elements
                case "g": svg_element = new DanceG(root, parent, element); break;
                case "marker": svg_element = new DanceMarker(root, parent, element); break;
                case "use": svg_element = new DanceUse(root, parent, element); break;

                // drawing elements
                case "circle": svg_element = new DanceCircle(root, parent, element); break;
                case "ellipse": svg_element = new DanceEllipse(root, parent, element); break;
                case "line": svg_element = new DanceLine(root, parent, element); break;
                case "path": svg_element = new DancePath(root, parent, element); break;
                case "polygon": svg_element = new DancePolygon(root, parent, element); break;
                case "polyline": svg_element = new DancePolyline(root, parent, element); break;
                case "rect": svg_element = new DanceRect(root, parent, element); break;
                case "text": svg_element = new DanceText(root, parent, element); break;

                // elements
                case "stop": svg_element = new DanceStop(root, parent, element); break;
                case "symbol": svg_element = new DanceSymbol(root, parent, element); break;

                // default
                default: return;
            }

            parent.Children.Add(svg_element);

            foreach (XElement item in element.Elements())
            {
                Read(root, svg_element, item);
            }
        }
    }
}
