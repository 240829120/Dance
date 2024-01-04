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
    /// 点集合
    /// </summary>
    [TypeConverter(typeof(DancePointCollectionTypeConverter))]
    public class DancePointCollection : List<DancePoint>
    {

    }

    /// <summary>
    /// 点集合转化器
    /// </summary>
    public class DancePointCollectionTypeConverter : TypeConverter
    {
        /// <summary>
        /// 点转化器
        /// </summary>
        private readonly DancePointTypeConverter Converter = new();

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
            DancePointCollection list = [];

            string? str = value?.ToString();
            if (string.IsNullOrWhiteSpace(str))
                return list;

            string[] str_points = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (string str_point in str_points)
            {
                if (this.Converter.ConvertFromString(str_point) is not DancePoint point)
                {
                    throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DancePointCollection)}");
                }

                list.Add(point);
            }

            return list;
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            throw new NotSupportedException();
        }
    }
}
