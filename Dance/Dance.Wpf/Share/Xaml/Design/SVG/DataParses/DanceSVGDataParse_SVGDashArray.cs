using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGDashArray
    /// </summary>
    public class DanceSVGDataParse_SVGDashArray : DanceSVGDataParse<DanceSVGDashArray>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.Value))
                return true;

            DanceSVGDashArray result = new();

            string[] components = attribute.Value.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in components)
            {
                double v = double.Parse(item.Trim());

                result.Value.Add(v);
            }

            attribute.Data = result;

            return true;
        }
    }
}
