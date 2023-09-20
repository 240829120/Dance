using System;
using System.Collections;
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
    /// 表格单元格集合
    /// </summary>
    public class DanceDataGridCellItemsControl : ItemsControl
    {
        static DanceDataGridCellItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceDataGridCellItemsControl), new FrameworkPropertyMetadata(typeof(DanceDataGridCellItemsControl)));
        }

        /// <summary>
        /// 所属表格
        /// </summary>
        /// <param name="dataGrid">表格</param>
        public DanceDataGridCellItemsControl(DanceDataGrid dataGrid)
        {
            this.DataGrid = dataGrid;
            this.ItemsSource = dataGrid.Columns;
        }

        // ===========================================================================================================
        // Field

        /// <summary>
        /// 所属表格
        /// </summary>
        internal DanceDataGrid DataGrid;

        // ===========================================================================================================
        // Property

        #region IsSelected -- 是否被选中

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            internal set { SetValue(IsSelectedPropertyKey, value); }
        }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public static readonly DependencyPropertyKey IsSelectedPropertyKey =
            DependencyProperty.RegisterReadOnly("IsSelected", typeof(bool), typeof(DanceDataGridCellItemsControl), new PropertyMetadata(false));

        /// <summary>
        /// 是否被选中
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = IsSelectedPropertyKey.DependencyProperty;

        #endregion

        // ===========================================================================================================
        // Override Function

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceDataGridCellItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceDataGridCellItem(this.DataGrid, this, this.DataContext);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DataGrid.SelectedValue = this.DataContext;
        }
    }
}