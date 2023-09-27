using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGPathData
    /// </summary>
    public class DanceSVGDataParse_SVGPathData : DanceSVGDataParse<DanceSVGPathData>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            DanceSVGPathData result = new()
            {
                Value = Geometry.Parse(attribute.Value).Clone()
            };

            attribute.Data = result;

            return true;
        }
    }
}
