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
    /// 分割线
    /// </summary>
    public class DanceSeparator : ContentControl
    {
        static DanceSeparator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceSeparator), new FrameworkPropertyMetadata(typeof(DanceSeparator)));
        }
    }
}
