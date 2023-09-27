using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGSpreadMethod
    /// </summary>
    public class DanceSVGDataParse_SVGSpreadMethod : DanceSVGDataParse<DanceSVGSpreadMethod>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            switch (attribute.Value)
            {
                case "pad": attribute.Data = DanceSVGSpreadMethod.Pad; break;
                case "repeat": attribute.Data = DanceSVGSpreadMethod.Repeat; break;
                case "reflect": attribute.Data = DanceSVGSpreadMethod.Reflect; break;
            }

            return true;
        }
    }
}
