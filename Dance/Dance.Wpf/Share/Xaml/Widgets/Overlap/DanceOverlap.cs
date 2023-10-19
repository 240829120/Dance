using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dance.Wpf
{
    /// <summary>
    /// 覆盖
    /// </summary>
    [ContentProperty("Content")]
    public class DanceOverlap : Control
    {
        public DanceOverlap()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            this.Loaded += DanceOverlap_Loaded;
            this.Unloaded += DanceOverlap_Unloaded;
            this.LayoutUpdated += DanceOverlap_LayoutUpdated;
        }

        // ================================================================================
        // Field

        /// <summary>
        /// 覆盖窗口
        /// </summary>
        private DanceOverlapWindow? OverlapWindow;

        // ================================================================================
        // Property

        #region Content -- 内容

        /// <summary>
        /// 内容
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(DanceOverlap), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceOverlap_Loaded(object sender, RoutedEventArgs e)
        {
            this.TryCreateOverlapWindow();

            if (this.OverlapWindow == null)
                return;

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow == null)
                return;

            this.OverlapWindow.Owner = parentWindow;
            parentWindow.LocationChanged -= ParentWindow_LocationChanged;
            parentWindow.LocationChanged += ParentWindow_LocationChanged;
            parentWindow.Closed -= ParentWindow_Closed;
            parentWindow.Closed += ParentWindow_Closed;
        }

        /// <summary>
        /// 卸载
        /// </summary>
        private void DanceOverlap_Unloaded(object sender, RoutedEventArgs e)
        {
            this.TryDesotryOverlapWindow();
        }

        /// <summary>
        /// 布局改变
        /// </summary>
        private void DanceOverlap_LayoutUpdated(object? sender, EventArgs e)
        {
            this.UpdatePosition();
        }

        /// <summary>
        /// 窗口位置改变
        /// </summary>
        private void ParentWindow_LocationChanged(object? sender, EventArgs e)
        {
            this.UpdatePosition();
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void ParentWindow_Closed(object? sender, EventArgs e)
        {
            this.TryDesotryOverlapWindow();
        }

        /// <summary>
        /// 尝试创建覆盖窗口
        /// </summary>
        private void TryCreateOverlapWindow()
        {
            if (this.OverlapWindow != null)
                return;

            this.OverlapWindow = new DanceOverlapWindow();
            this.OverlapWindow.Loaded += (s, e) => this.UpdatePosition();

            // 内容
            this.OverlapWindow.SetBinding(DanceOverlapWindow.ContentProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(DanceOverlap.ContentProperty),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            // 数据上下文
            this.OverlapWindow.SetBinding(DanceOverlapWindow.DataContextProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(DanceOverlap.DataContextProperty),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            // 透明度
            this.OverlapWindow.SetBinding(DanceOverlapWindow.OpacityProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(DanceOverlap.OpacityProperty),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            // 可见性
            DanceMappingConverter converter = new();
            converter.Items.Add(new() { From = true, To = Visibility.Visible });
            converter.Items.Add(new() { From = false, To = Visibility.Collapsed });
            converter.Items.Add(new() { From = null, To = Visibility.Collapsed });
            this.OverlapWindow.SetBinding(DanceOverlapWindow.VisibilityProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath("IsVisible"),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = converter
            });

            this.OverlapWindow.Show();
        }

        /// <summary>
        /// 尝试销毁覆盖窗口
        /// </summary>
        private void TryDesotryOverlapWindow()
        {
            this.OverlapWindow?.Close();
            this.OverlapWindow = null;

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow == null)
                return;

            parentWindow.LocationChanged -= ParentWindow_LocationChanged;
            parentWindow.Closed -= ParentWindow_Closed;
        }

        /// <summary>
        /// 更新位置
        /// </summary>
        private void UpdatePosition()
        {
            if (this.OverlapWindow == null)
                return;

            Point point = this.PointToScreen(new());
            this.OverlapWindow.Left = point.X / DanceXamlExpansion.DpiScale.DpiScaleX;
            this.OverlapWindow.Top = point.Y / DanceXamlExpansion.DpiScale.DpiScaleY;
            this.OverlapWindow.Width = this.ActualWidth;
            this.OverlapWindow.Height = this.ActualHeight;
        }
    }
}
