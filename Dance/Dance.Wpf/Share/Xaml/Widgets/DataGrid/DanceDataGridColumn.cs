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
    /// 表格列
    /// </summary>
    public class DanceDataGridColumn : DependencyObject
    {
        #region Width -- 宽度

        /// <summary>
        /// 宽度
        /// </summary>
        public GridLength Width
        {
            get { return (GridLength)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(GridLength), typeof(DanceDataGridColumn), new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

        #endregion

        #region MinWidth -- 最小宽度

        /// <summary>
        /// 最小宽度
        /// </summary>
        public double MinWindth
        {
            get { return (double)GetValue(MinWindthProperty); }
            set { SetValue(MinWindthProperty, value); }
        }

        /// <summary>
        /// 最小宽度
        /// </summary>
        public static readonly DependencyProperty MinWindthProperty =
            DependencyProperty.Register("MinWindth", typeof(double), typeof(DanceDataGridCellItem), new PropertyMetadata(60d));

        #endregion

        #region Header -- 标题

        /// <summary>
        /// 标题
        /// </summary>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(DanceDataGridColumn), new PropertyMetadata(null));

        #endregion

        #region CellTemplate -- 单元格模板

        /// <summary>
        /// 单元格模板
        /// </summary>
        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }

        /// <summary>
        /// 单元格模板
        /// </summary>
        public static readonly DependencyProperty CellTemplateProperty =
            DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(DanceDataGridColumn), new PropertyMetadata(null));

        #endregion

        #region ActualWidth -- 真实宽度

        /// <summary>
        /// 真实宽度
        /// </summary>
        public double ActualWidth
        {
            get { return (double)GetValue(ActualWidthProperty); }
            set { SetValue(ActualWidthProperty, value); }
        }

        /// <summary>
        /// 真实宽度
        /// </summary>
        public static readonly DependencyProperty ActualWidthProperty =
            DependencyProperty.Register("ActualWidth", typeof(double), typeof(DanceDataGridColumn), new PropertyMetadata(0d));

        #endregion
    }
}
