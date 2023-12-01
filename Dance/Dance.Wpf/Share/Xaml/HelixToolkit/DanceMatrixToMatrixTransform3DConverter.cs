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
            if (value is not DanceMatrix3 matrix)
                return null;

            return new MatrixTransform3D(new Matrix3D(matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                                                      matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                                                      matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                                                      matrix.M41, matrix.M42, matrix.M43, matrix.M44));
        }

        /// <summary>
        /// 反向转化
        /// </summary>
        public override object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not MatrixTransform3D transform3D)
                return SharpDX.Matrix.Identity;

            DanceMatrix3 matrix = new((float)transform3D.Matrix.M11, (float)transform3D.Matrix.M12, (float)transform3D.Matrix.M13, (float)transform3D.Matrix.M14,
                                      (float)transform3D.Matrix.M21, (float)transform3D.Matrix.M22, (float)transform3D.Matrix.M23, (float)transform3D.Matrix.M24,
                                      (float)transform3D.Matrix.M31, (float)transform3D.Matrix.M32, (float)transform3D.Matrix.M33, (float)transform3D.Matrix.M34,
                                      (float)transform3D.Matrix.OffsetX, (float)transform3D.Matrix.OffsetY, (float)transform3D.Matrix.OffsetZ, (float)transform3D.Matrix.M44);

            return matrix;
        }
    }
}
