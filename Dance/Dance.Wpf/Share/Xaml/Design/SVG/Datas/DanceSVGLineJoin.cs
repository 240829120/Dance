using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 连接线样式
    /// </summary>
    public class DanceSVGLineJoin : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public PenLineJoin Value;

        public static readonly DanceSVGLineJoin Miter = new() { Value = PenLineJoin.Miter };

        public static readonly DanceSVGLineJoin Round = new() { Value = PenLineJoin.Round };

        public static readonly DanceSVGLineJoin Bevel = new() { Value = PenLineJoin.Bevel };
    }
}
