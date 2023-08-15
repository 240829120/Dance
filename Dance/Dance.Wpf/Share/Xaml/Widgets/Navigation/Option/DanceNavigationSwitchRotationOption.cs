using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 导航切换旋转参数
    /// </summary>
    public class DanceNavigationSwitchRotationOption : DependencyObject
    {
        #region Angle -- 旋转角度

        /// <summary>
        /// 旋转角度
        /// </summary>
        public static double GetAngle(DependencyObject obj)
        {
            return (double)obj.GetValue(AngleProperty);
        }
        /// <summary>
        /// 旋转角度
        /// </summary>
        public static void SetAngle(DependencyObject obj, double value)
        {
            obj.SetValue(AngleProperty, value);
        }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.RegisterAttached("Angle", typeof(double), typeof(DanceNavigationSwitchRotationOption), new PropertyMetadata(90d));

        #endregion
    }
}
