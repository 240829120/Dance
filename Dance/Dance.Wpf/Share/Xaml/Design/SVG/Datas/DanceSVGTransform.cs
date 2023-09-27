using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 变换
    /// </summary>
    public class DanceSVGTransform : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public List<Transform> Value { get; set; } = new();
    }
}
