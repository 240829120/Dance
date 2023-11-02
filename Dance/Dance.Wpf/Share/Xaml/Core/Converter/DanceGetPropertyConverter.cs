using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Dance.Wpf
{
    /// <summary>
    /// 获取属性转化器
    /// </summary>
    public class DanceGetPropertyConverter : IMultiValueConverter
    {
        /// <summary>
        /// 转化
        /// values[0]: 对象
        /// values[1]: 属性
        /// </summary>
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2)
                return null;

            if (values[0] is not object obj || values[1] is not string propertyName)
                return null;

            PropertyInfo? propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
                return null;

            return propertyInfo.GetValue(obj, null);
        }

        /// <summary>
        /// 反转化
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
