﻿using System;
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
    /// 二维向量
    /// </summary>
    [DebuggerDisplay("X={X}, Y={Y}")]
    [TypeConverter(typeof(Vector2FTypeConverter))]
    public struct DanceVector
    {
        public static readonly DanceVector Zero = new(0, 0);

        /// <summary>
        /// 二维向量
        /// </summary>
        public DanceVector()
        {

        }

        /// <summary>
        /// 二维向量
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public DanceVector(float x, float y)
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

        public static bool TryParse(string value, out DanceVector vector2F)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] array = value.Split(',');
                if (array.Length == 2 && float.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var result) && float.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var result2))
                {
                    vector2F = new DanceVector(result, result2);
                    return true;
                }
            }

            vector2F = default;
            return false;
        }
    }

    /// <summary>
    /// 二维向量类型转化器
    /// </summary>
    public class Vector2FTypeConverter : TypeConverter
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
            if (DanceVector.TryParse(value?.ToString() ?? string.Empty, out var vector2F))
            {
                return vector2F;
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DanceVector)}");
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            if (value is DanceVector vector2F)
            {
                return vector2F.X.ToString(CultureInfo.InvariantCulture) + ", " + vector2F.Y.ToString(CultureInfo.InvariantCulture);
            }

            throw new NotSupportedException();
        }
    }
}
