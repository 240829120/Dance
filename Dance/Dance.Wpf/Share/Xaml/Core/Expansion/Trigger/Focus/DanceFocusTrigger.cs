using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 聚焦触发器
    /// </summary>
    public class DanceFocusTrigger : DependencyObject
    {
        #region IsMouseUpFocus -- 鼠标抬起时是否聚焦

        /// <summary>
        /// /获取鼠标抬起时是否聚焦
        /// </summary>
        public static bool GetIsMouseUpFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMouseUpFocusProperty);
        }

        /// <summary>
        /// 设置鼠标抬起时是否聚焦
        /// </summary>
        public static void SetIsMouseUpFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMouseUpFocusProperty, value);
        }

        /// <summary>
        /// 鼠标抬起时是否聚焦
        /// </summary>
        public static readonly DependencyProperty IsMouseUpFocusProperty =
            DependencyProperty.RegisterAttached("IsMouseUpFocus", typeof(bool), typeof(DanceFocusTrigger), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                bool register = false;
                if (e.NewValue is bool newValue)
                {
                    register = newValue;
                }

                element.MouseUp += Element_MouseUp;
                if (register)
                {
                    element.MouseUp += Element_MouseUp;
                }
            })));

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        private static void Element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            element.Focus();
        }

        #endregion
    }
}
