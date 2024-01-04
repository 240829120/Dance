using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// TimeSpan 转 Double 模式
    /// </summary>
    public enum DanceTimeSpan2DoubleMode
    {
        /// <summary>
        /// 毫秒
        /// </summary>
        Millisecond,

        /// <summary>
        /// 秒
        /// </summary>
        Second,

        /// <summary>
        /// 分钟
        /// </summary>
        Minute,

        /// <summary>
        /// 小时
        /// </summary>
        Hour,

        /// <summary>
        /// 天数
        /// </summary>
        Day
    }

    /// <summary>
    /// TimeSpan 转 Double 转化器
    /// </summary>
    public class DanceTimeSpan2DoubleConverter : IValueConverter
    {
        /// <summary>
        /// 转化模式
        /// </summary>
        public DanceTimeSpan2DoubleMode Mode { get; set; } = DanceTimeSpan2DoubleMode.Millisecond;

        /// <summary>
        /// 转化
        /// </summary>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not TimeSpan timeSpan)
                return 0d;

            return this.Mode switch
            {
                DanceTimeSpan2DoubleMode.Millisecond => timeSpan.TotalMilliseconds,
                DanceTimeSpan2DoubleMode.Second => timeSpan.TotalSeconds,
                DanceTimeSpan2DoubleMode.Minute => timeSpan.TotalMinutes,
                DanceTimeSpan2DoubleMode.Hour => timeSpan.TotalHours,
                DanceTimeSpan2DoubleMode.Day => timeSpan.TotalDays,
                _ => 0d,
            };
        }

        /// <summary>
        /// 反转化
        /// </summary>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not double number)
                return TimeSpan.Zero;

            return this.Mode switch
            {
                DanceTimeSpan2DoubleMode.Millisecond => TimeSpan.FromMilliseconds(number),
                DanceTimeSpan2DoubleMode.Second => TimeSpan.FromSeconds(number),
                DanceTimeSpan2DoubleMode.Minute => TimeSpan.FromMinutes(number),
                DanceTimeSpan2DoubleMode.Hour => TimeSpan.FromHours(number),
                DanceTimeSpan2DoubleMode.Day => TimeSpan.FromDays(number),
                _ => TimeSpan.Zero,
            };
        }
    }
}
