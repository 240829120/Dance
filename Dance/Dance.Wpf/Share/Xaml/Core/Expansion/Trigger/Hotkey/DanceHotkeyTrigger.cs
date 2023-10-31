using Gma.System.MouseKeyHook;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Dance.Wpf
{
    /// <summary>
    /// 热键触发器
    /// </summary>
    public class DanceHotkeyTrigger : DependencyObject
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly static ILog log = LogManager.GetLogger(typeof(DanceHotkeyTrigger));

        static DanceHotkeyTrigger()
        {
            KeyboardEvents = Hook.AppEvents();
            KeyboardEvents.KeyDown -= KeyboardEvents_KeyDown;
            KeyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }

        /// <summary>
        /// 键盘事件
        /// </summary>
        private static readonly IKeyboardEvents KeyboardEvents;

        /// <summary>
        /// 缓存
        /// </summary>
        private static readonly List<FrameworkElement> Caches = new();

        #region HotkeyBindings -- 热键绑定集合

        /// <summary>
        /// 获取热键绑定集合
        /// </summary>
        public static DanceHotkeyCollection GetHotkeyBindings(DependencyObject obj)
        {
            return (DanceHotkeyCollection)obj.GetValue(HotkeyBindingsProperty);
        }

        /// <summary>
        /// 设置热键绑定集合
        /// </summary>
        public static void SetHotkeyBindings(DependencyObject obj, DanceHotkeyCollection value)
        {
            obj.SetValue(HotkeyBindingsProperty, value);
        }

        /// <summary>
        /// 获取热键绑定集合
        /// </summary>
        public static readonly DependencyProperty HotkeyBindingsProperty =
            DependencyProperty.RegisterAttached("HotkeyBindings", typeof(DanceHotkeyCollection), typeof(DanceHotkeyTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                if (e.NewValue is not DanceHotkeyCollection collection)
                {
                    element.Loaded -= Element_Loaded;
                    element.Unloaded -= Element_Unloaded;

                    Unregister(element);
                }
                else
                {
                    element.Loaded -= Element_Loaded;
                    element.Loaded += Element_Loaded;
                    element.Unloaded -= Element_Unloaded;
                    element.Unloaded += Element_Unloaded;

                    Register(element);
                }

            })));

        #endregion

        /// <summary>
        /// 按键输入
        /// </summary>
        private static void KeyboardEvents_KeyDown(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                Application.Current?.Dispatcher.BeginInvoke(() =>
                {
                    Execute(e);
                });
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        private static void Execute(System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                lock (Caches)
                {
                    foreach (FrameworkElement element in Caches)
                    {
                        DanceHotkeyCollection? collection = GetHotkeyBindings(element);
                        if (collection == null)
                            continue;

                        foreach (DanceHotkeyBinding item in collection)
                        {
                            if (item == null || item.Command == null)
                                continue;

                            if (item.Modifiers == e.Modifiers && item.Key == e.KeyCode)
                            {
                                try
                                {
                                    if (!item.Command.CanExecute(item.CommandParameter))
                                        continue;

                                    item.Command.Execute(item.CommandParameter);
                                }
                                catch (Exception ex)
                                {
                                    log.Error(ex);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 元素加载
        /// </summary>
        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            Register(element);
        }

        /// <summary>
        /// 元素卸载
        /// </summary>
        private static void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            Unregister(element);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="element">元素</param>
        private static void Register(FrameworkElement element)
        {
            DanceHotkeyCollection? collection = GetHotkeyBindings(element);
            collection?.ForEach(p => p.DataContext = element.DataContext);

            lock (Caches)
            {
                if (Caches.Contains(element))
                    return;

                Caches.Add(element);
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="element">元素</param>
        private static void Unregister(FrameworkElement element)
        {
            DanceHotkeyCollection? collection = GetHotkeyBindings(element);
            collection?.ForEach(p => p.DataContext = null);

            lock (Caches)
            {
                if (Caches.Contains(element))
                {
                    Caches.Remove(element);
                }
            }
        }
    }
}