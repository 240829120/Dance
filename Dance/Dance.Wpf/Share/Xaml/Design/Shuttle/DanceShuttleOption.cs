using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 穿梭设置
    /// </summary>
    public class DanceShuttleOption : DependencyObject
    {
        #region UnitLength -- 单位长度

        /// <summary>
        /// 获取单位长度
        /// </summary>
        public static double GetUnitLength(DependencyObject obj)
        {
            return (double)obj.GetValue(UnitLengthProperty);
        }

        /// <summary>
        /// 设置单位长度
        /// </summary>
        public static void SetUnitLength(DependencyObject obj, double value)
        {
            obj.SetValue(UnitLengthProperty, value);
        }

        /// <summary>
        /// 单位长度
        /// </summary>
        public static readonly DependencyProperty UnitLengthProperty =
            DependencyProperty.RegisterAttached("UnitLength", typeof(double), typeof(DanceShuttleOption), new PropertyMetadata(30d));

        #endregion
    }
}
