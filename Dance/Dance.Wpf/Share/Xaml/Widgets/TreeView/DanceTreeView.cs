using System;
using System.Collections;
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

        // =======================================================================================
        // Field

        /// <summary>
        /// 选中的项集合
        /// </summary>
        internal List<DanceTreeViewItem> SelectedItems = new();

        // =======================================================================================
        // Property

        #region IsMultiSelectedEnabled -- 是否启用多选

        /// <summary>
        /// 是否启用多选
        /// </summary>
        public bool IsMultiSelectedEnabled
        {
            get { return (bool)GetValue(IsMultiSelectedEnabledProperty); }
            set { SetValue(IsMultiSelectedEnabledProperty, value); }
        }

        /// <summary>
        /// 是否启用多选
        /// </summary>
        public static readonly DependencyProperty IsMultiSelectedEnabledProperty =
            DependencyProperty.Register("IsMultiSelectedEnabled", typeof(bool), typeof(DanceTreeView), new PropertyMetadata(false));

        #endregion

        #region SlectedValue -- 当前选中项

        /// <summary>
        /// 当前选中项
        /// </summary>
        public object SlectedValue
        {
            get { return (object)GetValue(SlectedValueProperty); }
            set { SetValue(SlectedValueProperty, value); }
        }

        /// <summary>
        /// 当前选中项
        /// </summary>
        public static readonly DependencyProperty SlectedValueProperty =
            DependencyProperty.Register("SlectedValue", typeof(object), typeof(DanceTreeView), new PropertyMetadata(null));

        #endregion

        #region SlectedValues -- 当前选中项集合

        /// <summary>
        /// 当前选中项集合
        /// </summary>
        public IList SelectedValues
        {
            get { return (IList)GetValue(SelectedValuesProperty); }
            set { SetValue(SelectedValuesProperty, value); }
        }

        /// <summary>
        /// 当前选中项集合
        /// </summary>
        public static readonly DependencyProperty SelectedValuesProperty =
            DependencyProperty.Register("SelectedValues", typeof(IList), typeof(DanceTreeView), new PropertyMetadata(null));

        #endregion

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
    }
}
