using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGBaseLineShift
    /// </summary>
    public class DanceSVGDataParse_SVGBaseLineShift : DanceSVGDataParse<DanceSVGBaseLineShift>
    {
        private readonly DanceSVGDataParse_SVGDouble Parser = new();

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            switch (attribute.Value)
            {
                case "baseline": attribute.Data = DanceSVGBaseLineShift.Baseline; break;
                case "sub": attribute.Data = DanceSVGBaseLineShift.Sub; break;
                case "super": attribute.Data = DanceSVGBaseLineShift.Super; break;
                default: Parser.Parse(attribute); break;
            }

            return true;
        }
    }
}
