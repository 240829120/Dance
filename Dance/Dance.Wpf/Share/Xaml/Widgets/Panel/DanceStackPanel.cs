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
    /// 栈面板
    /// </summary>
    public class DanceStackPanel : DancePanel
    {
        #region Orientation -- 方向

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// 方向
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DanceStackPanel), new PropertyMetadata(Orientation.Vertical));

        #endregion

        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="availableSize">可用大小</param>
        /// <returns>大小</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new();

            if (this.Orientation == Orientation.Vertical)
            {
                foreach (UIElement element in this.Children)
                {
                    element.Measure(availableSize);
                    if (GetIsFloat(element))
                        continue;

                    size.Width = Math.Max(size.Width, element.DesiredSize.Width);
                    size.Height += element.DesiredSize.Height;
                }
            }
            else
            {
                foreach (UIElement element in this.Children)
                {
                    element.Measure(availableSize);
                    if (GetIsFloat(element))
                        continue;

                    size.Height = Math.Max(size.Height, element.DesiredSize.Height);
                    size.Width += element.DesiredSize.Width;
                }
            }

            size.Width = Math.Max(size.Width, availableSize.Width);
            size.Height = Math.Max(size.Height, availableSize.Height);

            return availableSize;
        }

        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="finalSize">可用大小</param>
        /// <returns>布局大小</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = new();

            if (this.Orientation == Orientation.Vertical)
            {
                foreach (UIElement element in this.Children)
                {
                    if (GetIsFloat(element))
                    {
                        element.Arrange(new(GetLeft(element), GetTop(element), element.DesiredSize.Width, element.DesiredSize.Height));
                    }
                    else
                    {
                        element.Arrange(new(0, size.Height, finalSize.Width, element.DesiredSize.Height));
                        size.Height += element.DesiredSize.Height;
                    }
                }
            }
            else
            {
                foreach (UIElement element in this.Children)
                {
                    if (GetIsFloat(element))
                    {
                        element.Arrange(new(GetLeft(element), GetTop(element), element.DesiredSize.Width, element.DesiredSize.Height));
                    }
                    else
                    {
                        element.Arrange(new(size.Width, 0, element.DesiredSize.Width, finalSize.Height));
                        size.Width += element.DesiredSize.Width;
                    }
                }
            }

            size.Width = Math.Max(size.Width, finalSize.Width);
            size.Height = Math.Max(size.Height, finalSize.Height);

            return size;
        }
    }
}
