using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 画刷
    /// </summary>
    public class DanceSVGBrush : DanceSVGData, IDanceSVGBrush
    {
        /// <summary>
        /// 值
        /// </summary>
        public Brush? Value { get; set; }

        /// <summary>
        /// 黑色
        /// </summary>
        public static readonly DanceSVGBrush Black = new() { Value = new SolidColorBrush(Colors.Black) };
    }
}
