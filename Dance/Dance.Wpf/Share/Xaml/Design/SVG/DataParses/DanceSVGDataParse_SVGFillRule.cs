using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGFillRule
    /// </summary>
    public class DanceSVGDataParse_SVGFillRule : DanceSVGDataParse<DanceSVGFillRule>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            switch (attribute.Value)
            {
                case "nonzero": attribute.Data = DanceSVGFillRule.Nonzero; break;
                case "evenodd": attribute.Data = DanceSVGFillRule.Evenodd; break;
            }

            return true;
        }
    }
}
