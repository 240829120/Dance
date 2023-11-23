using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线轨道
    /// </summary>
    public class DanceTimelineTrack : ItemsControl
    {
        static DanceTimelineTrack()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrack), new FrameworkPropertyMetadata(typeof(DanceTimelineTrack)));
        }

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? OwnerTimeline;

        /// <summary>
        /// 所属容器
        /// </summary>
        internal DanceTimelineTrackHeaderPanel? OwnerPanel;

        // ==========================================================================================================================================
        // Override

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.OwnerTimeline = this.GetVisualTreeParent<DanceTimeline>();
            this.OwnerPanel = this.GetVisualTreeParent<DanceTimelineTrackHeaderPanel>();
        }

        /// <summary>
        /// 是否是子元素容器
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceTimelineElement;
        }

        /// <summary>
        /// 获取子元素容器
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTimelineElement();
        }
    }
}
