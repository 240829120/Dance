using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace Dance.Wpf
{
    /// <summary>
    /// 变换转化器
    /// </summary>
    public class DanceTransformConverter : DanceConverterBase
    {
        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not DanceTransform3D transform)
                return null;

            return transform.ToMatrixTransform3D();
        }

        /// <summary>
        /// 反向转化
        /// </summary>
        public override object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
