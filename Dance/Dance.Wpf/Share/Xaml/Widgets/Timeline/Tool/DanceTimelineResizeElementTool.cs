using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线修改宽度工具
    /// </summary>
    public class DanceTimelineResizeElementTool : DanceTimelineToolBase
    {
        /// <summary>
        /// 时间线修改宽度工具
        /// </summary>
        /// <param name="timeline">时间线</param>
        public DanceTimelineResizeElementTool(DanceTimeline timeline) : base(timeline)
        {

        }

        /// <summary>
        /// 尝试改变元素大小
        /// </summary>
        /// <param name="resizeInfo">改变元素大小信息</param>
        /// <returns>是否成功改变</returns>
        public bool TryResizeElement(DanceTimelineResizeElementInfo resizeInfo)
        {
            if (resizeInfo.Resize.ResizeType == DanceTimelineElementResizeType.BeginTime)
            {
                return this.TryResizeBeginTime(resizeInfo);
            }
            else
            {
                return this.TryResizeEndTime(resizeInfo);
            }
        }

        /// <summary>
        /// 尝试改变开始时间
        /// </summary>
        /// <param name="resizeInfo">改变元素大小信息</param>
        /// <returns>是否成功改变</returns>
        private bool TryResizeBeginTime(DanceTimelineResizeElementInfo resizeInfo)
        {
            if (resizeInfo.Element.OwnerTrackPanel == null)
                return false;

            resizeInfo.RealTime = resizeInfo.RealTime < TimeSpan.Zero ? TimeSpan.Zero : resizeInfo.RealTime;

            if (resizeInfo.RealTime >= resizeInfo.Element.EndTime || resizeInfo.Element.EndTime - resizeInfo.RealTime < DanceTimelineConstants.MIN_ELEMENT_WIDTH)
                return false;

            DanceTimelineElement? maxRight = null;

            foreach (DanceTimelineElement item in resizeInfo.Element.OwnerTrackPanel.Children)
            {
                if (item == resizeInfo.Element)
                    continue;

                if (!(item.EndTime <= resizeInfo.Element.BeginTime || item.BeginTime >= resizeInfo.Element.EndTime))
                    return false;

                if (item.EndTime > resizeInfo.Element.BeginTime)
                    continue;

                if (maxRight == null || maxRight.EndTime < item.EndTime)
                {
                    maxRight = item;
                    continue;
                }
            }

            if (maxRight == null)
                return true;

            resizeInfo.RealTime = this.Timeline.GetEffectiveTimeSpan(resizeInfo.RealTime);
            resizeInfo.RealTime = (resizeInfo.RealTime < maxRight.EndTime) ? maxRight.EndTime : resizeInfo.RealTime;

            if (resizeInfo.RealTime >= resizeInfo.Element.EndTime || resizeInfo.Element.EndTime - resizeInfo.RealTime < DanceTimelineConstants.MIN_ELEMENT_WIDTH)
                return false;

            return true;
        }

        /// <summary>
        /// 尝试改变结束时间
        /// </summary>
        /// <param name="resizeInfo">改变元素大小信息</param>
        /// <returns>是否成功改变</returns>
        private bool TryResizeEndTime(DanceTimelineResizeElementInfo resizeInfo)
        {
            if (resizeInfo.Element.OwnerTrackPanel == null)
                return false;

            resizeInfo.RealTime = resizeInfo.RealTime > this.Timeline.Duration ? this.Timeline.Duration : resizeInfo.RealTime;

            if (resizeInfo.RealTime <= resizeInfo.Element.BeginTime || resizeInfo.RealTime - resizeInfo.Element.BeginTime < DanceTimelineConstants.MIN_ELEMENT_WIDTH)
                return false;

            DanceTimelineElement? maxLeft = null;

            foreach (DanceTimelineElement item in resizeInfo.Element.OwnerTrackPanel.Children)
            {
                if (item == resizeInfo.Element)
                    continue;

                if (!(item.EndTime <= resizeInfo.Element.BeginTime || item.BeginTime >= resizeInfo.Element.EndTime))
                    return false;

                if (item.BeginTime < resizeInfo.Element.EndTime)
                    continue;

                if (maxLeft == null || maxLeft.BeginTime > item.BeginTime)
                {
                    maxLeft = item;
                    continue;
                }
            }

            if (maxLeft == null)
                return true;

            resizeInfo.RealTime = this.Timeline.GetEffectiveTimeSpan(resizeInfo.RealTime);
            resizeInfo.RealTime = (resizeInfo.RealTime < maxLeft.BeginTime) ? resizeInfo.RealTime : maxLeft.BeginTime;

            if (resizeInfo.RealTime <= resizeInfo.Element.BeginTime || resizeInfo.RealTime - resizeInfo.Element.BeginTime < DanceTimelineConstants.MIN_ELEMENT_WIDTH)
                return false;

            return true;
        }
    }
}
