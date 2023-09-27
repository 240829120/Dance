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
    public interface IDanceSVGBrush
    {
        /// <summary>
        /// 画刷
        /// </summary>
        Brush? Value { get; }
    }
}
