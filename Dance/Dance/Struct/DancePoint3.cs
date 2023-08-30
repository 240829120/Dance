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
    /// 三维点
    /// </summary>
    [DebuggerDisplay("X={X}, Y={Y}, Z={Z}")]
    [TypeConverter(typeof(DancePoint3TypeConverter))]
    public struct DancePoint3
    {
        public readonly static DancePoint3 Zero = new(0, 0, 0);

        /// <summary>
        /// 三维点
        /// </summary>
        public DancePoint3()
        {

        }

        /// <summary>
        /// 三维点
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public DancePoint3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// X值
        /// </summary>
        public float X;

        /// <summary>
        /// Y值
        /// </summary>
        public float Y;

        /// <summary>
        /// Z值
        /// </summary>
        public float Z;

        /// <summary>
        /// 尝试转化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="point">转化后的点</param>
        /// <returns>是否成功转化</returns>
        public static bool TryParse(string value, out DancePoint3 point)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] array = value.Split(',');
                if (array.Length == 3 &&
                    float.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var result) &&
                    float.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var result2) &&
                    float.TryParse(array[2], NumberStyles.Number, CultureInfo.InvariantCulture, out var result3))
                {
                    point = new DancePoint3(result, result2, result3);
                    return true;
                }
            }

            point = default;
            return false;
        }
    }

    /// <summary>
    /// 二维点类型转化器
    /// </summary>
    public class DancePoint3TypeConverter : TypeConverter
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
            if (DancePoint3.TryParse(value?.ToString() ?? string.Empty, out var point))
            {
                return point;
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DancePoint3)}");
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            if (value is DancePoint3 point)
            {
                return point.X.ToString(CultureInfo.InvariantCulture) + ", " + point.Y.ToString(CultureInfo.InvariantCulture) + ", " + point.Z.ToString(CultureInfo.InvariantCulture);
            }

            throw new NotSupportedException();
        }
    }
}
