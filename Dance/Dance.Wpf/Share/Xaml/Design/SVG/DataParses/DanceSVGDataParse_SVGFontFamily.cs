using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGFontFamily
    /// </summary>
    public class DanceSVGDataParse_SVGFontFamily : DanceSVGDataParse<DanceSVGFontFamily>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            DanceSVGFontFamily result = new()
            {
                Value = new FontFamily(attribute.Value)
            };

            attribute.Data = result;

            return true;
        }
    }
}
