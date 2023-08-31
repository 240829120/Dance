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
    public class DanceHttpErrorEventArgs : DanceHttpRequestEventArgs
    {
        /// <summary>
        /// Http请求异常事件参数
        /// </summary>
        /// <param name="context">Http扩展上下文</param>
        public DanceHttpErrorEventArgs(DanceHttpExpansionContext context) : base(context)
        {
            this.Error = context.Error;
            this.ErrorTime = context.ErrorTime;
        }

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ErrorTime { get; private set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public Exception? Error { get; private set; }
    }
}
