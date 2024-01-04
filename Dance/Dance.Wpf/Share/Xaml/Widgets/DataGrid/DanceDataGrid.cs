using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using log4net;

namespace Dance.Wpf
{
    /// <summary>
    /// 表格
    /// </summary>
    [TemplatePart(Name = nameof(PART_ScrollViewer_Header), Type = typeof(ScrollViewer))]
    [TemplatePart(Name = nameof(PART_ScrollViewer_Items), Type = typeof(ScrollViewer))]
    public class DanceDataGrid : ItemsControl
    {
        static DanceDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceDataGrid), new FrameworkPropertyMetadata(typeof(DanceDataGrid)));
        }

        public DanceDataGrid()
        {
            this.Columns = [];
        }

        // =================================================================================================================
        // Field

        /// <summary>
        /// 列头
        /// </summary>
        private ScrollViewer? PART_ScrollViewer_Header;

        /// <summary>
        /// 数据项
        /// </summary>
        private ScrollViewer? PART_ScrollViewer_Items;

        // =================================================================================================================
        // Property

        #region Columns -- 列集合

        /// <summary>
        /// 列集合
        /// </summary>
        public ObservableCollection<DanceDataGridColumn> Columns
        {
            get { return (ObservableCollection<DanceDataGridColumn>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// 列集合
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(ObservableCollection<DanceDataGridColumn>), typeof(DanceDataGrid), new PropertyMetadata(null));

        #endregion

        #region RowHeight -- 行高

        /// <summary>
        /// 行高
        /// </summary>
        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }

        /// <summary>
        /// 行高
        /// </summary>
        public static readonly DependencyProperty RowHeightProperty =
            DependencyProperty.Register("RowHeight", typeof(double), typeof(DanceDataGrid), new PropertyMetadata(30d));

        #endregion

        #region SelectedValue -- 当前选中项

        /// <summary>
        /// 当前选中项
        /// </summary>
        public object? SelectedValue
        {
            get { return (object?)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        /// <summary>
        /// 当前选中项
        /// </summary>
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(DanceDataGrid), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceDataGrid dataGrid)
                    return;

                if (e.OldValue != null && dataGrid.ItemContainerGenerator.ContainerFromItem(e.OldValue) is DanceDataGridCellItemsControl oldContainer)
                {
                    oldContainer.IsSelected = false;
                }

                if (e.NewValue != null && dataGrid.ItemContainerGenerator.ContainerFromItem(e.NewValue) is DanceDataGridCellItemsControl newContainer)
                {
                    newContainer.IsSelected = true;
                    dataGrid.SelectedIndex = dataGrid.ItemContainerGenerator.IndexFromContainer(newContainer);
                }
                else
                {
                    dataGrid.SelectedIndex = -1;
                }

            })));

        #endregion

        #region SelectedIndex -- 当前选中索引

        /// <summary>
        /// 当前选中索引
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexPropertyKey.DependencyProperty); }
            private set { SetValue(SelectedIndexPropertyKey, value); }
        }

        /// <summary>
        /// 当前选中索引
        /// </summary>
        public static readonly DependencyPropertyKey SelectedIndexPropertyKey =
            DependencyProperty.RegisterReadOnly("SelectedIndex", typeof(int), typeof(DanceDataGrid), new PropertyMetadata(-1));

        #endregion

        // =================================================================================================================
        // Protected Function

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceDataGridCellItemsControl;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceDataGridCellItemsControl(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_ScrollViewer_Header = this.Template.FindName(nameof(PART_ScrollViewer_Header), this) as ScrollViewer;
            this.PART_ScrollViewer_Items = this.Template.FindName(nameof(PART_ScrollViewer_Items), this) as ScrollViewer;

            if (this.PART_ScrollViewer_Header != null && this.PART_ScrollViewer_Items != null)
            {
                this.PART_ScrollViewer_Items.ScrollChanged -= PART_ScrollViewer_Items_ScrollChanged;
                this.PART_ScrollViewer_Items.ScrollChanged += PART_ScrollViewer_Items_ScrollChanged;
            }
        }

        private void PART_ScrollViewer_Items_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (this.PART_ScrollViewer_Header == null || this.PART_ScrollViewer_Items == null)
                return;

            this.PART_ScrollViewer_Header?.ScrollToHorizontalOffset(this.PART_ScrollViewer_Items.HorizontalOffset);
        }
    }
}
