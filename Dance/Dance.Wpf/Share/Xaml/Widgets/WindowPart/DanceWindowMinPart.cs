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
    /// 窗口最小化部件
    /// </summary>
    public class DanceWindowMinPart : Button
    {
        static DanceWindowMinPart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceWindowMinPart), new FrameworkPropertyMetadata(typeof(DanceWindowMinPart)));
        }

        /// <summary>
        /// 点击
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();

            if (Window.GetWindow(this) is not Window window)
                return;

            window.WindowState = WindowState.Minimized;
        }
    }
}
