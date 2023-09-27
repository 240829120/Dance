using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// tspan 元素
    /// </summary>
    public class DanceTspan : DanceSVGElement
    {
        /// <summary>
        /// path 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceTspan(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 节点
        /// </summary>
        public XNode? Node { get; set; }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            DanceTspan result = new(this.SVG, this.Parent, this.Element)
            {
                Node = this.Node
            };

            return result;
        }

        /// <summary>
        /// 构建文本形状
        /// </summary>
        /// <param name="group">文本形状组</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="totalwidth">总宽度</param>
        public void BuildTextSpan(GeometryGroup group, ref double x, ref double y, ref double totalwidth)
        {
            if (this.Node == null)
                return;

            if (this.Node.NodeType == XmlNodeType.Element)
            {
                foreach (DanceSVGElement element in this.Children)
                {
                    if (element is not DanceTspan item)
                        continue;

                    item.BuildTextSpan(group, ref x, ref y, ref totalwidth);
                }

                return;
            }

            if (this.Node is not XText xtext)
                return;

            string text = xtext.Value;
            if (string.IsNullOrWhiteSpace(text))
                return;

            double baseline = y;

            DanceSVGBaseLineShift? baseline_shift = this.GetAttributeValue<DanceSVGBaseLineShift>("baseline-shift", true, null);
            double font_size = this.GetAttributeValue<DanceSVGDouble>("font-size", true, new DanceSVGDouble(12))?.GetValue(12) ?? 12;

            if (baseline_shift != null)
            {
                if (baseline_shift == DanceSVGBaseLineShift.Sub)
                {
                    baseline += font_size * 0.5;
                }
                else if (baseline_shift == DanceSVGBaseLineShift.Super)
                {
                    baseline -= font_size * 1.25;
                }
                else if (baseline_shift != null)
                {
                    baseline += font_size * baseline_shift.Value?.GetValue(font_size) ?? 0;
                }
            }

            if (this.BuildGlyphRun(text, x, baseline, ref totalwidth) is Geometry geometry)
            {
                group.Children.Add(geometry);
            }

            x += totalwidth;
        }

        /// <summary>
        /// 构建文本形状
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="totalwidth">总宽度</param>
        /// <returns>形状</returns>
        private Geometry? BuildGlyphRun(string text, double x, double y, ref double totalwidth)
        {
            double baseline = y;

            //DanceSVGBaseLineShift baseline_shift = this.GetAttributeValue<DanceSVGBaseLineShift>("baseline-shift", true, null);
            double font_size = this.GetAttributeValue<DanceSVGDouble>("font-size", true, new DanceSVGDouble(12))?.GetValue(12) ?? 12;
            DanceSVGFontFamily font_family = this.GetAttributeValue<DanceSVGFontFamily>("font-family", true, DanceSVGFontFamily.Default) ?? DanceSVGFontFamily.Default;
            DanceSVGFontWeight font_weight = this.GetAttributeValue<DanceSVGFontWeight>("font-weight", true, DanceSVGFontWeight.Default) ?? DanceSVGFontWeight.Default;
            DanceSVGFontStyle font_style = this.GetAttributeValue<DanceSVGFontStyle>("font-style", true, DanceSVGFontStyle.Default) ?? DanceSVGFontStyle.Default;
            DanceSVGTextAnchor text_anchor = this.GetAttributeValue<DanceSVGTextAnchor>("text-anchor", true, DanceSVGTextAnchor.Default) ?? DanceSVGTextAnchor.Default;
            DanceSVGTextDecoration? text_decoration = this.GetAttributeValue<DanceSVGTextDecoration>("text-decoration", true, null);
            double letter_spacing = this.GetAttributeValue<DanceSVGDouble>("letter-spacing", true, DanceSVGDouble.Zero)?.GetValue(12) ?? 0;
            double word_spacing = this.GetAttributeValue<DanceSVGDouble>("word-spacing", true, DanceSVGDouble.Zero)?.GetValue(12) ?? 0;

            GlyphRun glyphs;

            Typeface type_face = new(font_family.Value, font_style.Value, font_weight.Value, FontStretch.FromOpenTypeStretch(9), DanceSVGFontFamily.Default.Value);

            if (!type_face.TryGetGlyphTypeface(out GlyphTypeface glyph_type_face))
            {
                return null;
            }

            glyphs = new((float)DanceXamlExpansion.GetPixelsPerDip());

            ((System.ComponentModel.ISupportInitialize)glyphs).BeginInit();

            glyphs.GlyphTypeface = glyph_type_face;
            glyphs.FontRenderingEmSize = font_size;
            List<char> text_chars = new();
            List<ushort> glyph_indices = new();
            List<double> advance_widths = new();
            totalwidth = 0;
            for (int i = 0; i < text.Length; ++i)
            {
                char c = text[i];
                int code_point = c;
                if (!glyph_type_face.CharacterToGlyphMap.TryGetValue(code_point, out ushort glyph_index))
                    continue;

                text_chars.Add(c);
                double glyph_width = glyph_type_face.AdvanceWidths[glyph_index];
                glyph_indices.Add(glyph_index);
                advance_widths.Add(glyph_width * font_size + letter_spacing);
                if (char.IsWhiteSpace(c))
                {
                    advance_widths[^1] += word_spacing;
                }
                totalwidth += advance_widths[^1];
            }
            glyphs.Characters = text_chars.ToArray();
            glyphs.GlyphIndices = glyph_indices.ToArray();
            glyphs.AdvanceWidths = advance_widths.ToArray();

            double alignmentoffset = 0;
            if (text_anchor.Value == TextAlignment.Center)
            {
                alignmentoffset = totalwidth / 2;
            }
            else if (text_anchor.Value == TextAlignment.Right)
            {
                alignmentoffset = totalwidth;
            }

            glyphs.BaselineOrigin = new Point(x - alignmentoffset, baseline);

            ((System.ComponentModel.ISupportInitialize)glyphs).EndInit();

            GeometryGroup result = new();

            result.Children.Add(glyphs.BuildGeometry());

            if (text_decoration != null)
            {
                double decoration_pos = 0;
                double decoration_thinkess = 0;

                if (text_decoration.Value == TextDecorationLocation.Strikethrough)
                {
                    decoration_pos = baseline - (type_face.StrikethroughPosition * font_size);
                    decoration_thinkess = type_face.StrikethroughThickness * font_size;
                }
                else if (text_decoration.Value == TextDecorationLocation.Underline)
                {
                    decoration_pos = baseline - (type_face.UnderlinePosition * font_size);
                    decoration_thinkess = type_face.UnderlineThickness * font_size;
                }
                else if (text_decoration.Value == TextDecorationLocation.OverLine)
                {
                    decoration_pos = baseline - font_size;
                    decoration_thinkess = type_face.StrikethroughThickness * font_size;
                }

                Rect bounds = new(result.Bounds.Left, decoration_pos, result.Bounds.Width, decoration_thinkess);

                result.Children.Add(new RectangleGeometry(bounds));
            }

            return result;
        }
    }
}