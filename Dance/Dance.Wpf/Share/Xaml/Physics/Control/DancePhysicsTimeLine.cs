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
    public class DancePhysicsTimeLine : Timeline
    {
        /// <summary>
        /// 物理引擎时间线
        /// </summary>
        /// <param name="layout">物理引擎</param>
        public DancePhysicsTimeLine(DancePhysicsLayout layout)
        {
            this.Layout = layout;
        }

        /// <summary>
        /// 物理引擎布局
        /// </summary>
        public DancePhysicsLayout Layout { get; private set; }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns>实例</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new DancePhysicsTimeLine(this.Layout);
        }

        /// <summary>
        /// 创建时刻
        /// </summary>
        /// <returns>时刻</returns>
        protected override Clock AllocateClock()
        {
            return new DanceActionClock(this, this.Update);
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            this.Layout?.InvalidateArrange();
        }
    }
}
