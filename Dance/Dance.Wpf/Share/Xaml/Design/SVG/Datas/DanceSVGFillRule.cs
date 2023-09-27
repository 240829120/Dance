using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 填充方式
    /// </summary>
    public class DanceSVGFillRule : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public FillRule Value;

        public static readonly DanceSVGFillRule Nonzero = new() { Value = FillRule.Nonzero };

        public static readonly DanceSVGFillRule Evenodd = new() { Value = FillRule.EvenOdd };

    }
}
