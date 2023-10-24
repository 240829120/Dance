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
    /// 树
    /// </summary>
    public class DanceTreeView : TreeView
    {
        static DanceTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTreeView), new FrameworkPropertyMetadata(typeof(DanceTreeView)));
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is DanceTreeViewItem node)
            {
                node.Level = 1;
                return true;
            }

            return false;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTreeViewItem() { Level = 1 };
        }
    }
}
