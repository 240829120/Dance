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
        #region IsMouseDownFocus -- 鼠标按下时聚焦

        /// <summary>
        /// 获取鼠标按下时聚焦
        /// </summary>
        public static bool GetIsMouseDownFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMouseDownFocusProperty);
        }

        /// <summary>
        /// 设置鼠标按下时聚焦
        /// </summary>
        public static void SetIsMouseDownFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMouseDownFocusProperty, value);
        }

        /// <summary>
        /// 鼠标按下时聚焦
        /// </summary>
        public static readonly DependencyProperty IsMouseDownFocusProperty =
            DependencyProperty.RegisterAttached("IsMouseDownFocus", typeof(bool), typeof(DanceFocusTrigger), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                bool register = false;
                if (e.NewValue is bool newValue)
                {
                    register = newValue;
                }

                element.PreviewMouseDown -= Element_PreviewMouseDown;
                if (register)
                {
                    element.PreviewMouseDown += Element_PreviewMouseDown;
                }
            })));

        private static void Element_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            element.Focus();
        }

        #endregion
    }
}
