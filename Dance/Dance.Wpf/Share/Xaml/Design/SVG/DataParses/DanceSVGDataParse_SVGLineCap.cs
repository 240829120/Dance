using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGLineCap
    /// </summary>
    public class DanceSVGDataParse_SVGLineCap : DanceSVGDataParse<DanceSVGLineCap>
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
                case "butt": attribute.Data = DanceSVGLineCap.Butt; break;
                case "round": attribute.Data = DanceSVGLineCap.Round; break;
                case "square": attribute.Data = DanceSVGLineCap.Square; break;
            }

            return true;
        }
    }
}
