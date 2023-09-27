﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGFontStyle
    /// </summary>
    public class DanceSVGDataParse_SVGFontStyle : DanceSVGDataParse<DanceSVGFontStyle>
    {
        private readonly static FontStyleConverter Converter = new();

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.Value) || Converter.ConvertFromString(attribute.Value) is not FontStyle fontStyle)
                return false;

            DanceSVGFontStyle result = new()
            {
                Value = fontStyle
            };

            attribute.Data = result;

            return true;
        }
    }
}
