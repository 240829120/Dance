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
    /// 改变窗口大小容器
    /// </summary>
    public class DanceWindowResizeContainerPart : ContentControl
    {
        static DanceWindowResizeContainerPart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceWindowResizeContainerPart), new FrameworkPropertyMetadata(typeof(DanceWindowResizeContainerPart)));
        }
    }
}
