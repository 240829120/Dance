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
    /// 范围 -- TimeSpan
    /// </summary>
    [DebuggerDisplay("MinValue={MinValue}, MaxValue={MaxValue}")]
    [TypeConverter(typeof(DanceRangeTimeSpanTypeConverter))]
    public struct DanceRangeTimeSpan
    {
        /// <summary>
        /// 范围 -- TimeSpan
        /// </summary>
        public DanceRangeTimeSpan() : this(TimeSpan.Zero, TimeSpan.Zero)
        {

        }

        /// <summary>
        /// 范围 -- TimeSpan
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public DanceRangeTimeSpan(TimeSpan minValue, TimeSpan maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public TimeSpan MinValue;

        /// <summary>
        /// 最大值
        /// </summary>
        public TimeSpan MaxValue;

        /// <summary>
        /// 尝试转化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="range">转化后的范围</param>
        /// <returns>是否成功转化</returns>
        public static bool TryParse(string value, out DanceRangeTimeSpan range)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] array = value.Split(',');
                if (array.Length == 2 && TimeSpan.TryParse(array[0], CultureInfo.InvariantCulture, out var result) && TimeSpan.TryParse(array[1], CultureInfo.InvariantCulture, out var result2))
                {
                    range = new DanceRangeTimeSpan(result, result2);
                    return true;
                }
            }

            range = default;
            return false;
        }
    }

    /// <summary>
    /// 范围 -- TimeSpan 类型类型转化器
    /// </summary>
    public class DanceRangeTimeSpanTypeConverter : TypeConverter
    {
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
            if (DanceRangeTimeSpan.TryParse(value?.ToString() ?? string.Empty, out var range))
            {
                return range;
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DanceRangeTimeSpan)}");
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            if (value is DanceRangeTimeSpan range)
            {
                return range.MinValue.ToString() + ", " + range.MaxValue.ToString();
            }

            throw new NotSupportedException();
        }
    }
}
