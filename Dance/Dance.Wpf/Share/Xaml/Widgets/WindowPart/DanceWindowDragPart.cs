using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 窗口拖拽部件
    /// </summary>
    public class DanceWindowDragPart : Control
    {
        static DanceWindowDragPart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceWindowDragPart), new FrameworkPropertyMetadata(typeof(DanceWindowDragPart)));
        }

        #region IsDoubleClickEnabled -- 是否启用双击

        /// <summary>
        /// 是否启用双击
        /// </summary>
        public bool IsDoubleClickEnabled
        {
            get { return (bool)GetValue(IsDoubleClickEnabledProperty); }
            set { SetValue(IsDoubleClickEnabledProperty, value); }
        }

        /// <summary>
        /// 是否启用双击
        /// </summary>
        public static readonly DependencyProperty IsDoubleClickEnabledProperty =
            DependencyProperty.Register("IsDoubleClickEnabled", typeof(bool), typeof(DanceWindowDragPart), new PropertyMetadata(true));

        #endregion

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            Window? window = Window.GetWindow(this);
            if (window == null)
                return;

            window?.DragMove();
        }

        /// <summary>
        /// 鼠标点击
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!this.IsDoubleClickEnabled || e.ClickCount != 2)
                return;

            Window? window = Window.GetWindow(this);
            if (window == null)
                return;

            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
    }
}
