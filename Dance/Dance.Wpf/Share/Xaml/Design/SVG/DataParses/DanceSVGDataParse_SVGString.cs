using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGString
    /// </summary>
    public class DanceSVGDataParse_SVGString : DanceSVGDataParse<DanceSVGString>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            DanceSVGString value = new()
            {
                Value = attribute.Value
            };

            attribute.Data = value;

            return true;
        }
    }
}
