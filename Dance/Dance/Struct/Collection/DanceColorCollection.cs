using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 颜色集合
    /// </summary>
    [TypeConverter(typeof(DanceColorCollectionTypeConverter))]
    public class DanceColorCollection : List<DanceColor>
    {

    }

    /// <summary>
    /// 颜色集合转化器
    /// </summary>
    public class DanceColorCollectionTypeConverter : TypeConverter
    {
        /// <summary>
        /// 颜色转化器
        /// </summary>
        private readonly DanceColorTypeConverter Converter = new();

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            DanceColorCollection list = new();

            string? str = value?.ToString();
            if (string.IsNullOrWhiteSpace(str))
                return list;

            string[] str_colors = str.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (string str_color in str_colors)
            {
                if (this.Converter.ConvertFromString(str_color) is not DanceColor color)
                {
                    throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DanceColorCollection)}");
                }

                list.Add(color);
            }

            return list;
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            throw new NotSupportedException();
        }
    }
}
