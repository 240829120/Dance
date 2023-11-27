using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线拷贝移动工具
    /// </summary>
    public class DanceTimelineCopyMoveTool : DanceTimelineToolBase
    {
        /// <summary>
        /// 时间线拷贝移动工具
        /// </summary>
        /// <param name="timeline">时间线</param>
        public DanceTimelineCopyMoveTool(DanceTimeline timeline) : base(timeline)
        {
            timeline.KeyDown -= KeyDown;
            timeline.KeyDown += KeyDown;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            if (this.Timeline == null)
                return;

            this.Timeline.KeyDown -= KeyDown;
        }

        // ===================================================================================================
        // Private Function

        /// <summary>
        /// 键盘按下
        /// </summary>
        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Timeline.IsReadOnly)
                return;

            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                this.Timeline.Status = DanceTimelineStatus.CopyMoveElement;
            }
        }

        /// <summary>
        /// 尝试拷贝移动元素
        /// </summary>
        /// <param name="moveInfo">移动信息</param>
        /// <returns>是否可以移动</returns>
        public bool TryCopyMove(DanceTimelineMoveElementInfo moveInfo)
        {
            TimeSpan srcWidth = moveInfo.EndTime - moveInfo.BeginTime;
            TimeSpan destWidth = moveInfo.WantEndTime - moveInfo.WantBeginTime;

            if (srcWidth != destWidth || destWidth <= TimeSpan.Zero)
                return false;

            DanceTimelineTrackPanel? destPanel = moveInfo.DestTrack.GetVisualTreeDescendants<DanceTimelineTrackPanel>().FirstOrDefault();
            if (destPanel == null)
                return false;

            if (moveInfo.WantBeginTime < TimeSpan.Zero)
            {
                moveInfo.RealBeginTime = TimeSpan.Zero;
                moveInfo.RealEndTime = destWidth;
            }
            if (moveInfo.WantEndTime > this.Timeline.Duration)
            {
                moveInfo.RealEndTime = this.Timeline.Duration;
                moveInfo.RealBeginTime = moveInfo.RealEndTime - destWidth;
            }

            foreach (DanceTimelineElement item in destPanel.Children)
            {
                if (moveInfo.RealBeginTime <= item.EndTime && moveInfo.RealEndTime >= item.EndTime)
                {
                    moveInfo.RealBeginTime = item.EndTime;
                    moveInfo.RealEndTime = moveInfo.RealBeginTime + destWidth;

                    break;
                }

                if (moveInfo.RealEndTime >= item.BeginTime && moveInfo.RealBeginTime <= item.BeginTime)
                {
                    moveInfo.RealEndTime = item.BeginTime;
                    moveInfo.RealBeginTime = moveInfo.RealEndTime - destWidth;

                    break;
                }
            }

            foreach (DanceTimelineElement item in destPanel.Children)
            {
                if (!(moveInfo.RealEndTime <= item.BeginTime || moveInfo.RealBeginTime >= item.EndTime))
                {
                    return false;
                }
            }

            if (moveInfo.RealBeginTime < TimeSpan.Zero || moveInfo.RealEndTime > this.Timeline.Duration)
                return false;

            return true;
        }
    }
}
