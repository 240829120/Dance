using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 导航视图转化器
    /// </summary>
    public class DanceNavigationViewConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is View)
                return value;

            if (value is Type type && type.IsAssignableTo(typeof(View)))
            {
                ConstructorInfo? constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    return constructor.Invoke(null) as View;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
