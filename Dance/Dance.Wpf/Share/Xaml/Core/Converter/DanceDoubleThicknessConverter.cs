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
    /// Double转厚度转化器
    /// </summary>
    public class DanceDoubleThicknessConverter : DanceConverterBase
    {
        /// <summary>
        /// 单位厚度
        /// </summary>
        public double UnitThickness { get; set; } = 10d;

        /// <summary>
        /// 厚度掩码
        /// </summary>
        public Thickness ThicknessMask { get; set; } = new Thickness(1);

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!int.TryParse(value?.ToString(), out int intValue) || !int.TryParse(parameter?.ToString(), out int offsetValue))
                return new Thickness(0);

            double thicknessValue = (intValue + offsetValue) * this.UnitThickness;

            double left = this.ThicknessMask.Left > 0 ? thicknessValue : 0d;
            double top = this.ThicknessMask.Top > 0 ? thicknessValue : 0d;
            double right = this.ThicknessMask.Right > 0 ? thicknessValue : 0d;
            double bottom = this.ThicknessMask.Bottom > 0 ? thicknessValue : 0d;

            return new Thickness(left, top, right, bottom);
        }
    }
}
