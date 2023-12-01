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
    /// 矩阵到WPF矩阵变换转化器
    /// </summary>
    public class DanceMatrixToMatrixTransform3DConverter : DanceConverterBase
    {
        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not SharpDX.Matrix matrix)
                return null;

            return new MatrixTransform3D(matrix.ToMatrix3D());
        }

        /// <summary>
        /// 反向转化
        /// </summary>
        public override object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not MatrixTransform3D transform3D)
                return SharpDX.Matrix.Identity;

            return transform3D.Matrix.ToMatrix();
        }
    }
}
