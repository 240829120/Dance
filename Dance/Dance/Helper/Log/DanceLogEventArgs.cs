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
    /// <param name="trigger">触发者</param>
    /// <param name="content">内容</param>
    /// <param name="exception">异常信息</param>
    public class DanceLogEventArgs(string trigger, string content, Exception? exception = null) : EventArgs
    {
        /// <summary>
        /// 触发者
        /// </summary>
        public string Trigger { get; private set; } = trigger;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; } = content;

        /// <summary>
        /// 触发时间
        /// </summary>
        public DateTime Time { get; private set; } = DateTime.Now;

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception? Exception { get; private set; } = exception;

        /// <summary>
        /// 其他数据
        /// </summary>
        public object? Tag { get; set; }
    }
}
