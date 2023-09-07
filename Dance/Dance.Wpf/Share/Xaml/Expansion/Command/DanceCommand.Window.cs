using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 命令 -- 窗口相关
    /// </summary>
    public static partial class DanceCommand
    {
        /// <summary>
        /// 窗口最小化命令
        /// </summary>
        public static RelayCommand<object> WindowMinCommand => new(obj =>
        {
            try
            {
                if (obj is not FrameworkElement element || Window.GetWindow(element) is not Window window)
                    return;

                window.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        });

        /// <summary>
        /// 窗口最大化或正常命令
        /// </summary>
        public static RelayCommand<object> WindowMaxOrNormalCommand => new(obj =>
        {
            try
            {
                if (obj is not FrameworkElement element || Window.GetWindow(element) is not Window window)
                    return;

                window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        });

        /// <summary>
        /// 窗口关闭命令
        /// </summary>
        public static RelayCommand<object> WindowCloseCommand => new(obj =>
        {
            try
            {
                if (obj is not FrameworkElement element || Window.GetWindow(element) is not Window window)
                    return;

                window.Close();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        });

    }
}
