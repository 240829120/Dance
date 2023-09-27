using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// Double类型扩展
    /// </summary>
    public static class DanceSVGDoubleExpand
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="v">SVGDouble</param>
        /// <param name="max">最大值</param>
        /// <returns>真实值</returns>
        public static double GetValue(this DanceSVGDouble v, double max = 100d)
        {
            if (v == null)
                return 0d;

            double result;

            if (!string.IsNullOrWhiteSpace(v.Unit) && v.Unit.Equals("%"))
            {
                result = v.Value / 100d * max;
            }
            else
            {
                result = v.Value;
            }

            return result;
        }
    }
}