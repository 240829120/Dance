using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 剪切路径
    /// </summary>
    public class DanceSVGClipPath : DanceSVGData, IDanceSVGClipPath
    {
        /// <summary>
        /// 剪切路径
        /// </summary>
        public Geometry? Value { get; set; }
    }
}
