using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 窗口触发器
    /// </summary>
    public class DanceWindowTrigger : DependencyObject
    {
        #region SetWidth -- 设置宽度

        /// <summary>
        /// 获取设置宽度
        /// </summary>
        public static double GetSetWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(SetWidthProperty);
        }

        /// <summary>
        /// 设置设置宽度
        /// </summary>
        public static void SetSetWidth(DependencyObject obj, double value)
        {
            obj.SetValue(SetWidthProperty, value);
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        public static readonly DependencyProperty SetWidthProperty =
            DependencyProperty.RegisterAttached("SetWidth", typeof(double), typeof(DanceWindowTrigger), new PropertyMetadata(0d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element || Window.GetWindow(element) is not Window window || e.NewValue is not double newValue)
                    return;

                window.Width = newValue;
            })));

        #endregion

        #region SetHeight -- 设置高度

        /// <summary>
        /// 获取设置高度
        /// </summary>
        public static double GetSetHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(SetHeightProperty);
        }

        /// <summary>
        /// 设置设置高度
        /// </summary>
        public static void SetSetHeight(DependencyObject obj, double value)
        {
            obj.SetValue(SetHeightProperty, value);
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        public static readonly DependencyProperty SetHeightProperty =
            DependencyProperty.RegisterAttached("SetHeight", typeof(double), typeof(DanceWindowTrigger), new PropertyMetadata(0d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element || Window.GetWindow(element) is not Window window || e.NewValue is not double newValue)
                    return;

                window.Height = newValue;
            })));

        #endregion

        #region SetTitle -- 设置标题

        /// <summary>
        /// 获取设置标题
        /// </summary>
        public static string GetSetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(SetTitleProperty);
        }

        /// <summary>
        /// 设置设置标题
        /// </summary>
        public static void SetSetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(SetTitleProperty, value);
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        public static readonly DependencyProperty SetTitleProperty =
            DependencyProperty.RegisterAttached("SetTitle", typeof(string), typeof(DanceWindowTrigger), new PropertyMetadata(null));

        #endregion
    }
}
