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
    /// 时间线轨道头部
    /// </summary>
    public class DanceTimelineTrackHeader : ContentControl
    {
        static DanceTimelineTrackHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrackHeader), new FrameworkPropertyMetadata(typeof(DanceTimelineTrackHeader)));
        }
    }
}
