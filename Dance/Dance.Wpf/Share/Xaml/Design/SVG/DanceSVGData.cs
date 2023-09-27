using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// SVG数据
    /// </summary>
    public class DanceSVGData : DanceSVGObject
    {
        /// <summary>
        /// 空
        /// </summary>
        public static DanceSVGData Null { get; } = new();

        /// <summary>
        /// 未设置
        /// </summary>
        public static DanceSVGData None { get; } = new();

        /// <summary>
        /// 继承
        /// </summary>
        public static DanceSVGData Inherit { get; } = new();
    }
}
