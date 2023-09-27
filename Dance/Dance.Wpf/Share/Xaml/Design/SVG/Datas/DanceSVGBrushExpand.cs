using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// Double类型扩展
    /// </summary>
    public static class DanceSVGBrushExpand
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="brush">画刷</param>
        /// <returns>画刷</returns>
        public static Brush? GetValue(this IDanceSVGBrush brush)
        {
            return brush?.Value;
        }
    }
}
