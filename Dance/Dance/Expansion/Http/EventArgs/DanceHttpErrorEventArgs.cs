using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Http请求异常事件参数
    /// </summary>
    /// <param name="context">Http扩展上下文</param>
    public class DanceHttpErrorEventArgs(DanceHttpExpansionContext context) : DanceHttpRequestEventArgs(context)
    {
        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ErrorTime { get; private set; } = context.ErrorTime;

        /// <summary>
        /// 错误信息
        /// </summary>
        public Exception? Error { get; private set; } = context.Error;
    }
}
