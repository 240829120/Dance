using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 循环信息
    /// </summary>
    public class DanceLoopInfo : DanceObject
    {
        /// <summary>
        /// 键
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 上一次触发时间
        /// </summary>
        public DateTime LastTriggerTime { get; set; }

        /// <summary>
        /// 等待时间
        /// </summary>
        public TimeSpan WaitTime { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        public Action? Action { get; set; }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Action = null;
        }
    }
}
