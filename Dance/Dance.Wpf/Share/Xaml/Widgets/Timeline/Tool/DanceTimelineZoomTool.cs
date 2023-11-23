using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线缩放工具
    /// </summary>
    public class DanceTimelineZoomTool : DanceTimelineToolBase
    {
        /// <summary>
        /// 时间线工具
        /// </summary>
        /// <param name="timeline">时间线</param>
        public DanceTimelineZoomTool(DanceTimeline timeline) : base(timeline)
        {
            timeline.MouseWheel -= MouseWheel;
            timeline.MouseWheel += MouseWheel;
        }

        /// <summary>
        /// 鼠标滚轮滚动
        /// </summary>
        private void MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
                return;

            double dest = this.Timeline.Zoom + (e.Delta > 0 ? 1 : -1);
            if (dest < DanceTimeline.MIN_ZOOM || dest > DanceTimeline.MAX_ZOOM)
                return;

            this.Timeline.Zoom = dest;
        }
    }
}