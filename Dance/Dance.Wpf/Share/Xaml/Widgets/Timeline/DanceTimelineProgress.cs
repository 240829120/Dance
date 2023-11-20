using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线进度
    /// </summary>
    public class DanceTimelineProgress : Control
    {
        static DanceTimelineProgress()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineProgress), new FrameworkPropertyMetadata(typeof(DanceTimelineProgress)));
        }

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? Timeline;

        #region CurrentTime -- 当前时间

        /// <summary>
        /// 当前时间
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return (TimeSpan)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(TimeSpan), typeof(DanceTimelineProgress), new PropertyMetadata(TimeSpan.Zero, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceTimelineProgress progress)
                    return;

                progress.UpdateMargin();
            })));

        #endregion

        /// <summary>
        /// 更新边距
        /// </summary>
        public void UpdateMargin()
        {
            this.TryGetOwner();
            if (this.Timeline == null)
                return;

            this.Margin = new Thickness((int)(this.CurrentTime.TotalHours * DanceTimeline.ONE_HOUR_DEFAULT_WIDTH * this.Timeline.Zoom - this.ActualWidth / 2d), 0, 0, 0);
        }

        private void TryGetOwner()
        {
            this.Timeline ??= DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);
        }

    }
}
