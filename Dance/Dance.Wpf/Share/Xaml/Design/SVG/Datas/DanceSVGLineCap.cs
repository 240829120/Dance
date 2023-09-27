using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 线冒
    /// </summary>
    public class DanceSVGLineCap : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public PenLineCap Value { get; set; }

        public static readonly DanceSVGLineCap Butt = new() { Value = PenLineCap.Flat };

        public static readonly DanceSVGLineCap Round = new() { Value = PenLineCap.Round };

        public static readonly DanceSVGLineCap Square = new() { Value = PenLineCap.Square };
    }
}
