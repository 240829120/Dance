using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// 虚线数组
    /// </summary>
    public class DanceSVGDashArray : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public List<double> Value { get; set; } = new List<double>();
    }
}
