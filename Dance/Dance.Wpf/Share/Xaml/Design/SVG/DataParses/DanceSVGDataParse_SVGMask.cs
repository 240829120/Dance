using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGMask
    /// </summary>
    public class DanceSVGDataParse_SVGMask : DanceSVGDataParse<DanceSVGMask>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            if (attribute.SVG == null || string.IsNullOrWhiteSpace(attribute.Value))
                return true;

            DanceSVGMask result = new();

            if (attribute.Value.StartsWith("url"))
            {
                result.Value = Get_URL(attribute.SVG, attribute.Value);
            }

            attribute.Data = result;

            return true;
        }

        /// <summary>
        /// 尝试从url中获取画刷
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="value">颜色</param>
        /// <returns>画刷</returns>
        private static DrawingBrush? Get_URL(DanceSVG svg, string value)
        {
            value = value.Replace(" ", "").Replace("\t", "");
            value = value.Replace("url(#", "").Replace(")", "");

            if (!svg.Resource.ContainsKey(value))
                return null;

            if (svg.Resource[value] is not IDanceSVGMask mask || mask.Value == null)
                return null;

            return mask.Value.Clone();
        }
    }
}
