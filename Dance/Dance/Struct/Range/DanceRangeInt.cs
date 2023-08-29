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
    /// 范围 -- Int
    /// </summary>
    [DebuggerDisplay("MinValue={MinValue}, MaxValue={MaxValue}")]
    [TypeConverter(typeof(DanceRangeIntTypeConverter))]
    public struct DanceRangeInt
    {
        /// <summary>
        /// 范围 -- Int
        /// </summary>
        public DanceRangeInt() : this(0, 0)
        {

        }

        /// <summary>
        /// 范围 -- Int
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public DanceRangeInt(int minValue, int maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue;

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue;

        /// <summary>
        /// 空
        /// </summary>
        public static DanceRangeInt Zero { get; } = new();

        /// <summary>
        /// 尝试转化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="range">转化后的范围</param>
        /// <returns>是否成功转化</returns>
        public static bool TryParse(string value, out DanceRangeInt range)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] array = value.Split(',');
                if (array.Length == 2 && int.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var result) && int.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var result2))
                {
                    range = new DanceRangeInt(result, result2);
                    return true;
                }
            }

            range = default;
            return false;
        }
    }

    /// <summary>
    /// 范围 -- Int 类型类型转化器
    /// </summary>
    public class DanceRangeIntTypeConverter : TypeConverter
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
            if (DanceRangeInt.TryParse(value?.ToString() ?? string.Empty, out var range))
            {
                return range;
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DanceRangeInt)}");
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            if (value is DanceRangeInt range)
            {
                return range.MinValue.ToString() + ", " + range.MaxValue.ToString();
            }

            throw new NotSupportedException();
        }
    }
}
