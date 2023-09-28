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
    /// 类型实例化转化器
    /// </summary>
    public class DanceTypeInstanceConverter : DanceConverterBase
    {
        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Type type || string.IsNullOrWhiteSpace(type.FullName))
                return null;

            return type.Assembly.CreateInstance(type.FullName);
        }
    }
}