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
    /// SVGTextAnchor
    /// </summary>
    public class DanceSVGDataParse_SVGTextAnchor : DanceSVGDataParse<DanceSVGTextAnchor>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            DanceSVGTextAnchor result = new();

            switch (attribute.Value)
            {
                case "start": result.Value = TextAlignment.Left; break;
                case "middle": result.Value = TextAlignment.Center; break;
                case "end": result.Value = TextAlignment.Right; break;
            }

            attribute.Data = result;

            return true;
        }
    }
}
