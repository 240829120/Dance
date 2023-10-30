using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class DanceTreeViewItem : HeaderedItemsControl
    {
        static DanceTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTreeViewItem), new FrameworkPropertyMetadata(typeof(DanceTreeViewItem)));
        }

        // =======================================================================================
        // Field

        /// <summary>
        /// 所属树
        /// </summary>
        internal DanceTreeView? TreeView { get; set; }

        // =======================================================================================
        // Property

        #region Level -- 等级

        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register("Level", typeof(int), typeof(DanceTreeViewItem), new PropertyMetadata(0));

        #endregion

        #region IsExpanded -- 是否展开

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// 是否展开
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(DanceTreeViewItem), new PropertyMetadata(false));

        #endregion

        #region IsSelected -- 是否选中

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(DanceTreeViewItem), new PropertyMetadata(false));

        #endregion

        #region IsDragOver -- 拖拽是否滑过

        /// <summary>
        /// 拖拽是否滑过
        /// </summary>
        public bool IsDragOver
        {
            get { return (bool)GetValue(IsDragOverProperty); }
            private set { SetValue(IsDragOverPropertyKey, value); }
        }

        /// <summary>
        /// 拖拽是否滑过
        /// </summary>
        public static readonly DependencyPropertyKey IsDragOverPropertyKey =
            DependencyProperty.RegisterReadOnly("IsDragOver", typeof(bool), typeof(DanceTreeViewItem), new PropertyMetadata(false));

        /// <summary>
        /// 拖拽是否滑过
        /// </summary>
        public static readonly DependencyProperty IsDragOverProperty = IsDragOverPropertyKey.DependencyProperty;

        #endregion

        // =======================================================================================
        // Override Function

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is DanceTreeViewItem node)
            {
                node.Level = this.Level + 1;
                node.TreeView = this.TreeView;
                return true;
            }

            return false;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTreeViewItem() { Level = this.Level + 1, TreeView = this.TreeView };
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            this.IsDragOver = true;
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);

            this.IsDragOver = false;
        }
    }
}