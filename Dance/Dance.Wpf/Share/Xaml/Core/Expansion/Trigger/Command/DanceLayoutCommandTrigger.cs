using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 布局触发器
    /// </summary>
    public class DanceLayoutCommandTrigger : DependencyObject
    {
        #region IsResponsiveLayoutEnabled -- 是否启用响应布局

        /// <summary>
        /// 获取是否启用响应布局
        /// </summary>
        public static bool GetIsResponsiveLayoutEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsResponsiveLayoutEnabledProperty);
        }

        /// <summary>
        /// 设置是否启用响应布局
        /// </summary>
        public static void SetIsResponsiveLayoutEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsResponsiveLayoutEnabledProperty, value);
        }

        /// <summary>
        /// 是否启用响应布局
        /// </summary>
        public static readonly DependencyProperty IsResponsiveLayoutEnabledProperty =
            DependencyProperty.RegisterAttached("IsResponsiveLayoutEnabled", typeof(bool), typeof(DanceLayoutCommandTrigger), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                bool register = false;
                if (e.NewValue is bool newValue)
                {
                    register = newValue;
                }

                element.SizeChanged -= Element_SizeChanged;
                element.Loaded -= Element_Loaded;
                if (register)
                {
                    element.SizeChanged += Element_SizeChanged;
                    element.Loaded += Element_Loaded;
                }

            })));

        /// <summary>
        /// 加载完成
        /// </summary>
        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            UpdateStatus(element);
        }

        /// <summary>
        /// 大小改变时触发
        /// </summary>
        private static void Element_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            UpdateStatus(element);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="element">元素</param>
        private static void UpdateStatus(FrameworkElement element)
        {
            double witdhSmall = GetWidthResponsiveLayoutSmall(element);
            double widthLarge = GetWidthResponsiveLayoutLarge(element);
            double heightSmall = GetHeightResponsiveLayoutSmall(element);
            double heightLarge = GetHeightResponsiveLayoutLarge(element);

            if (element.ActualWidth < witdhSmall)
                SetWidthResponsiveLayoutStatus(element, DanceLayoutCommandStatus.Small);
            else if (element.ActualWidth <= widthLarge)
                SetWidthResponsiveLayoutStatus(element, DanceLayoutCommandStatus.Normal);
            else
                SetWidthResponsiveLayoutStatus(element, DanceLayoutCommandStatus.Large);

            if (element.ActualHeight < heightSmall)
                SetHeightResponsiveLayoutStatus(element, DanceLayoutCommandStatus.Small);
            else if (element.ActualHeight <= heightLarge)
                SetHeightResponsiveLayoutStatus(element, DanceLayoutCommandStatus.Normal);
            else
                SetHeightResponsiveLayoutStatus(element, DanceLayoutCommandStatus.Large);
        }

        #endregion

        #region WidthResponsiveLayoutStatus -- 宽度响应式状态

        /// <summary>
        /// 获取宽度响应式状态
        /// </summary>
        public static DanceLayoutCommandStatus GetWidthResponsiveLayoutStatus(DependencyObject obj)
        {
            return (DanceLayoutCommandStatus)obj.GetValue(WidthResponsiveLayoutStatusProperty);
        }

        /// <summary>
        /// 设置宽度响应式状态
        /// </summary>
        public static void SetWidthResponsiveLayoutStatus(DependencyObject obj, DanceLayoutCommandStatus value)
        {
            obj.SetValue(WidthResponsiveLayoutStatusProperty, value);
        }

        /// <summary>
        /// 宽度响应式状态
        /// </summary>
        public static readonly DependencyProperty WidthResponsiveLayoutStatusProperty =
            DependencyProperty.RegisterAttached("WidthResponsiveLayoutStatus", typeof(DanceLayoutCommandStatus), typeof(DanceLayoutCommandTrigger), new PropertyMetadata(DanceLayoutCommandStatus.Normal));

        #endregion

        #region HeightResponsiveLayoutStatus -- 高度响应式状态

        /// <summary>
        /// 获取高度响应式状态
        /// </summary>
        public static DanceLayoutCommandStatus GetHeightResponsiveLayoutStatus(DependencyObject obj)
        {
            return (DanceLayoutCommandStatus)obj.GetValue(HeightResponsiveLayoutStatusProperty);
        }

        /// <summary>
        /// 设置高度响应式状态
        /// </summary>
        public static void SetHeightResponsiveLayoutStatus(DependencyObject obj, DanceLayoutCommandStatus value)
        {
            obj.SetValue(HeightResponsiveLayoutStatusProperty, value);
        }

        /// <summary>
        /// 高度响应式状态
        /// </summary>
        public static readonly DependencyProperty HeightResponsiveLayoutStatusProperty =
            DependencyProperty.RegisterAttached("HeightResponsiveLayoutStatus", typeof(DanceLayoutCommandStatus), typeof(DanceLayoutCommandTrigger), new PropertyMetadata(DanceLayoutCommandStatus.Normal));

        #endregion

        #region WidthResponsiveLayoutSmall -- 宽度响应式值 -- 小

        /// <summary>
        /// 获取宽度响应式值 -- 小
        /// </summary>
        public static double GetWidthResponsiveLayoutSmall(DependencyObject obj)
        {
            return (double)obj.GetValue(WidthResponsiveLayoutSmallProperty);
        }

        /// <summary>
        /// 设置宽度响应式值 -- 小
        /// </summary>
        public static void SetWidthResponsiveLayoutSmall(DependencyObject obj, double value)
        {
            obj.SetValue(WidthResponsiveLayoutSmallProperty, value);
        }

        /// <summary>
        /// 宽度响应式值 -- 小
        /// </summary>
        public static readonly DependencyProperty WidthResponsiveLayoutSmallProperty =
            DependencyProperty.RegisterAttached("WidthResponsiveLayoutSmall", typeof(double), typeof(DanceLayoutCommandTrigger), new PropertyMetadata(100d));

        #endregion

        #region WidthResponsiveLayoutLarge -- 宽度响应式值 -- 大

        /// <summary>
        /// 获取宽度响应式值 -- 大
        /// </summary>
        public static double GetWidthResponsiveLayoutLarge(DependencyObject obj)
        {
            return (double)obj.GetValue(WidthResponsiveLayoutLargeProperty);
        }

        /// <summary>
        /// 设置宽度响应式值 -- 大
        /// </summary>
        public static void SetWidthResponsiveLayoutLarge(DependencyObject obj, double value)
        {
            obj.SetValue(WidthResponsiveLayoutLargeProperty, value);
        }

        /// <summary>
        /// 宽度响应式值 -- 大
        /// </summary>
        public static readonly DependencyProperty WidthResponsiveLayoutLargeProperty =
            DependencyProperty.RegisterAttached("WidthResponsiveLayoutLarge", typeof(double), typeof(DanceLayoutCommandTrigger), new PropertyMetadata(1000d));

        #endregion

        #region HeightResponsiveLayoutSmall -- 高度响应式值 -- 小

        /// <summary>
        /// 获取高度响应式值 -- 小
        /// </summary>
        public static double GetHeightResponsiveLayoutSmall(DependencyObject obj)
        {
            return (double)obj.GetValue(HeightResponsiveLayoutSmallProperty);
        }

        /// <summary>
        /// 设置高度响应式值 -- 小
        /// </summary>
        public static void SetHeightResponsiveLayoutSmall(DependencyObject obj, double value)
        {
            obj.SetValue(HeightResponsiveLayoutSmallProperty, value);
        }

        /// <summary>
        /// 高度响应式值 -- 小
        /// </summary>
        public static readonly DependencyProperty HeightResponsiveLayoutSmallProperty =
            DependencyProperty.RegisterAttached("HeightResponsiveLayoutSmall", typeof(double), typeof(DanceLayoutCommandTrigger), new PropertyMetadata(100d));

        #endregion

        #region HeightResponsiveLayoutLarge -- 高度响应式值 -- 大


        /// <summary>
        /// 获取高度响应式值 -- 大
        /// </summary>
        public static double GetHeightResponsiveLayoutLarge(DependencyObject obj)
        {
            return (double)obj.GetValue(HeightResponsiveLayoutLargeProperty);
        }

        /// <summary>
        /// 设置高度响应式值 -- 大
        /// </summary>
        public static void SetHeightResponsiveLayoutLarge(DependencyObject obj, double value)
        {
            obj.SetValue(HeightResponsiveLayoutLargeProperty, value);
        }

        /// <summary>
        /// 高度响应式值 -- 大
        /// </summary>
        public static readonly DependencyProperty HeightResponsiveLayoutLargeProperty =
            DependencyProperty.RegisterAttached("HeightResponsiveLayoutLarge", typeof(double), typeof(DanceLayoutCommandTrigger), new PropertyMetadata(1000d));

        #endregion
    }
}
