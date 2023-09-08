using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceLogManager))]
    public class DanceLogManager : IDanceLogManager
    {
        /// <summary>
        /// 记录日志事件
        /// </summary>
        public event EventHandler<DanceLogEventArgs>? Logging;

        /// <summary>
        /// 触发
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="e">事件参数</param>
        public void TriggerLogging(object sender, DanceLogEventArgs e)
        {
            this.Logging?.Invoke(sender, e);
        }
    }
}
