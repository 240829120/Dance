using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

        // =======================================================================================
        // Field

        /// <summary>
        /// 当前选中的节点
        /// </summary>
        private DanceTreeViewItem? SelectedNode;

        // =======================================================================================
        // Override Function

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is DanceTreeViewItem node)
            {
                node.Level = 1;
                node.TreeView = this;
                return true;
            }

            return false;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTreeViewItem() { Level = 1, TreeView = this };
        }

        /// <summary>
        /// 鼠标左键双击
        /// </summary>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || e.OriginalSource is not FrameworkElement element || DanceXamlExpansion.GetVisualTreeParent<DanceTreeViewItem>(element) is not DanceTreeViewItem item)
                return;

            item.IsExpanded = !item.IsExpanded;
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is not FrameworkElement element || DanceXamlExpansion.GetVisualTreeParent<DanceTreeViewItem>(element) is not DanceTreeViewItem item)
                return;

            item.IsSelected = true;

            if (this.SelectedNode != null && this.SelectedNode != item)
            {
                this.SelectedNode.IsSelected = false;
            }
            this.SelectedNode = item;
        }
    }
}
