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
    /// 面板
    /// </summary>
    public abstract class DancePanel : Panel
    {
        #region IsFloat -- 是否浮动

        /// <summary>
        /// 获取是否浮动
        /// </summary>
        public static bool GetIsFloat(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFloatProperty);
        }

        /// <summary>
        /// 设置是否浮动
        /// </summary>
        public static void SetIsFloat(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFloatProperty, value);
        }

        /// <summary>
        /// 是否浮动
        /// </summary>
        public static readonly DependencyProperty IsFloatProperty =
            DependencyProperty.RegisterAttached("IsFloat", typeof(bool), typeof(DancePanel), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                DanceXamlExpansion.GetVisualTreeParent<DancePanel>(element)?.InvalidateArrange();

            })));

        #endregion

        #region Left -- 浮动Left

        /// <summary>
        /// 获取浮动Left
        /// </summary>
        public static double GetLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(LeftProperty);
        }

        /// <summary>
        /// 设置浮动Left
        /// </summary>
        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }

        /// <summary>
        /// 浮动Left
        /// </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.RegisterAttached("Left", typeof(double), typeof(DancePanel), new PropertyMetadata(0d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                DanceXamlExpansion.GetVisualTreeParent<DancePanel>(element)?.InvalidateArrange();

            })));

        #endregion

        #region Top -- 浮动Top

        /// <summary>
        /// 获取浮动Top
        /// </summary>
        public static double GetTop(DependencyObject obj)
        {
            return (double)obj.GetValue(TopProperty);
        }

        /// <summary>
        /// 设置浮动Top
        /// </summary>
        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }

        /// <summary>
        /// 浮动Top
        /// </summary>
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.RegisterAttached("Top", typeof(double), typeof(DancePanel), new PropertyMetadata(0d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                DanceXamlExpansion.GetVisualTreeParent<DancePanel>(element)?.InvalidateArrange();

            })));

        #endregion
    }
}
