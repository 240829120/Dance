using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 延时信息
    /// </summary>
    public class DanceDelayInfo : DanceObject
    {
        /// <summary>
        /// 键值
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        public Action? Action { get; set; }

        /// <summary>
        /// 延时时间
        /// </summary>
        public TimeSpan DelayTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Action = null;
        }
    }
}
