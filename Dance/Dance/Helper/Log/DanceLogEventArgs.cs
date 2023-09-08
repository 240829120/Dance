using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 日志事件参数
    /// </summary>
    public class DanceLogEventArgs : EventArgs
    {
        /// <summary>
        /// 日志事件参数
        /// </summary>
        /// <param name="trigger">触发者</param>
        /// <param name="content">内容</param>
        /// <param name="exception">异常信息</param>
        public DanceLogEventArgs(string trigger, string content, Exception? exception = null)
        {
            this.Trigger = trigger;
            this.Content = content;
            this.Exception = exception;
        }

        /// <summary>
        /// 触发者
        /// </summary>
        public string Trigger { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 触发时间
        /// </summary>
        public DateTime Time { get; private set; } = DateTime.Now;

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception? Exception { get; private set; }

        /// <summary>
        /// 其他数据
        /// </summary>
        public object? Tag { get; set; }
    }
}
