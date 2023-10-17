using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 消息扩展
    /// </summary>
    public static class DanceMessageExpansion
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            if (System.Windows.Application.Current == null)
                return;

            string? path = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
            if (string.IsNullOrWhiteSpace(path))
                return;

            NotifyIcon = new()
            {
                Icon = Icon.ExtractAssociatedIcon(path),
                Text = System.Windows.Application.Current.MainWindow?.Title,
                Visible = true
            };
        }

        // ================================================================================================================================
        // MessageBox -- 消息框

        /// <summary>
        /// 显示提示框，需要切换至UI线程
        /// </summary>
        /// <param name="header">头部</param>
        /// <param name="icon">图标</param>
        /// <param name="content">内容</param>
        /// <param name="action">行为</param>
        /// <param name="owner">所属窗口</param>
        /// <returns>行为</returns>
        public static DanceMessageBoxAction ShowMessageBox(object? header, ImageSource? icon, object? content, DanceMessageBoxAction action, Window? owner)
        {
            DanceMessageBoxWindow window = new();
            window.MessageBox.Header = header;
            window.MessageBox.Icon = icon;
            window.MessageBox.HasIcon = icon != null;
            window.MessageBox.HasNoButton = action.HasFlag(DanceMessageBoxAction.NO);
            window.MessageBox.HasCancelButton = action.HasFlag(DanceMessageBoxAction.CANCEL);
            window.MessageBox.Content = content;
            window.MessageBox.Action = action;

            window.Owner = owner ?? System.Windows.Application.Current?.MainWindow;
            window.ShowDialog();

            return window.box.ResultAction;
        }

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="header">头部</param>
        /// <param name="icon">图标</param>
        /// <param name="content">内容</param>
        /// <param name="action">行为</param>
        /// <param name="owner">所属窗口</param>
        /// <returns>行为</returns>
        public static DanceMessageBoxAction ShowMessageBox(object? header, DanceMessageBoxIcon icon, object? content, DanceMessageBoxAction action)
        {
            DanceMessageBoxAction result = DanceMessageBoxAction.YES;

            System.Windows.Application.Current?.Dispatcher.Invoke(() =>
            {
                ImageSource? source = null;
                switch (icon)
                {
                    case DanceMessageBoxIcon.None: break;
                    case DanceMessageBoxIcon.Failure: source = DanceResourceIcons.Failure; break;
                    case DanceMessageBoxIcon.Success: source = DanceResourceIcons.Success; break;
                    case DanceMessageBoxIcon.Warning: source = DanceResourceIcons.Warning; break;
                    case DanceMessageBoxIcon.Info: source = DanceResourceIcons.Info; break;
                }

                result = ShowMessageBox(header, source, content, action, null);
            });

            return result;
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="header">头部</param>
        /// <param name="content">内容</param>
        /// <param name="action">行为</param>
        /// <returns>行为</returns>
        public static DanceMessageBoxAction ShowMessageBox(object? header, object? content, DanceMessageBoxAction action)
        {
            return ShowMessageBox(header, DanceMessageBoxIcon.None, content, action);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="header">头部</param>
        /// <param name="content">内容</param>
        /// <returns>行为</returns>
        public static DanceMessageBoxAction ShowMessageBox(object? header, object? content)
        {
            return ShowMessageBox(header, DanceMessageBoxIcon.None, content, DanceMessageBoxAction.YES);
        }

        // ================================================================================================================================
        // Notify -- 通知

        /// <summary>
        /// 通知
        /// </summary>
        public static NotifyIcon? NotifyIcon { get; private set; }

        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <param name="icon">图标</param>
        /// <param name="header">头部</param>
        /// <param name="content">内容</param>
        public static void ShowNotify(TimeSpan timeout, ToolTipIcon icon, string header, string content)
        {
            NotifyIcon?.ShowBalloonTip((int)timeout.TotalMilliseconds, header, content, icon);
        }

        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="icon">图标</param>
        /// <param name="header">头部</param>
        /// <param name="content">内容</param>
        public static void ShowNotify(ToolTipIcon icon, string header, string content)
        {
            ShowNotify(TimeSpan.FromSeconds(5), icon, header, content);
        }
    }
}
