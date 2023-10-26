using AvalonDock.Controls;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 菜单触发器
    /// </summary>
    public class DanceMenuTrigger : DependencyObject
    {
        #region IsSubmenuOpenedUpdateStatus -- 子菜单打开时更新状态

        /// <summary>
        /// 获取子菜单打开时更新命令
        /// </summary>
        public static bool GetIsSubmenuOpenedUpdateStatus(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSubmenuOpenedUpdateStatusProperty);
        }

        /// <summary>
        /// 设置子菜单打开时更新命令
        /// </summary>
        public static void SetIsSubmenuOpenedUpdateStatus(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSubmenuOpenedUpdateStatusProperty, value);
        }

        /// <summary>
        /// 子菜单打开时更新命令
        /// </summary>
        public static readonly DependencyProperty IsSubmenuOpenedUpdateStatusProperty =
            DependencyProperty.RegisterAttached("IsSubmenuOpenedUpdateStatus", typeof(bool), typeof(DanceMenuTrigger), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not MenuItem menuItem)
                    return;

                menuItem.SubmenuOpened -= MenuItem_SubmenuOpened;
                menuItem.SubmenuOpened += MenuItem_SubmenuOpened;

            })));

        private static void MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem menuItem)
                return;

            foreach (object obj in menuItem.Items)
            {
                if (obj is not MenuItem item || item.Command is not IRelayCommand cmd)
                    continue;

                cmd.NotifyCanExecuteChanged();
            }
        }

        #endregion
    }
}
