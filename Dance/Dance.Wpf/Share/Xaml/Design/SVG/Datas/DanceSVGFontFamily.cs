using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 字体
    /// </summary>
    public class DanceSVGFontFamily : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public FontFamily? Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly DanceSVGFontFamily Default = new() { Value = new FontFamily("Microsoft YaHei") };
    }
}
