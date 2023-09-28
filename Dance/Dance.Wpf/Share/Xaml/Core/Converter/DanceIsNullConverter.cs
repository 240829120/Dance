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
    /// 为空转化器
    /// </summary>
    public class DanceIsNullConverter : DanceConverterBase
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public object? DefaultValue { get; set; }

        /// <summary>
        /// 为空值
        /// </summary>
        public object? NullValue { get; set; }

        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                return string.IsNullOrWhiteSpace(str) ? this.NullValue : this.DefaultValue;

            return value == null ? this.NullValue : this.DefaultValue;
        }
    }
}
