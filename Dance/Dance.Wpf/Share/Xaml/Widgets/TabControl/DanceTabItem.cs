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
    /// Tab项
    /// </summary>
    public class DanceTabItem : TabItem
    {
        static DanceTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTabItem), new FrameworkPropertyMetadata(typeof(DanceTabItem)));
        }
    }
}
