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
    /// 数据表格布局
    /// </summary>
    public class DanceDataGridPanel : Panel
    {
        #region ColumnMinWidth -- 列最小宽度

        /// <summary>
        /// 列最小宽度
        /// </summary>
        public double ColumnMinWidth
        {
            get { return (double)GetValue(ColumnMinWidthProperty); }
            set { SetValue(ColumnMinWidthProperty, value); }
        }

        /// <summary>
        /// 列最小宽度
        /// </summary>
        public static readonly DependencyProperty ColumnMinWidthProperty =
            DependencyProperty.Register("ColumnMinWidth", typeof(double), typeof(DanceDataGridPanel), new PropertyMetadata(20d));

        #endregion

        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="availableSize">可用区域</param>
        /// <returns>测量结果</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new();

            foreach (UIElement item in this.Children)
            {
                item.Measure(availableSize);

                size.Width += item.DesiredSize.Width;
            }

            size.Width = Math.Max(size.Width, availableSize.Width);
            size.Height = availableSize.Height;

            return size;
        }

        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="finalSize">可用区域</param>
        /// <returns>布局结果</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = new();

            // 统计
            List<DanceDataGridPanelLayoutInfo> infos = new();

            foreach (UIElement item in this.Children)
            {
                if (item is not FrameworkElement element || element.DataContext is not DanceDataGridColumn column)
                    continue;

                infos.Add(new DanceDataGridPanelLayoutInfo(element, column));
            }

            foreach (DanceDataGridPanelLayoutInfo info in infos.Where(p => p.Column.Width.GridUnitType == GridUnitType.Pixel))
            {
                info.Width = info.Column.Width.Value;
                size.Width += info.Width;
            }

            foreach (DanceDataGridPanelLayoutInfo info in infos.Where(p => p.Column.Width.GridUnitType == GridUnitType.Auto))
            {
                info.Width = info.Element.DesiredSize.Width;
                size.Width += info.Width;
            }

            double starSum = infos.Where(p => p.Column.Width.GridUnitType == GridUnitType.Star).Sum(p => p.Column.Width.Value);

            if (starSum > 0)
            {
                double starOneWidth = (finalSize.Width - size.Width) / starSum;

                foreach (DanceDataGridPanelLayoutInfo info in infos.Where(p => p.Column.Width.GridUnitType == GridUnitType.Star))
                {
                    info.Width = Math.Max(this.ColumnMinWidth, starOneWidth * info.Column.Width.Value);
                    size.Width += info.Width;
                }
            }

            size.Height = finalSize.Height;

            // 布局
            double x = 0;
            foreach (DanceDataGridPanelLayoutInfo info in infos.Where(p => p.Column.Width.GridUnitType == GridUnitType.Auto))
            {
                info.Element.Arrange(new Rect(x, 0, info.Width, size.Height));
                x += info.Width;
            }

            return size;
        }

        /// <summary>
        /// 布局信息
        /// </summary>
        private class DanceDataGridPanelLayoutInfo
        {
            /// <summary>
            /// 布局信息
            /// </summary>
            /// <param name="element">元素</param>
            /// <param name="column">列</param>
            public DanceDataGridPanelLayoutInfo(FrameworkElement element, DanceDataGridColumn column)
            {
                this.Element = element;
                this.Column = column;
            }

            /// <summary>
            /// 元素
            /// </summary>
            public FrameworkElement Element { get; private set; }

            /// <summary>
            /// 列信息
            /// </summary>
            public DanceDataGridColumn Column { get; private set; }

            /// <summary>
            /// 宽度
            /// </summary>
            public double Width { get; set; }
        }
    }
}
