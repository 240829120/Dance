using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 日志
    /// </summary>
    public class DanceLogAppender : AppenderSkeleton
    {
        /// <summary>
        /// 记录日志时触发
        /// </summary>
        public static event EventHandler<DanceLogEventArgs>? Logging;

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="loggingEvent">日志事件</param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            try
            {
#if DEBUG
                Debug.WriteLine($"{loggingEvent.RenderedMessage}\r\n");
#endif

                Logging?.Invoke(this, new DanceLogEventArgs("log4net", $"{loggingEvent.RenderedMessage}\r\n", loggingEvent.ExceptionObject));
            }
            catch
            {
                // Nothing todo.
            }
        }
    }
}
