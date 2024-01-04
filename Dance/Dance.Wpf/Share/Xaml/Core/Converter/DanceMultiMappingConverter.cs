using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dance.Wpf
{
    /// <summary>
    /// 多值映射转化器项
    /// </summary>
    [ContentProperty(nameof(Froms))]
    public class DanceMultiMappingConverterItem
    {
        /// <summary>
        /// 源集合
        /// </summary>
        public List<object> Froms { get; } = [];

        /// <summary>
        /// 目标
        /// </summary>
        public object? To { get; set; }
    }

    /// <summary>
    /// 多值映射转化器
    /// </summary>
    [ContentProperty(nameof(Items))]
    public class DanceMultiMappingConverter : IMultiValueConverter
    {
        /// <summary>
        /// 默认转化值
        /// </summary>
        public object? DefaultConvertValue { get; set; }

        /// <summary>
        /// 项集合
        /// </summary>
        public List<DanceMultiMappingConverterItem> Items { get; } = [];

        /// <summary>
        /// 转化
        /// </summary>
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return this.DefaultConvertValue;

            foreach (DanceMultiMappingConverterItem item in this.Items)
            {
                if (item.Froms.Count != values.Length)
                    continue;

                bool isMatch = true;
                for (int i = 0; i < values.Length; ++i)
                {
                    if (!object.Equals(values[i], item.Froms[i]))
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    return item.To;
                }
            }

            return this.DefaultConvertValue;
        }

        /// <summary>
        /// 翻转
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
