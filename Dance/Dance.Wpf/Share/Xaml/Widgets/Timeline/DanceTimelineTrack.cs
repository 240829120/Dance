using System;
using System.Collections.Generic;
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

        // =============================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? Timeline;

        // =============================================================================================
        // Property

        #region IsSelected -- 是否被选中

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(DanceTimelineTrack), new PropertyMetadata(false));

        #endregion

        // =============================================================================================
        // Override

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceTimelineTrackItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTimelineTrackItem() { Timeline = this.Timeline, TimelineTrack = this };
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.IsSelected = true;
        }
    }
}
