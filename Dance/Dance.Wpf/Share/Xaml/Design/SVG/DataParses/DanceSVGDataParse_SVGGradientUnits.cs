using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// DataGradientUnits
    /// </summary>
    public class DanceSVGDataParse_SVGGradientUnits : DanceSVGDataParse<DanceSVGGradientUnits>
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
                case "userSpaceOnUse": attribute.Data = DanceSVGGradientUnits.UserSpaceOnUse; break;
                case "objectBoundingBox": attribute.Data = DanceSVGGradientUnits.ObjectBoundingBox; break;
            }

            return true;
        }
    }
}
