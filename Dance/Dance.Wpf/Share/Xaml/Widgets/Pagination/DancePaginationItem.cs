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
    /// 分页项
    /// </summary>
    public class DancePaginationItem : ContentControl
    {
        static DancePaginationItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DancePaginationItem), new FrameworkPropertyMetadata(typeof(DancePaginationItem)));
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (this.DataContext is not DancePaginationInfo info || DanceXamlExpansion.GetVisualTreeParent<DancePagination>(this) is not DancePagination pagination)
                return;

            pagination.ChangePage(info);
        }
    }
}
