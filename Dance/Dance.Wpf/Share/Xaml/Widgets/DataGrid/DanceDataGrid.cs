using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Dance.Wpf
{
    /// <summary>
    /// 表格
    /// </summary>
    public class DanceDataGrid : ItemsControl
    {
        static DanceDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceDataGrid), new FrameworkPropertyMetadata(typeof(DanceDataGrid)));
        }

        public DanceDataGrid()
        {
            this.Columns = new ObservableCollection<DanceDataGridColumn>();
        }

        #region Columns -- 列集合

        /// <summary>
        /// 列集合
        /// </summary>
        public IEnumerable Columns
        {
            get { return (IEnumerable)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// 列集合
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(IEnumerable), typeof(DanceDataGrid), new PropertyMetadata(null));

        #endregion
    }
}
