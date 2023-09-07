using log4net;
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
    /// 命令触发器
    /// </summary>
    public static partial class DanceCommandTrigger
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly static ILog log = LogManager.GetLogger(typeof(DanceCommandTrigger));

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 获取加载命令
        /// </summary>
        public static ICommand GetLoadedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedCommandProperty);
        }

        /// <summary>
        /// 设置加载命令
        /// </summary>
        public static void SetLoadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedCommandProperty, value);
        }

        /// <summary>
        /// 加载命令
        /// </summary>
        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.RegisterAttached("LoadedCommand", typeof(ICommand), typeof(DanceCommandTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Loaded -= Execute_LoadedCommand;
                element.Loaded += Execute_LoadedCommand;

            })));

        /// <summary>
        /// 执行加载命令
        /// </summary>
        private static void Execute_LoadedCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetLoadedCommand(element) is not ICommand command)
                    return;

                command.Execute(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region UnloadedCommand -- 卸载命令

        /// <summary>
        /// 获取卸载命令
        /// </summary>
        public static ICommand GetUnloadedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(UnloadedCommandProperty);
        }

        /// <summary>
        /// 设置卸载命令
        /// </summary>
        public static void SetUnloadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(UnloadedCommandProperty, value);
        }

        /// <summary>
        /// 卸载命令
        /// </summary>
        public static readonly DependencyProperty UnloadedCommandProperty =
            DependencyProperty.RegisterAttached("UnloadedCommand", typeof(ICommand), typeof(DanceCommandTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Unloaded -= Execute_UnloadedCommand;
                element.Unloaded += Execute_UnloadedCommand;
            })));

        /// <summary>
        /// 执行卸载命令
        /// </summary>
        private static void Execute_UnloadedCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetUnloadedCommand(element) is not ICommand command)
                    return;

                command.Execute(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region LoadedOnceCommand -- 加载一次命令

        /// <summary>
        /// 获取加载一次命令
        /// </summary>
        public static ICommand GetLoadedOnceCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedOnceCommandProperty);
        }

        /// <summary>
        /// 设置加载一次命令
        /// </summary>
        public static void SetLoadedOnceCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedOnceCommandProperty, value);
        }

        /// <summary>
        /// 加载一次命令
        /// </summary>
        public static readonly DependencyProperty LoadedOnceCommandProperty =
            DependencyProperty.RegisterAttached("LoadedOnceCommand", typeof(ICommand), typeof(DanceCommandTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Loaded -= Execute_LoadedOnceCommand;
                element.Loaded += Execute_LoadedOnceCommand;
            })));

        /// <summary>
        /// 执行卸载命令
        /// </summary>
        private static void Execute_LoadedOnceCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetLoadedOnceCommand(element) is not ICommand command)
                    return;

                if (DanceCommandTriggerOption.GetIsAlreadyLoaded(element))
                    return;

                DanceCommandTriggerOption.SetIsAlreadyLoaded(element, true);
                command.Execute(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region UnloadedOnceCommand 卸载一次命令

        /// <summary>
        /// 获取卸载一次命令
        /// </summary>
        public static ICommand GetUnloadedOnceCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(UnloadedOnceCommandProperty);
        }

        /// <summary>
        /// 设置卸载一次命令
        /// </summary>
        public static void SetUnloadedOnceCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(UnloadedOnceCommandProperty, value);
        }

        /// <summary>
        /// 卸载一次命令
        /// </summary>
        public static readonly DependencyProperty UnloadedOnceCommandProperty =
            DependencyProperty.RegisterAttached("UnloadedOnceCommand", typeof(ICommand), typeof(DanceCommandTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Unloaded -= Execute_UnloadedOnceCommand;
                element.Unloaded += Execute_UnloadedOnceCommand;
            })));

        /// <summary>
        /// 执行卸载一次命令
        /// </summary>
        private static void Execute_UnloadedOnceCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetUnloadedOnceCommand(element) is not ICommand command)
                    return;

                if (DanceCommandTriggerOption.GetIsAlreadyUnloaded(element))
                    return;

                DanceCommandTriggerOption.SetIsAlreadyUnloaded(element, true);
                command.Execute(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion
    }
}
