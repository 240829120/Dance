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
    /// 时间线轨道头部
    /// </summary>
    public class DanceTimelineTrackHeader : ContentControl
    {
        static DanceTimelineTrackHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTimelineTrackHeader), new FrameworkPropertyMetadata(typeof(DanceTimelineTrackHeader)));
        }

        // ==========================================================================================================================================
        // Field

        /// <summary>
        /// 所属时间线
        /// </summary>
        internal DanceTimeline? OwnerTimeline;

        /// <summary>
        /// 所属面板
        /// </summary>
        internal DanceTimelineTrackHeaderPanel? OwnerPanel;

        // ==========================================================================================================================================
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
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(DanceTimelineTrackHeader), new PropertyMetadata(false));

        #endregion

        // ==========================================================================================================================================
        // Override

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.OwnerTimeline = DanceXamlExpansion.GetVisualTreeParent<DanceTimeline>(this);
            this.OwnerPanel = DanceXamlExpansion.GetVisualTreeParent<DanceTimelineTrackHeaderPanel>(this);
        }

        /// <summary>
        /// 鼠标左键点击
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (this.OwnerTimeline == null || this.OwnerPanel == null)
                return;

            DanceTimelineTrackTool? tool = this.OwnerTimeline.GetTool<DanceTimelineTrackTool>();
            if (tool == null)
                return;

            e.Handled = true;

            int index = this.OwnerPanel.Children.IndexOf(this);
            tool.SelectTrack(index);
        }
    }
}
