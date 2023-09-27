using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGDouble
    /// </summary>
    public class DanceSVGDataParse_SVGDouble : DanceSVGDataParse<DanceSVGDouble>
    {
        /// <summary>
        /// 单位集合
        /// </summary>
        public static readonly string[] UNITS = new string[] { "px", "em", "pt", "ex", "pc", "in", "mm", "cm", "%" };

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.Value))
                return true;

            DanceSVGDouble result = new();

            foreach (string unit in UNITS)
            {
                if (attribute.Value.EndsWith(unit))
                {
                    result.Unit = unit;

                    result.Value = double.Parse(attribute.Value[..attribute.Value.IndexOf(unit[0])]);

                    attribute.Data = result;

                    return true;
                }
            }

            result.Value = double.Parse(attribute.Value);

            attribute.Data = result;

            return true;
        }
    }
}
