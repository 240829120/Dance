using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 导航切换旋转参数
    /// </summary>
    public class DanceNavigationSwitchRotationOption : BindableObject
    {
        #region Angle -- 旋转角度

        /// <summary>
        /// 获取旋转角度
        /// </summary>
        /// <param name="view">元素</param>
        /// <returns>值</returns>
        public static double GetAngle(BindableObject view)
        {
            return (double)view.GetValue(AngleProperty);
        }

        /// <summary>
        /// 设置旋转角度
        /// </summary>
        /// <param name="view">元素</param>
        /// <param name="value">值</param>
        public static void SetAngle(BindableObject view, double value)
        {
            view.SetValue(AngleProperty, value);
        }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public static readonly BindableProperty AngleProperty =
            BindableProperty.CreateAttached("Angle", typeof(double), typeof(DanceNavigationSwitchRotationOption), 90d);

        #endregion
    }
}
