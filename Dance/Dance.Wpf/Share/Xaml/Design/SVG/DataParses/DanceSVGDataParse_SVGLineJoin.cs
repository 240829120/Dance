using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGLineJoin
    /// </summary>
    public class DanceSVGDataParse_SVGLineJoin : DanceSVGDataParse<DanceSVGLineJoin>
    {

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="element">当前元素</param>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            switch (attribute.Value)
            {
                case "miter": attribute.Data = DanceSVGLineJoin.Miter; break;
                case "round": attribute.Data = DanceSVGLineJoin.Round; break;
                case "bevel": attribute.Data = DanceSVGLineJoin.Bevel; break;
            }

            return true;
        }
    }
}
