using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Dance.Wpf
{
    /// <summary>
    /// 属性表格设置
    /// </summary>
    public class DanceToolkitPropertyGridOption : FrameworkElement
    {
        #region IsShowLabel -- 是否显示标签

        /// <summary>
        /// 获取是否显示标签
        /// </summary>
        public static bool GetIsShowLabel(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsShowLabelProperty);
        }

        /// <summary>
        /// 设置是否显示标签
        /// </summary>
        public static void SetIsShowLabel(DependencyObject obj, bool value)
        {
            obj.SetValue(IsShowLabelProperty, value);
        }

        /// <summary>
        /// 是否显示标签
        /// </summary>
        public static readonly DependencyProperty IsShowLabelProperty =
            DependencyProperty.RegisterAttached("IsShowLabel", typeof(bool), typeof(DanceToolkitPropertyGridOption), new PropertyMetadata(true, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element || s is PropertyItemBase)
                    return;

                element.Loaded -= Element_Loaded;
                element.Loaded += Element_Loaded;

            })));

        /// <summary>
        /// 加载完成
        /// </summary>
        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            PropertyItemBase? item = element.GetVisualTreeParent<PropertyItemBase>();
            item?.SetValue(IsShowLabelProperty, GetIsShowLabel(element));
        }

        #endregion
    }
}
