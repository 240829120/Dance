using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGDashArray
    /// </summary>
    public class DanceSVGDataParse_SVGPointArray : DanceSVGDataParse<DanceSVGPointArray>
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

            DanceSVGPointArray result = new();

            string[] components = attribute.Value.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < components.Length; i += 2)
            {
                double x = double.Parse(components[i].Trim());
                double y = double.Parse(components[i + 1].Trim());

                result.Value.Add(new Point(x, y));
            }

            attribute.Data = result;

            return true;
        }
    }
}
