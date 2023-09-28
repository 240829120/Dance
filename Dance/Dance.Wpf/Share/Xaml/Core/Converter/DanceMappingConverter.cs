using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dance.Wpf
{
    /// <summary>
    /// 映射转化项
    /// </summary>
    public class DanceMappingConverterItem
    {
        /// <summary>
        /// 源
        /// </summary>
        public object? From { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public object? To { get; set; }
    }

    /// <summary>
    /// 映射转化器
    /// </summary>
    [ContentProperty(nameof(Items))]
    public class DanceMappingConverter : DanceConverterBase
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public object? DefaultValue { get; set; }

        /// <summary>
        /// 项集合
        /// </summary>
        public List<DanceMappingConverterItem> Items { get; } = new();

        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || this.Items.Count == 0)
                return this.DefaultValue;

            return this.Items.FirstOrDefault(p => object.Equals(value, p));
        }
    }
}
