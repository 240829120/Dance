using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 物理引擎时间线
    /// </summary>
    public class DancePhysicsTimeLine : AnimationTimeline
    {
        /// <summary>
        /// 物理引擎时间线
        /// </summary>
        /// <param name="layout">物理引擎</param>
        public DancePhysicsTimeLine(DancePhysicsLayout layout)
        {
            this.Layout = layout;
            this.Duration = new Duration(TimeSpan.FromDays(1));
            this.FillBehavior = FillBehavior.HoldEnd;
        }

        /// <summary>
        /// 物理引擎布局
        /// </summary>
        public DancePhysicsLayout Layout { get; private set; }

        /// <summary>
        /// 目标属性类型
        /// </summary>
        public override Type TargetPropertyType => typeof(long);

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns>实例</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new DancePhysicsTimeLine(this.Layout);
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            if (this.Layout != null)
            {
                if (this.Layout.ItemsPanel == null)
                {
                    this.Layout.ItemsPanel = XamlExpansion.GetVisualTreeDescendants<DancePhysicsPanel>(Layout)?.FirstOrDefault();
                }
            }

            this.Layout?.ItemsPanel?.InvalidateVisual();

            return base.GetCurrentValue(defaultOriginValue, defaultDestinationValue, animationClock);
        }
    }
}
