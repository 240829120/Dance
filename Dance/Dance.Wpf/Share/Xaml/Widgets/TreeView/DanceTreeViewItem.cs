using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class DanceTreeViewItem : TreeViewItem
    {
        static DanceTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTreeViewItem), new FrameworkPropertyMetadata(typeof(DanceTreeViewItem)));
        }

        /// <summary>
        /// 所属树
        /// </summary>
        internal DanceTreeView? TreeView { get; set; }

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

        #region IsMultiSelected -- 是否被多选

        /// <summary>
        /// 是否被多选
        /// </summary>
        public bool IsMultiSelected
        {
            get { return (bool)GetValue(IsMultiSelectedProperty); }
            set { SetValue(IsMultiSelectedProperty, value); }
        }

        /// <summary>
        /// 是否被多选
        /// </summary>
        public static readonly DependencyProperty IsMultiSelectedProperty =
            DependencyProperty.Register("IsMultiSelected", typeof(bool), typeof(DanceTreeViewItem), new PropertyMetadata(false));

        #endregion

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

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (this.TreeView == null || e.OriginalSource is not FrameworkElement element || DanceXamlExpansion.GetVisualTreeParent<DanceTreeViewItem>(element) != this)
                return;

            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                this.IsMultiSelected = !this.IsMultiSelected;

                if (this.IsMultiSelected)
                {
                    this.TreeView.SelectedItems.Add(this);
                }
                else
                {
                    this.TreeView.SelectedItems.Remove(this);
                }

                this.TreeView.UpdateSelectedValues();

                return;
            }
            else if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                DanceTreeViewItem? from = this.TreeView.SelectedItems.LastOrDefault();
                if (from == null)
                {
                    this.IsMultiSelected = true;
                    this.TreeView.SelectedItems.Clear();
                    this.TreeView.SelectedItems.Add(this);

                    this.TreeView.UpdateSelectedValues();

                    return;
                }

                List<DanceTreeViewItem> items = new();
                this.TraversalTreeViewItem(items, from, this);

                this.TreeView.SelectedItems.ForEach(p => p.IsMultiSelected = false);
                this.TreeView.SelectedItems.Clear();
                items.ForEach(p => p.IsMultiSelected = true);
                this.TreeView.SelectedItems.AddRange(items);

                this.TreeView.UpdateSelectedValues();

                return;
            }
            else
            {
                this.TreeView.SelectedItems.ForEach(p => p.IsMultiSelected = false);

                this.IsMultiSelected = true;
                this.TreeView.SelectedItems.Clear();
                this.TreeView.SelectedItems.Add(this);

                this.TreeView.UpdateSelectedValues();
            }
        }

        /// <summary>
        /// 便利书节点
        /// </summary>
        /// <param name="list">便利结果集合</param>
        /// <param name="from">开始节点</param>
        /// <param name="to">结束节点</param>
        private void TraversalTreeViewItem(List<DanceTreeViewItem> list, DanceTreeViewItem from, DanceTreeViewItem to)
        {
            var items = DanceXamlExpansion.GetVisualTreeDescendants<DanceTreeViewItem>(this.TreeView);
            int start = items.IndexOf(from);
            int end = items.IndexOf(to);

            list.AddRange(items.Skip(start).Take(end - start + 1));
        }
    }
}