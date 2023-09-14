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


        // ===========================================================================================================
        // Property

        /// <summary>
        /// 所属表格
        /// </summary>
        internal DanceDataGrid DataGrid { get; private set; }

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
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(DanceDataGridCellItemsControl), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceDataGridCellItemsControl cellItems)
                    return;

                cellItems.DataGrid.SelectedValue = cellItems.DataContext;
            })));

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

            this.IsSelected = true;
        }
    }
}
