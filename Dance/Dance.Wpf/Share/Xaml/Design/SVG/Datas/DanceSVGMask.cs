using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 蒙版
    /// </summary>
    public class DanceSVGMask : DanceSVGData, IDanceSVGMask
    {
        /// <summary>
        /// 值
        /// </summary>
        public DrawingBrush? Value { get; set; }
    }
}
