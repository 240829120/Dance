using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 对齐方式
    /// </summary>
    public class DanceSVGTextAnchor : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public TextAlignment Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly DanceSVGTextAnchor Default = new() { Value = TextAlignment.Left };
    }
}
