using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 行为时刻
    /// </summary>
    public class DanceActionClock : Clock
    {
        /// <summary>
        /// 行为时刻
        /// </summary>
        /// <param name="timeline">事件线</param>
        /// <param name="action">行为</param>
        public DanceActionClock(Timeline timeline, Action action) : base(timeline)
        {
            this.Action = action;
        }

        /// <summary>
        /// 行为
        /// </summary>
        public Action Action { get; private set; }

        protected override void DiscontinuousTimeMovement()
        {
            this.Action?.Invoke();
        }
    }
}
