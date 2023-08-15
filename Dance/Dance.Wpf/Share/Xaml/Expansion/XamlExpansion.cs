using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// Xaml扩展
    /// </summary>
    public static class XamlExpansion
    {
        /// <summary>
        /// 使用可视化树查找父级控件
        /// </summary>
        /// <typeparam name="T">要查找的控件类型</typeparam>
        /// <param name="element">查找的开始控件</param>
        /// <returns>查找结果</returns>
        public static T? GetVisualTreeParent<T>(this DependencyObject element) where T : DependencyObject
        {
            return GetVisualTreeParent(element, typeof(T)) as T;
        }

        /// <summary>
        /// 使用可视化树查找父级控件
        /// </summary>
        /// <param name="type">父元素类型</param>
        /// <param name="element">查找的开始控件</param>
        /// <returns>查找结果</returns>
        public static object? GetVisualTreeParent(this DependencyObject element, Type type)
        {
            if (element == null)
                return null;

            if (type.IsAssignableFrom(element.GetType()))
                return element;

            return GetVisualTreeParent(VisualTreeHelper.GetParent(element), type);
        }

        /// <summary>
        /// 使用可视化树查找子控件
        /// </summary>
        /// <param name="element">查找的开始控件</param>
        /// <param name="type">子控件类型</param>
        /// <returns>查找结果</returns>
        public static List<DependencyObject> GetVisualTreeDescendants(this DependencyObject? element, Type type)
        {
            List<DependencyObject> result = new();

            if (element == null)
                return result;

            if (type.IsAssignableFrom(element.GetType()))
                result.Add(element);

            int count = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < count; ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);

                result.AddRange(GetVisualTreeDescendants(child, type));
            }

            return result;
        }

        /// <summary>
        /// 使用可视化树查找子控件
        /// </summary>
        /// <typeparam name="T">子控件类型</typeparam>
        /// <param name="element">查找的开始控件</param>
        /// <returns>查找结果</returns>
        public static List<T> GetVisualTreeDescendants<T>(this DependencyObject? element) where T : DependencyObject
        {
            return GetVisualTreeDescendants(element, typeof(T)).Cast<T>().ToList();
        }
    }
}
