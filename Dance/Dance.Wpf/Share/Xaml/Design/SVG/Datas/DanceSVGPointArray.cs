using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 点集合
    /// </summary>
    public class DanceSVGPointArray : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public List<Point> Value { get; set; } = new();
    }
}
