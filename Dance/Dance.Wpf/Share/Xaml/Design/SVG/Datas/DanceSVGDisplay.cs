using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// 显示属性
    /// </summary>
    public class DanceSVGDisplay : DanceSVGData
    {
        public static readonly DanceSVGDisplay Inline = new();
        public static readonly DanceSVGDisplay Block = new();
        public static readonly DanceSVGDisplay ListItem = new();
        public static readonly DanceSVGDisplay RunIn = new();
        public static readonly DanceSVGDisplay Compact = new();
        public static readonly DanceSVGDisplay Marker = new();
        public static readonly DanceSVGDisplay Table = new();
        public static readonly DanceSVGDisplay InlineTable = new();
        public static readonly DanceSVGDisplay TableRowGroup = new();
        public static readonly DanceSVGDisplay TableHeaderGroup = new();
        public static readonly DanceSVGDisplay TableFooterGroup = new();
        public static readonly DanceSVGDisplay TableRow = new();
        public static readonly DanceSVGDisplay TableColumnGroup = new();
        public static readonly DanceSVGDisplay TableColumn = new();
        public static readonly DanceSVGDisplay TableCell = new();
        public static readonly DanceSVGDisplay TableCaption = new();
    }
}
