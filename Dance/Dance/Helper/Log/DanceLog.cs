using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 日志
    /// </summary>
    public static class DanceLog
    {
        /// <summary>
        /// 记录日志时触发
        /// </summary>
        public static event EventHandler<DanceLogEventArgs>? Logging;

        /// <summary>
        /// 触发日志
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="e">参数</param>
        public static void TriggerLogging(object sender, DanceLogEventArgs e)
        {
            try
            {
                Logging?.Invoke(sender, e);
            }
            catch
            {
                // nothing todo.
            }
        }
    }
}
