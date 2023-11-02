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
    /// 分组项
    /// </summary>
    public class DanceGroupItem : ContentControl
    {
        static DanceGroupItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceGroupItem), new FrameworkPropertyMetadata(typeof(DanceGroupItem)));
        }
    }
}
