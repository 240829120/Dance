using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGDisplay
    /// </summary>
    public class DanceSVGDataParse_SVGDisplay : DanceSVGDataParse<DanceSVGDisplay>
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
                case "inline": attribute.Data = DanceSVGDisplay.Inline; break;
                case "block": attribute.Data = DanceSVGDisplay.Block; break;
                case "list-item": attribute.Data = DanceSVGDisplay.ListItem; break;
                case "run-in": attribute.Data = DanceSVGDisplay.RunIn; break;
                case "compact": attribute.Data = DanceSVGDisplay.Compact; break;
                case "marker": attribute.Data = DanceSVGDisplay.Marker; break;
                case "table": attribute.Data = DanceSVGDisplay.Table; break;
                case "inline-table": attribute.Data = DanceSVGDisplay.InlineTable; break;
                case "table-row-group": attribute.Data = DanceSVGDisplay.TableRowGroup; break;
                case "table-header-group": attribute.Data = DanceSVGDisplay.TableHeaderGroup; break;
                case "table-row": attribute.Data = DanceSVGDisplay.TableRow; break;
                case "table-column-group": attribute.Data = DanceSVGDisplay.TableColumnGroup; break;
                case "table-column": attribute.Data = DanceSVGDisplay.TableColumn; break;
                case "table-cell": attribute.Data = DanceSVGDisplay.TableCell; break;
                case "table-caption": attribute.Data = DanceSVGDisplay.TableCaption; break;
                case "none": attribute.Data = DanceSVGDisplay.None; break;
            }

            return true;
        }
    }
}
