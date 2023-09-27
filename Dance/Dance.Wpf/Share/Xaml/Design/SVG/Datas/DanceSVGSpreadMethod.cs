using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 过渡方向
    /// </summary>
    public class DanceSVGSpreadMethod : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public GradientSpreadMethod Value { get; set; }

        public static readonly DanceSVGSpreadMethod Pad = new() { Value = GradientSpreadMethod.Pad };

        public static readonly DanceSVGSpreadMethod Repeat = new() { Value = GradientSpreadMethod.Repeat };

        public static readonly DanceSVGSpreadMethod Reflect = new() { Value = GradientSpreadMethod.Reflect };
    }
}
