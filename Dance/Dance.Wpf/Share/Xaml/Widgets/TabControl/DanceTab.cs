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
    /// Tab控件
    /// </summary>
    public class DanceTab : TabControl
    {
        static DanceTab()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTab), new FrameworkPropertyMetadata(typeof(DanceTab)));
        }
    }
}
