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
    /// SVGFontWeight
    /// </summary>
    public class DanceSVGDataParse_SVGTextDecoration : DanceSVGDataParse<DanceSVGTextDecoration>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            DanceSVGTextDecoration result = new();

            switch (attribute.Value)
            {
                case "underline": result.Value = TextDecorationLocation.Underline; break;
                case "overline": result.Value = TextDecorationLocation.OverLine; break;
                case "line-through": result.Value = TextDecorationLocation.Strikethrough; break;
            }

            attribute.Data = result;

            return true;
        }
    }
}
