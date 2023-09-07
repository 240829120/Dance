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
    /// 窗口最大化或正常化部件
    /// </summary>
    public class DanceWindowMaxOrNormalPart : CheckBox
    {
        static DanceWindowMaxOrNormalPart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceWindowMaxOrNormalPart), new FrameworkPropertyMetadata(typeof(DanceWindowMaxOrNormalPart)));
        }

        public DanceWindowMaxOrNormalPart()
        {
            this.Loaded += DanceWindowMaxOrNormalPart_Loaded;
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceWindowMaxOrNormalPart_Loaded(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is not Window window)
                return;

            this.IsChecked = window.WindowState == WindowState.Maximized;
        }

        /// <summary>
        /// 点击
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();

            if (Window.GetWindow(this) is not Window window)
                return;

            window.WindowState = this.IsChecked == true ? WindowState.Maximized : WindowState.Normal;
        }
    }
}
