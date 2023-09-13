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
    /// 列集合
    /// </summary>
    public class DanceDataGridColumnItemsControl : ItemsControl
    {
        static DanceDataGridColumnItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceDataGridColumnItemsControl), new FrameworkPropertyMetadata(typeof(DanceDataGridColumnItemsControl)));
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceDataGridColumnItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceDataGridColumnItem();
        }
    }
}
