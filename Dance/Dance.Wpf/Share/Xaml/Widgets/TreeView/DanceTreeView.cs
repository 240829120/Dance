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
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 树
    /// </summary>
    public class DanceTreeView : ItemsControl
    {
        static DanceTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTreeView), new FrameworkPropertyMetadata(typeof(DanceTreeView)));
        }

        // =======================================================================================
        // Field

        /// <summary>
        /// 当前选中的节点集合
        /// </summary>
        private readonly List<DanceTreeViewItem> SelectedNodes = [];

        /// <summary>
        /// 用于标记Shift键的节点
        /// </summary>
        private DanceTreeViewItem? ShiftNode;

        /// <summary>
        /// 用于标记Control键取消选择的节点
        /// </summary>
        private DanceTreeViewItem? ControlCancelSelectedNode;

        // =======================================================================================
        // Property 

        #region IsMultiSelection -- 是否是多选

        /// <summary>
        /// 是否是多选
        /// </summary>
        public bool IsMultiSelection
        {
            get { return (bool)GetValue(IsMultiSelectionProperty); }
            set { SetValue(IsMultiSelectionProperty, value); }
        }

        /// <summary>
        /// 是否是多选
        /// </summary>
        public static readonly DependencyProperty IsMultiSelectionProperty =
            DependencyProperty.Register("IsMultiSelection", typeof(bool), typeof(DanceTreeView), new PropertyMetadata(false));

        #endregion

        /// <summary>
        /// 获取选中项的值
        /// </summary>
        public List<object> GetSelectedValues()
        {
            List<object> result = [];

            if (this.SelectedNodes.Count == 0)
            {
                return result;
            }

            foreach (DanceTreeViewItem item in this.SelectedNodes)
            {
                result.Add(item.DataContext);
            }

            return result;
        }

        /// <summary>
        /// 清理选中项
        /// </summary>
        public void ClearSelected()
        {
            foreach (DanceTreeViewItem item in this.SelectedNodes)
            {
                item.IsSelected = false;
            }

            this.SelectedNodes.Clear();
        }

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
            e.Handled = true;
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is not FrameworkElement element || DanceXamlExpansion.GetVisualTreeParent<DanceTreeViewItem>(element) is not DanceTreeViewItem item)
                return;

            this.UpdateSelectionBefore(item);
        }

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is not FrameworkElement element || DanceXamlExpansion.GetVisualTreeParent<DanceTreeViewItem>(element) is not DanceTreeViewItem item)
                return;

            this.UpdateSelectionAfter(item);
        }

        // =======================================================================================
        // Private Function

        /// <summary>
        /// 更新选择区域 -- 前置
        /// </summary>
        /// <param name="item">节点</param>
        private void UpdateSelectionBefore(DanceTreeViewItem item)
        {
            // 加选
            if (this.IsMultiSelection && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                this.UpdateSelectionBefore_Add(item);
                this.ShiftNode = null;
                return;
            }

            // 框选
            if (this.IsMultiSelection && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                this.UpdateSelectionBefore_Region(item);
                return;
            }

            // 正常
            this.UpdateSelectionBefore_Normal(item);
            this.ShiftNode = null;
        }

        /// <summary>
        /// 更新选择区域 -- 前置 -- 正常
        /// </summary>
        /// <param name="item">节点</param>
        private void UpdateSelectionBefore_Normal(DanceTreeViewItem item)
        {
            if (this.SelectedNodes.Contains(item))
                return;

            this.SelectedNodes.ForEach(p => p.IsSelected = p == item && p.IsSelected);
            this.SelectedNodes.Clear();
            item.IsSelected = true;
            this.SelectedNodes.Add(item);
        }

        /// <summary>
        /// 更新选择区域 -- 前置 -- 加选
        /// </summary>
        /// <param name="item">节点</param>
        private void UpdateSelectionBefore_Add(DanceTreeViewItem item)
        {
            if (!item.IsSelected)
            {
                item.IsSelected = true;
                this.SelectedNodes.Add(item);
            }
            else
            {
                this.ControlCancelSelectedNode = item;
            }
        }

        /// <summary>
        /// 更新选择区域 -- 前置 -- 区域
        /// </summary>
        /// <param name="item">节点</param>
        private void UpdateSelectionBefore_Region(DanceTreeViewItem item)
        {
            if (this.SelectedNodes.Count == 0)
            {
                item.IsSelected = true;
                this.SelectedNodes.Add(item);

                return;
            }
            else if (this.SelectedNodes.Count == 1 && this.SelectedNodes.Contains(item))
            {
                return;
            }

            this.ShiftNode ??= this.SelectedNodes.Last();
            List<DanceTreeViewItem> region = TraversalTreeViewItem(this);
            int point1 = region.IndexOf(this.ShiftNode);
            int point2 = region.IndexOf(item);
            int start = Math.Min(point1, point2);
            int end = Math.Max(point1, point2);
            region = region.Skip(start).Take(end - start + 1).ToList();

            this.SelectedNodes.ForEach(p => p.IsSelected = false);
            this.SelectedNodes.Clear();
            region.ForEach(p => p.IsSelected = true);
            this.SelectedNodes.AddRange(region);
        }

        /// <summary>
        /// 更新选择区域 -- 后置
        /// </summary>
        /// <param name="item">节点</param>
        private void UpdateSelectionAfter(DanceTreeViewItem item)
        {
            // 加选
            if (this.IsMultiSelection && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                this.UpdateSelectionAfter_Add();
                return;
            }

            // 框选
            if (this.IsMultiSelection && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                return;
            }

            // 正常
            this.UpdateSelectionAfter_Normal(item);
        }

        /// <summary>
        /// 更新选择区域 -- 后置 -- 正常
        /// </summary>
        /// <param name="item">节点</param>
        private void UpdateSelectionAfter_Normal(DanceTreeViewItem item)
        {
            this.SelectedNodes.ForEach(p => p.IsSelected = p == item && p.IsSelected);
            this.SelectedNodes.Clear();
            item.IsSelected = true;
            this.SelectedNodes.Add(item);
        }

        /// <summary>
        /// 更新选择区域 -- 后置 -- 加选
        /// </summary>
        private void UpdateSelectionAfter_Add()
        {
            if (this.ControlCancelSelectedNode != null)
            {
                this.ControlCancelSelectedNode.IsSelected = false;
                this.SelectedNodes.Remove(this.ControlCancelSelectedNode);
                this.ControlCancelSelectedNode = null;
            }
        }

        /// <summary>
        /// 使用可视化树查找子节点
        /// </summary>
        /// <param name="element">查找的开始控件</param>
        /// <returns>查找结果</returns>
        private static List<DanceTreeViewItem> TraversalTreeViewItem(DependencyObject? element)
        {
            List<DanceTreeViewItem> result = [];

            if (element == null)
                return result;

            if (element is DanceTreeViewItem item)
            {
                result.Add(item);

                if (!item.IsExpanded)
                    return result;
            }
            int count = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < count; ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);

                result.AddRange(TraversalTreeViewItem(child));
            }

            return result;
        }

    }
}