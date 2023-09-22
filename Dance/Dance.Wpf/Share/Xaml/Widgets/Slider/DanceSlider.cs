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
    /// 滑块
    /// </summary>
    public class DanceSlider : Control
    {
        static DanceSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceSlider), new FrameworkPropertyMetadata(typeof(DanceSlider)));
        }

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
            DependencyProperty.Register("Minimum", typeof(double), typeof(DanceSlider), new PropertyMetadata(0d));

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
            DependencyProperty.Register("Maximum", typeof(double), typeof(DanceSlider), new PropertyMetadata(100d));

        #endregion

        #region MinValue -- 小值

        /// <summary>
        /// 小值
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        /// <summary>
        /// 小值
        /// </summary>
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(DanceSlider), new PropertyMetadata(0d));

        #endregion

        #region MaxValue -- 大值

        /// <summary>
        /// 大值
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        /// <summary>
        /// 大值
        /// </summary>
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(DanceSlider), new PropertyMetadata(0d));

        #endregion
    }
}
