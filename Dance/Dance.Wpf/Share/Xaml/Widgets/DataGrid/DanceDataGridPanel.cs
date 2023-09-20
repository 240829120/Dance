﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 数据表格布局
    /// </summary>
    public class DanceDataGridPanel : Panel
    {
        #region IsColumnHeader -- 是否是列头

        /// <summary>
        /// 是否是列头
        /// </summary>
        public bool IsColumnHeader
        {
            get { return (bool)GetValue(IsColumnHeaderProperty); }
            set { SetValue(IsColumnHeaderProperty, value); }
        }

        /// <summary>
        /// 是否是列头
        /// </summary>
        public static readonly DependencyProperty IsColumnHeaderProperty =
            DependencyProperty.Register("IsColumnHeader", typeof(bool), typeof(DanceDataGridPanel), new PropertyMetadata(false));

        #endregion

        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="availableSize">可用区域</param>
        /// <returns>测量结果</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new();

            // 非列头
            if (!this.IsColumnHeader)
            {
                foreach (FrameworkElement element in this.Children)
                {
                    if (element == null || element.DataContext is not DanceDataGridColumn column)
                        continue;

                    size.Width += column.ActualWidth;
                }

                return size;
            }

            // 列头
            DanceDataGridColumnItemsControl? headers = DanceXamlExpansion.GetVisualTreeParent<DanceDataGridColumnItemsControl>(this);
            if (headers == null || headers.ActualHeight == 0)
                return size;

            // 统计
            List<DanceDataGridColumn> starColumns = new();
            double stars = 0;

            foreach (FrameworkElement element in this.Children)
            {
                if (element == null || element.DataContext is not DanceDataGridColumn column)
                    continue;

                element.Measure(availableSize);

                switch (column.Width.GridUnitType)
                {
                    case GridUnitType.Auto:
                        column.ActualWidth = element.DesiredSize.Width;
                        size.Width += element.DesiredSize.Width;
                        break;
                    case GridUnitType.Pixel:
                        column.ActualWidth = column.Width.Value;
                        size.Width += column.Width.Value;
                        break;
                    case GridUnitType.Star:
                        starColumns.Add(column);
                        stars += column.Width.Value;
                        break;
                    default:
                        break;
                }
            }

            if (starColumns.Count > 0)
            {
                double starOneWidth = (headers.ActualWidth - size.Width) / stars;
                foreach (DanceDataGridColumn column in starColumns)
                {
                    column.ActualWidth = Math.Max(column.MinWindth, starOneWidth * column.Width.Value);
                    size.Width += column.ActualWidth;
                }
            }

            size.Height = headers.ActualHeight;

            return size;
        }

        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="finalSize">可用区域</param>
        /// <returns>布局结果</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = new()
            {
                Height = finalSize.Height
            };

            // 布局
            foreach (FrameworkElement element in this.Children)
            {
                if (element == null || element.DataContext is not DanceDataGridColumn column)
                    continue;

                element.Arrange(new Rect(size.Width, 0, column.ActualWidth, size.Height));
                size.Width += column.ActualWidth;
            }

            return size;
        }
    }
}