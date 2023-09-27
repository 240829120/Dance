using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 使用坐标系
    /// </summary>
    public class DanceSVGGradientUnits : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public BrushMappingMode Value { get; set; }

        public static readonly DanceSVGGradientUnits ObjectBoundingBox = new() { Value = BrushMappingMode.RelativeToBoundingBox };

        public static readonly DanceSVGGradientUnits UserSpaceOnUse = new() { Value = BrushMappingMode.Absolute };
    }
}
