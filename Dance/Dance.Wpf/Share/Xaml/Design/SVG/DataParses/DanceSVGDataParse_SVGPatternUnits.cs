using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGPatternUnits
    /// </summary>
    public class DanceSVGDataParse_SVGPatternUnits : DanceSVGDataParse<DanceSVGPatternUnits>
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
                case "objectBoundingBox": attribute.Data = DanceSVGPatternUnits.objectBoundingBox; break;
                case "userSpaceOnUse": attribute.Data = DanceSVGPatternUnits.userSpaceOnUse; break;
            }

            return true;
        }
    }
}
