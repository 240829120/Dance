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
        #region IsMenuOpendUpdateStatus -- 菜单打开时更新状态

        /// <summary>
        /// 获取菜单打开时更新状态
        /// </summary>
        public static bool GetIsMenuOpendUpdateStatus(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMenuOpendUpdateStatusProperty);
        }

        /// <summary>
        /// 设置菜单打开时更新状态
        /// </summary>
        public static void SetIsMenuOpendUpdateStatus(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMenuOpendUpdateStatusProperty, value);
        }

        /// <summary>
        /// 菜单打开时更新状态
        /// </summary>
        public static readonly DependencyProperty IsMenuOpendUpdateStatusProperty =
            DependencyProperty.RegisterAttached("IsMenuOpendUpdateStatus", typeof(bool), typeof(DanceMenuTrigger), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                bool register = false;
                if (e.NewValue is bool newValue)
                {
                    register = newValue;
                }

                if (s is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= MenuItem_SubmenuOpened;
                    if (register)
                    {
                        menuItem.SubmenuOpened += MenuItem_SubmenuOpened;
                    }
                }

                if (s is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= ContextMenu_Opened;
                    if (register)
                    {
                        contextMenu.Opened += ContextMenu_Opened;
                    }
                }

            })));


        /// <summary>
        /// 子菜单打开
        /// </summary>
        private static void MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem menu)
                return;

            foreach (object obj in menu.Items)
            {
                if (obj is not MenuItem item || item.Command is not IRelayCommand cmd)
                    continue;

                cmd.NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// 右键菜单打开
        /// </summary>
        private static void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (sender is not ContextMenu menu)
                return;

            foreach (object obj in menu.Items)
            {
                if (obj is not MenuItem item || item.Command is not IRelayCommand cmd)
                    continue;

                cmd.NotifyCanExecuteChanged();
            }
        }

        #endregion

    }
}
