using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Dance.Wpf
{
    /// <summary>
    /// Double转GridLength转化器
    /// </summary>
    public class DanceDoubleGridLengthConverter : DanceConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse(value?.ToString(), out double doubleValue))
                return new GridLength(0);

            return new GridLength(doubleValue, GridUnitType.Pixel);
        }
    }
}
