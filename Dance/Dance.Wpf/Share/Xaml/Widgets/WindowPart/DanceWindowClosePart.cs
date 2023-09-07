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
    /// 窗口关闭部件
    /// </summary>
    public class DanceWindowClosePart : Button
    {
        static DanceWindowClosePart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceWindowClosePart), new FrameworkPropertyMetadata(typeof(DanceWindowClosePart)));
        }

        /// <summary>
        /// 点击
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();

            if (Window.GetWindow(this) is not Window window)
                return;

            window.Close();
        }
    }
}
