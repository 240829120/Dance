using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 滤镜
    /// </summary>
    public interface IDanceSVGMask
    {
        /// <summary>
        /// 值
        /// </summary>
        DrawingBrush? Value { get; }
    }
}
