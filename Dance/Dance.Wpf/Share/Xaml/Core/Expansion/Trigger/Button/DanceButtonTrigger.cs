using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace Dance.Wpf
{
    /// <summary>
    /// 按钮触发器
    /// </summary>
    public class DanceButtonTrigger : DependencyObject
    {
        #region CallPrivateFunctionTarget -- 调用私有方法目标元素

        /// <summary>
        /// 获取调用私有方法目标元素
        /// </summary>
        public static FrameworkElement GetCallPrivateFunctionTarget(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(CallPrivateFunctionTargetProperty);
        }

        /// <summary>
        /// 设置调用私有方法目标元素
        /// </summary>
        public static void SetCallPrivateFunctionTarget(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(CallPrivateFunctionTargetProperty, value);
        }

        /// <summary>
        /// 调用私有方法目标元素
        /// </summary>
        public static readonly DependencyProperty CallPrivateFunctionTargetProperty =
            DependencyProperty.RegisterAttached("CallPrivateFunctionTarget", typeof(FrameworkElement), typeof(DanceButtonTrigger), new PropertyMetadata(null));

        #endregion

        #region CallPrivateFunction -- 调用私有方法

        /// <summary>
        /// 获取调用私有方法
        /// </summary>
        public static string GetCallPrivateFunction(DependencyObject obj)
        {
            return (string)obj.GetValue(CallPrivateFunctionProperty);
        }

        /// <summary>
        /// 设置调用私有方法
        /// </summary>
        public static void SetCallPrivateFunction(DependencyObject obj, string value)
        {
            obj.SetValue(CallPrivateFunctionProperty, value);
        }

        /// <summary>
        /// 调用私有方法
        /// </summary>
        public static readonly DependencyProperty CallPrivateFunctionProperty =
            DependencyProperty.RegisterAttached("CallPrivateFunction", typeof(string), typeof(DanceButtonTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not Button button)
                    return;

                button.Click -= Button_Click_CallPrivateFunction;
                button.Click += Button_Click_CallPrivateFunction;
            })));

        /// <summary>
        /// 调用私有方法
        /// </summary>
        private static void Button_Click_CallPrivateFunction(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            if (GetCallPrivateFunctionTarget(button) is not FrameworkElement target)
                return;

            if (GetCallPrivateFunction(button) is not string function || string.IsNullOrWhiteSpace(function))
                return;

            if (target.GetType().GetMethod(function, BindingFlags.Instance | BindingFlags.NonPublic) is not MethodInfo mi)
                return;

            mi.Invoke(target, new object[] { sender, e });
        }

        #endregion
    }
}
