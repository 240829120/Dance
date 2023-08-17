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
    /// 手势识别控件
    /// </summary>
    public class DanceGestureRecognizer : DependencyObject
    {
        #region Swipe -- 轻扫

        /// <summary>
        /// 获取轻扫
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>轻扫手势</returns>
        public static DanceSwipeGestureRecognizer GetSwipe(DependencyObject obj)
        {
            return (DanceSwipeGestureRecognizer)obj.GetValue(SwipeProperty);
        }

        /// <summary>
        /// 设置轻扫
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="value">轻扫手势</param>
        public static void SetSwipe(DependencyObject obj, DanceSwipeGestureRecognizer value)
        {
            obj.SetValue(SwipeProperty, value);
        }

        /// <summary>
        /// 轻扫
        /// </summary>
        public static readonly DependencyProperty SwipeProperty =
            DependencyProperty.RegisterAttached("Swipe", typeof(DanceSwipeGestureRecognizer), typeof(DanceGestureRecognizer), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (e.OldValue is IDanceGestureRecognizer oldValue)
                {
                    oldValue.UnRegister();
                }

                if (s is FrameworkElement element && e.NewValue is IDanceGestureRecognizer newValue)
                {
                    newValue.Register(element);
                }

            })));

        #endregion
    }
}