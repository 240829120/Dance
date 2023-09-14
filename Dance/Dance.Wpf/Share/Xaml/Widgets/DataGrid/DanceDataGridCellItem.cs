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
    /// 单元格项
    /// </summary>
    public class DanceDataGridCellItem : Control
    {
        static DanceDataGridCellItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceDataGridCellItem), new FrameworkPropertyMetadata(typeof(DanceDataGridCellItem)));
        }

        /// <summary>
        /// 单元格项
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="cellItemsControl"></param>
        /// <param name="model">所属模型</param>
        public DanceDataGridCellItem(DanceDataGrid dataGrid, DanceDataGridCellItemsControl cellItemsControl, object? model)
        {
            this.DataGrid = dataGrid;
            this.CellItemsControl = cellItemsControl;
            this.Model = model;
        }

        /// <summary>
        /// 所属表格
        /// </summary>
        internal DanceDataGrid DataGrid { get; private set; }

        /// <summary>
        /// 所属单元格集合
        /// </summary>
        internal DanceDataGridCellItemsControl CellItemsControl { get; private set; }

        #region Model -- 模型

        /// <summary>
        /// 模型
        /// </summary>
        public object? Model
        {
            get { return (object?)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        /// <summary>
        /// 模型
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(object), typeof(DanceDataGridCellItem), new PropertyMetadata(null));

        #endregion
    }
}
