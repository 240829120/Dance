using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGBrush
    /// </summary>
    public class DanceSVGDataParse_SVGBrush : DanceSVGDataParse<DanceSVGBrush>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.Value))
            {
                return true;
            }

            DanceSVGBrush result = new();

            if (attribute.Value.StartsWith("url"))
            {
                result.Value = Get_URL(attribute.SVG, attribute.Value);
            }
            else if (attribute.Value.StartsWith("#"))
            {
                Color color = ParseColor_Well(attribute.Value);
                result.Value = new SolidColorBrush(color);
            }
            else if (attribute.Value.StartsWith("rgb"))
            {
                Color color = ParseColor_RGB(attribute.Value);
                result.Value = new SolidColorBrush(color);
            }
            else
            {
                Color color = (Color)ColorConverter.ConvertFromString(attribute.Value);
                result.Value = new SolidColorBrush(color);
            }

            attribute.Data = result;

            return true;
        }

        /// <summary>
        /// 解释颜色 -- 井号
        /// </summary>
        /// <param name="value">颜色</param>
        /// <returns>颜色</returns>
        private static Color ParseColor_Well(string value)
        {
            Color result = new()
            {
                A = 0xFF
            };

            string color = value[1..].Trim();
            if (color.Length == 3)
            {
                result.R = Byte.Parse(String.Format("{0}{0}", color[0]), NumberStyles.HexNumber);
                result.G = Byte.Parse(String.Format("{0}{0}", color[1]), NumberStyles.HexNumber);
                result.B = Byte.Parse(String.Format("{0}{0}", color[2]), NumberStyles.HexNumber);
            }
            else if (color.Length == 6)
            {
                result.R = Byte.Parse(color[..2], NumberStyles.HexNumber);
                result.G = Byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber);
                result.B = Byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber);
            }

            return result;
        }

        /// <summary>
        /// 解释颜色 -- RGB
        /// </summary>
        /// <param name="value">颜色</param>
        /// <returns>颜色</returns>
        private static Color ParseColor_RGB(string value)
        {
            Color result = new()
            {
                A = 0xFF
            };

            string color = value[3..].Trim();

            color = color[1..^1].Trim();

            string[] components = color.Split(',');
            for (int i = 0; i < components.Length; ++i)
            {
                components[i] = components[i].Trim();
            }

            if (components.Length == 3)
            {
                // R
                if (components[0].EndsWith("%"))
                {
                    result.R = (byte)(double.Parse(components[0][..^1].Trim()) / 255d);
                }
                else
                {
                    result.R = Byte.Parse(components[0]);
                }

                // G
                if (components[1].EndsWith("%"))
                {
                    result.R = (byte)(double.Parse(components[1][..(components[0].Length - 1)].Trim()) / 255d);
                }
                else
                {
                    result.R = Byte.Parse(components[1]);
                }

                // B
                if (components[2].EndsWith("%"))
                {
                    result.R = (byte)(double.Parse(components[2][..(components[0].Length - 1)].Trim()) / 255d);
                }
                else
                {
                    result.R = Byte.Parse(components[2]);
                }
            }

            return result;
        }

        /// <summary>
        /// 尝试从url中获取画刷
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="value">颜色</param>
        /// <returns>画刷</returns>
        private static Brush? Get_URL(DanceSVG? svg, string value)
        {
            value = value.Replace(" ", "").Replace("\t", "");
            value = value.Replace("url(#", "").Replace(")", "");

            if (svg == null || !svg.Resource.ContainsKey(value))
                return null;

            if (svg.Resource[value] is not IDanceSVGBrush brush || brush.Value == null)
                return null;

            return brush.Value.Clone();
        }
    }
}
