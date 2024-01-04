using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// 默认转化值
        /// </summary>
        public object? DefaultConvertValue { get; set; }

        /// <summary>
        /// 默认翻转值
        /// </summary>
        public object? DefaultConvertBackValue { get; set; }

        /// <summary>
        /// 项集合
        /// </summary>
        public List<DanceMappingConverterItem> Items { get; } = [];

        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Items.Count == 0)
                return this.DefaultConvertValue;

            object? result = this.Items.FirstOrDefault(p => object.Equals(value, p.From))?.To ?? this.DefaultConvertValue;

            return result;
        }

        /// <summary>
        /// 翻转
        /// </summary>
        public override object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Items.Count == 0)
                return this.DefaultConvertBackValue;

            object? result = this.Items.FirstOrDefault(p => object.Equals(value, p.To))?.From ?? this.DefaultConvertBackValue;

            return result;
        }
    }
}
