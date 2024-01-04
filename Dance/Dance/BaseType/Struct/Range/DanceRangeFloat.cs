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
    /// 范围 -- Float
    /// </summary>
    /// <param name="minValue">最小值</param>
    /// <param name="maxValue">最大值</param>
    [DebuggerDisplay("MinValue={MinValue}, MaxValue={MaxValue}")]
    [TypeConverter(typeof(DanceRangeFloatTypeConverter))]
    public struct DanceRangeFloat(float minValue, float maxValue)
    {
        /// <summary>
        /// 范围 -- TimeSpan
        /// </summary>
        public DanceRangeFloat() : this(0f, 0f)
        {

        }

        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue = minValue;

        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue = maxValue;

        /// <summary>
        /// 空
        /// </summary>
        public static DanceRangeFloat Zero { get; } = new();

        /// <summary>
        /// 尝试转化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="range">转化后的范围</param>
        /// <returns>是否成功转化</returns>
        public static bool TryParse(string value, out DanceRangeFloat range)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] array = value.Split(',');
                if (array.Length == 2 && float.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var result) && float.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var result2))
                {
                    range = new DanceRangeFloat(result, result2);
                    return true;
                }
            }

            range = default;
            return false;
        }
    }

    /// <summary>
    /// 范围 -- Float 类型类型转化器
    /// </summary>
    public class DanceRangeFloatTypeConverter : TypeConverter
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
            if (DanceRangeFloat.TryParse(value?.ToString() ?? string.Empty, out var range))
            {
                return range;
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DanceRangeFloat)}");
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            if (value is DanceRangeFloat range)
            {
                return range.MinValue.ToString() + ", " + range.MaxValue.ToString();
            }

            throw new NotSupportedException();
        }
    }
}
