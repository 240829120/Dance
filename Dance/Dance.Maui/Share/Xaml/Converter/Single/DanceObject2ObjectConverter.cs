using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 对象转映射
    /// </summary>
    public class DanceObject2ObjectMap
    {
        /// <summary>
        /// 源
        /// </summary>
        public object? From { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object? To { get; set; }
    }

    /// <summary>
    /// 对象转对象转化器
    /// </summary>
    public class DanceObject2ObjectConverter : IValueConverter
    {
        /// <summary>
        /// 映射集合
        /// </summary>
        public IEnumerable<DanceObject2ObjectMap>? Maps { get; set; }

        /// <summary>
        /// 默认源
        /// </summary>
        public object? DefaultFrom { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object? DefaultTo { get; set; }

        /// <summary>
        /// 转化
        /// </summary>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (this.Maps == null || value == null)
                return this.DefaultTo;

            return this.Maps.FirstOrDefault(p => value.Equals(p.From))?.To ?? this.DefaultTo;
        }

        /// <summary>
        /// 反转化
        /// </summary>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (this.Maps == null || value == null)
                return this.DefaultFrom;

            return this.Maps.FirstOrDefault(p => value.Equals(p.To))?.From ?? this.DefaultFrom;
        }
    }
}
