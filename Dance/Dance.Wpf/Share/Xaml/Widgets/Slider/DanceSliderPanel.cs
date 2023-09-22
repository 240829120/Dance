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
    /// 滑块布局
    /// </summary>
    public class DanceSliderPanel : Panel
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
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DanceSliderPanel), new PropertyMetadata(Orientation.Horizontal));

        #endregion

        #region Minimum -- 最小值

        /// <summary>
        /// 最小值
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(DanceSliderPanel), new PropertyMetadata(0d));

        #endregion

        #region Maximum -- 最大值

        /// <summary>
        /// 最大值
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(DanceSliderPanel), new PropertyMetadata(100d));

        #endregion

        #region Value -- 值

        /// <summary>
        /// 获取值
        /// </summary>
        public static double GetValue(DependencyObject obj)
        {
            return (double)obj.GetValue(ValueProperty);
        }

        /// <summary>
        /// 设置值
        /// </summary>
        public static void SetValue(DependencyObject obj, double value)
        {
            obj.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// 值
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(double), typeof(DanceSliderPanel), new PropertyMetadata(0d));

        #endregion

        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="availableSize">可用区域</param>
        /// <returns>测量结果</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (FrameworkElement element in this.Children)
            {
                element.Measure(availableSize);
            }

            return availableSize;
        }

        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="finalSize">可用区域</param>
        /// <returns>布局结果</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            double length = this.Maximum - this.Minimum;

            if (length < 0)
                return finalSize;

            if (this.Orientation == Orientation.Vertical)
            {
                double one = finalSize.Height / length;
                List<DanceSliderLink> links = new();

                foreach (FrameworkElement element in this.Children)
                {
                    if (element is DanceSliderLink link)
                    {
                        links.Add(link);

                        continue;
                    }

                    double value = GetValue(element);

                    element.Arrange(new(0, value * one - element.DesiredSize.Height / 2d, element.DesiredSize.Width, element.DesiredSize.Height));
                }

                foreach (DanceSliderLink link in links)
                {
                    if (link.MinValueThumb == null || link.MaxValueThumb == null)
                        continue;

                    double minValue = Math.Min(GetValue(link.MinValueThumb), GetValue(link.MaxValueThumb));
                    double maxValue = Math.Max(GetValue(link.MinValueThumb), GetValue(link.MaxValueThumb));

                    link.Arrange(new((this.DesiredSize.Width - link.DesiredSize.Width) / 2d, minValue * one, link.DesiredSize.Width, (maxValue - minValue) * one));
                }
            }
            else
            {
                double one = finalSize.Width / length;
                List<DanceSliderLink> links = new();

                foreach (FrameworkElement element in this.Children)
                {
                    if (element is DanceSliderLink link)
                    {
                        links.Add(link);

                        continue;
                    }

                    double value = GetValue(element);

                    element.Arrange(new(value * one - element.DesiredSize.Width / 2d, 0, element.DesiredSize.Width, element.DesiredSize.Height));
                }

                foreach (DanceSliderLink link in links)
                {
                    if (link.MinValueThumb == null || link.MaxValueThumb == null)
                        continue;

                    double minValue = Math.Min(GetValue(link.MinValueThumb), GetValue(link.MaxValueThumb));
                    double maxValue = Math.Max(GetValue(link.MinValueThumb), GetValue(link.MaxValueThumb));

                    link.Arrange(new(minValue * one, (this.DesiredSize.Height - link.DesiredSize.Height) / 2d, (maxValue - minValue) * one, link.DesiredSize.Height));
                }
            }

            return finalSize;
        }
    }
}
