using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGFontWeight
    /// </summary>
    public class DanceSVGDataParse_SVGFontWeight : DanceSVGDataParse<DanceSVGFontWeight>
    {
        private readonly static FontWeightConverter Converter = new();

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.Value))
                return false;

            if (Converter.ConvertFromString(attribute.Value) is not FontWeight fontWeight)
                return false;

            DanceSVGFontWeight result = new()
            {
                Value = fontWeight
            };

            attribute.Data = result;

            return true;
        }
    }
}
