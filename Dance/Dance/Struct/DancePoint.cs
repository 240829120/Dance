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
    /// 二维点
    /// </summary>
    [DebuggerDisplay("X={X}, Y={Y}")]
    [TypeConverter(typeof(Point2FTypeConverter))]
    public struct DancePoint
    {
        public readonly static DancePoint Zero = new(0, 0);

        /// <summary>
        /// 二维点
        /// </summary>
        public DancePoint()
        {

        }

        /// <summary>
        /// 二维点
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public DancePoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
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
        /// 尝试转化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="point">转化后的点</param>
        /// <returns>是否成功转化</returns>
        public static bool TryParse(string value, out DancePoint point)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] array = value.Split(',');
                if (array.Length == 2 && float.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var result) && float.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var result2))
                {
                    point = new DancePoint(result, result2);
                    return true;
                }
            }

            point = default;
            return false;
        }

        /// <summary>
        /// 点和向量相加
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="vector">向量</param>
        /// <returns></returns>
        public static DancePoint operator +(DancePoint point, DanceVector vector)
        {
            return new DancePoint(point.X + vector.X, point.Y + vector.Y);
        }

        /// <summary>
        /// 点和向量相加
        /// </summary
        /// <param name="vector">向量</param>
        /// <param name="point">点</param>
        /// <returns></returns>
        public static DancePoint operator +(DanceVector vector, DancePoint point)
        {
            return new DancePoint(point.X + vector.X, point.Y + vector.Y);
        }
    }

    /// <summary>
    /// 二维点类型转化器
    /// </summary>
    public class Point2FTypeConverter : TypeConverter
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
            if (DancePoint.TryParse(value?.ToString() ?? string.Empty, out var point2F))
            {
                return point2F;
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DancePoint)}");
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            if (value is DancePoint point2F)
            {
                return point2F.X.ToString(CultureInfo.InvariantCulture) + ", " + point2F.Y.ToString(CultureInfo.InvariantCulture);
            }

            throw new NotSupportedException();
        }
    }
}
