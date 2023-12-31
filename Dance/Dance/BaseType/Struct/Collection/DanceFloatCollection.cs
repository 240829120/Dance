﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Float类型列表
    /// </summary>
    [TypeConverter(typeof(DanceFloatCollectionTypeConverter))]
    public class DanceFloatCollection : List<float>
    {
    }

    /// <summary>
    /// 点集合转化器
    /// </summary>
    public class DanceFloatCollectionTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <summary>
        /// 分隔符
        /// </summary>
        private static readonly char[] SEPARATOR = [',', ' '];

        public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            DanceFloatCollection list = [];

            string? str = value?.ToString();
            if (string.IsNullOrWhiteSpace(str))
                return list;

            string[] str_values = str.Split(SEPARATOR, StringSplitOptions.RemoveEmptyEntries);

            foreach (string str_value in str_values)
            {
                if (!float.TryParse(str_value, out float f))
                {
                    throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DanceFloatCollection)}");
                }
                list.Add(f);
            }

            return list;
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            throw new NotSupportedException();
        }
    }
}
