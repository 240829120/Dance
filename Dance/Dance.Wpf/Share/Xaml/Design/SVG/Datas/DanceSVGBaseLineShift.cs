using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// 文本定位
    /// </summary>
    public class DanceSVGBaseLineShift : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public DanceSVGDouble? Value { get; set; }

        public static readonly DanceSVGBaseLineShift Baseline = new();

        public static readonly DanceSVGBaseLineShift Sub = new();

        public static readonly DanceSVGBaseLineShift Super = new();
    }
}
