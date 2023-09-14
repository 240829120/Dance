using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 表格列项
    /// </summary>
    public class DanceDataGridColumnItem : Control
    {
        static DanceDataGridColumnItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceDataGridColumnItem), new FrameworkPropertyMetadata(typeof(DanceDataGridColumnItem)));
        }
    }
}
