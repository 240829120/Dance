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
    /// 滑块连接连接
    /// </summary>
    public class DanceSliderLink : Control
    {
        static DanceSliderLink()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceSliderLink), new FrameworkPropertyMetadata(typeof(DanceSliderLink)));
        }

        #region MinValueThumb -- 小值滑块

        /// <summary>
        /// 小值滑块
        /// </summary>
        public DanceSliderThumb MinValueThumb
        {
            get { return (DanceSliderThumb)GetValue(MinValueThumbProperty); }
            set { SetValue(MinValueThumbProperty, value); }
        }

        /// <summary>
        /// 小值滑块
        /// </summary>
        public static readonly DependencyProperty MinValueThumbProperty =
            DependencyProperty.Register("MinValueThumb", typeof(DanceSliderThumb), typeof(DanceSliderLink), new PropertyMetadata(null));

        #endregion

        #region MaxValueThumb -- 大值滑块

        /// <summary>
        /// 大值滑块
        /// </summary>
        public DanceSliderThumb MaxValueThumb
        {
            get { return (DanceSliderThumb)GetValue(MaxValueThumbProperty); }
            set { SetValue(MaxValueThumbProperty, value); }
        }

        /// <summary>
        /// 大值滑块
        /// </summary>
        public static readonly DependencyProperty MaxValueThumbProperty =
            DependencyProperty.Register("MaxValueThumb", typeof(DanceSliderThumb), typeof(DanceSliderLink), new PropertyMetadata(null));

        #endregion
    }
}
