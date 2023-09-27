using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 字体样式
    /// </summary>
    public class DanceSVGFontStyle : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public FontStyle Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly DanceSVGFontStyle Default = new() { Value = FontStyles.Normal };
    }
}
