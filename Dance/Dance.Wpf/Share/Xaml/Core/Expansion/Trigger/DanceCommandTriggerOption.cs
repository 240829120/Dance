using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 命令触发器设置
    /// </summary>
    public static class DanceCommandTriggerOption
    {
        #region IsAlreadyLoaded -- 是否已经完成了加载

        /// <summary>
        /// 获取是否已经完成了加载
        /// </summary>
        public static bool GetIsAlreadyLoaded(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAlreadyLoadedProperty);
        }

        /// <summary>
        /// 设置是否已经完成了加载
        /// </summary>
        public static void SetIsAlreadyLoaded(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAlreadyLoadedProperty, value);
        }

        /// <summary>
        /// 是否已经完成了加载
        /// </summary>
        public static readonly DependencyProperty IsAlreadyLoadedProperty =
            DependencyProperty.RegisterAttached("IsAlreadyLoaded", typeof(bool), typeof(DanceCommandTriggerOption), new PropertyMetadata(false));

        #endregion

        #region IsAlreadyUnloaded -- 是否已经完成了卸载

        /// <summary>
        /// 获取是否已经完成了卸载
        /// </summary>
        public static bool GetIsAlreadyUnloaded(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAlreadyUnloadedProperty);
        }

        /// <summary>
        /// 设置是否已经完成了卸载
        /// </summary>
        public static void SetIsAlreadyUnloaded(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAlreadyUnloadedProperty, value);
        }

        /// <summary>
        /// 是否已经完成了卸载
        /// </summary>
        public static readonly DependencyProperty IsAlreadyUnloadedProperty =
            DependencyProperty.RegisterAttached("IsAlreadyUnloaded", typeof(bool), typeof(DanceCommandTriggerOption), new PropertyMetadata(false));

        #endregion
    }
}
