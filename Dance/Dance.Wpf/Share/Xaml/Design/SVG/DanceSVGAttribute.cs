using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// SVG属性
    /// </summary>
    public class DanceSVGAttribute : DanceSVGObject
    {
        /// <summary>
        /// 值解释器集合
        /// </summary>
        static readonly List<IDanceSVGDataParse> Providers = new();

        static DanceSVGAttribute()
        {
            Providers.Add(new DanceSVGDataParse_SVGDouble());
            Providers.Add(new DanceSVGDataParse_SVGString());
            Providers.Add(new DanceSVGDataParse_SVGBrush());
            Providers.Add(new DanceSVGDataParse_SVGColor());
            Providers.Add(new DanceSVGDataParse_SVGDashArray());
            Providers.Add(new DanceSVGDataParse_SVGLineCap());
            Providers.Add(new DanceSVGDataParse_SVGLineJoin());
            Providers.Add(new DanceSVGDataParse_SVGFillRule());
            Providers.Add(new DanceSVGDataParse_SVGPathData());
            Providers.Add(new DanceSVGDataParse_SVGPointArray());
            Providers.Add(new DanceSVGDataParse_SVGTransform());
            Providers.Add(new DanceSVGDataParse_SVGPatternUnits());
            Providers.Add(new DanceSVGDataParse_SVGGradientUnits());
            Providers.Add(new DanceSVGDataParse_SVGDisplay());
            Providers.Add(new DanceSVGDataParse_SVGClipPath());
            Providers.Add(new DanceSVGDataParse_SVGMask());
            Providers.Add(new DanceSVGDataParse_SVGSpreadMethod());
            Providers.Add(new DanceSVGDataParse_SVGFontFamily());
            Providers.Add(new DanceSVGDataParse_SVGFontWeight());
            Providers.Add(new DanceSVGDataParse_SVGFontStyle());
            Providers.Add(new DanceSVGDataParse_SVGTextDecoration());
            Providers.Add(new DanceSVGDataParse_SVGTextAnchor());
            Providers.Add(new DanceSVGDataParse_SVGBaseLineShift());
        }

        /// <summary>
        /// SVG属性
        /// </summary>
        /// <param name="svg">根元素</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        public DanceSVGAttribute(DanceSVG? svg, XName name, string value)
        {
            this.SVG = svg;
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// 尝试获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static void Parse<T>(DanceSVGAttribute attribute) where T : DanceSVGData
        {
            if (string.IsNullOrWhiteSpace(attribute.Value) || attribute.Value.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                attribute.Data = DanceSVGData.Null;
                attribute.IsParsed = true;

                return;
            }

            if (attribute.Value.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                attribute.Data = DanceSVGData.None;
                attribute.IsParsed = true;

                return;
            }

            Type type = typeof(T);

            foreach (IDanceSVGDataParse provider in Providers)
            {
                if (provider.Type != type)
                    continue;

                if (!provider.Parse(attribute))
                {
                    throw new DanceSVGDataParseException(attribute.Name, attribute.Value);
                }

                attribute.IsParsed = true;

                return;
            }

            attribute.IsParsed = true;

            throw new DanceSVGDataParseException(attribute.Name, attribute.Value);
        }

        /// <summary>
        /// 根元素
        /// </summary>
        public DanceSVG? SVG { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public XName Name { get; private set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string? Value { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public DanceSVGData? Data { get; set; }

        /// <summary>
        /// 是否已经转化过
        /// </summary>
        public bool IsParsed { get; set; }

        public override string ToString()
        {
            return $"{{{this.Name}, {this.Value}}}";
        }
    }
}
