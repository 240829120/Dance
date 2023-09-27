using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 字体
    /// </summary>
    public class DanceSVGFontWeight : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public FontWeight Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly DanceSVGFontWeight Default = new() { Value = FontWeights.Normal };
    }
}
