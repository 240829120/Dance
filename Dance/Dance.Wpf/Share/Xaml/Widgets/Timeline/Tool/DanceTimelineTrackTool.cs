using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 轨道工具
    /// </summary>
    public class DanceTimelineTrackTool : DanceTimelineToolBase
    {
        /// <summary>
        /// 轨道工具
        /// </summary>
        /// <param name="timeline">时间线</param>
        public DanceTimelineTrackTool(DanceTimeline timeline) : base(timeline)
        {

        }

        /// <summary>
        /// 选择轨道
        /// </summary>
        /// <param name="index">轨道索引</param>
        public void SelectTrack(int index)
        {
            if (this.Timeline.PART_HeaderItems == null)
                return;

            DanceTimelineTrackHeaderPanel? headerPanel = this.Timeline.PART_HeaderItems.GetVisualTreeDescendants<DanceTimelineTrackHeaderPanel>().FirstOrDefault();
            if (headerPanel != null)
            {
                int i = 0;
                foreach (DanceTimelineTrackHeader item in headerPanel.Children)
                {
                    item.IsSelected = index == i++;
                }
            }

            DanceTimelineTrackHeaderPanel? itemsPanel = this.Timeline.PART_TrackItems.GetVisualTreeDescendants<DanceTimelineTrackHeaderPanel>().FirstOrDefault();
            DanceTimelineTrack? selectedTrack = itemsPanel?.Children?.GetItemAt(index) as DanceTimelineTrack;

            this.Timeline.InvokeTrackSelectionChanged(selectedTrack);
        }
    }
}