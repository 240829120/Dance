using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 颜色
    /// </summary>
    public struct DanceColor
    {
        /// <summary>
        /// 颜色
        /// </summary>
        public DanceColor()
        {

        }

        /// <summary>
        /// 颜色
        /// </summary>
        /// <param name="a">透明度</param>
        /// <param name="r">红色</param>
        /// <param name="g">绿色</param>
        /// <param name="b">蓝色</param>
        public DanceColor(float a, float r, float g, float b)
        {
            this.A = a;
            this.R = r;
            this.G = g;
            this.B = b;
        }

        /// <summary>
        /// 颜色
        /// </summary>
        /// <param name="a">透明度</param>
        /// <param name="r">红色</param>
        /// <param name="g">绿色</param>
        /// <param name="b">蓝色</param>
        public DanceColor(byte a, byte r, byte g, byte b)
        {
            this.A = a / 255f;
            this.R = r / 255f;
            this.G = g / 255f;
            this.B = b / 255f;
        }

        /// <summary>
        /// 颜色
        /// </summary>
        /// <param name="code">颜色码 #AARRGGBB</param>
        public DanceColor(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code[0] != '#')
                throw new ArgumentException(nameof(code));

            this.A = byte.Parse(code.Substring(1, 2), NumberStyles.HexNumber);
            this.R = byte.Parse(code.Substring(3, 2), NumberStyles.HexNumber);
            this.G = byte.Parse(code.Substring(5, 2), NumberStyles.HexNumber);
            this.B = byte.Parse(code.Substring(7, 2), NumberStyles.HexNumber);
        }

        /// <summary>
        /// 透明度
        /// </summary>
        public float A;

        /// <summary>
        /// 红色
        /// </summary>
        public float R;

        /// <summary>
        /// 绿色
        /// </summary>
        public float G;

        /// <summary>
        /// 蓝色
        /// </summary>
        public float B;

        /// <summary>
        /// 尝试转化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="color">转化后的颜色</param>
        /// <returns>是否成功转化</returns>
        public static bool TryParse(string value, out DanceColor color)
        {
            if (!string.IsNullOrEmpty(value) && value[0] == '#')
            {
                byte a = byte.Parse(value.Substring(1, 2), NumberStyles.HexNumber);
                byte r = byte.Parse(value.Substring(3, 2), NumberStyles.HexNumber);
                byte g = byte.Parse(value.Substring(5, 2), NumberStyles.HexNumber);
                byte b = byte.Parse(value.Substring(7, 2), NumberStyles.HexNumber);

                color = new(a, r, g, b);

                return true;
            }

            color = default;
            return false;
        }

        public override string ToString()
        {
            return $"#{this.A:X2}{this.R:X2}{this.G:X2}{this.B:X2}";
        }
    }

    /// <summary>
    /// 颜色类型转化器
    /// </summary>
    public class DanceColorTypeConverter : TypeConverter
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
            if (DanceColor.TryParse(value?.ToString() ?? string.Empty, out var color))
            {
                return color;
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(DanceColor)}");
        }

        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            if (value is DanceColor color)
            {
                return color.ToString();
            }

            throw new NotSupportedException();
        }
    }
}
