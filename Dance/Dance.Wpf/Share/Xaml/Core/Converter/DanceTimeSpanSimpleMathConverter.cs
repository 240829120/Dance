using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dance.Wpf
{
    /// <summary>
    /// 算法
    /// </summary>
    public enum DanceTimeSpanSimpleMathConverterType
    {
        /// <summary>
        /// 加法
        /// </summary>
        Add,

        /// <summary>
        /// 减法
        /// </summary>
        Subtract
    }

    /// <summary>
    /// TimeSpan 简单数学转化器
    /// </summary>
    public class DanceTimeSpanSimpleMathConverter : DanceConverterBase
    {
        /// <summary>
        /// 转化方法
        /// </summary>
        public DanceTimeSpanSimpleMathConverterType ConverterType { get; set; } = DanceTimeSpanSimpleMathConverterType.Add;

        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _ = TimeSpan.TryParse(value?.ToString(), out TimeSpan v1);
            _ = TimeSpan.TryParse(parameter?.ToString(), out TimeSpan v2);

            return this.ConverterType switch
            {
                DanceTimeSpanSimpleMathConverterType.Add => v1 + v2,
                DanceTimeSpanSimpleMathConverterType.Subtract => v1 - v2,
                _ => (v1 + v2),
            };
        }
    }
}
