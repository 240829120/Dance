using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        [NotNull]
        internal DanceTimeline? OwnerTimeline = null;

        // ==========================================================================================================================================
        // Property



        // ==========================================================================================================================================
        // Property

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
            return new DanceTimelineElement()
            {
                OwnerTimeline = this.OwnerTimeline,
                OwnerTrack = this
            };
        }
    }
}
