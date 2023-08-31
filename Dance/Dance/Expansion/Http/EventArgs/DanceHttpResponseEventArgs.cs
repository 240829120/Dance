using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Http请求返回事件参数
    /// </summary>
    public class DanceHttpResponseEventArgs : DanceHttpRequestEventArgs
    {
        /// <summary>
        /// Http请求事件参数
        /// </summary>
        /// <param name="context">Http扩展上下文</param>
        public DanceHttpResponseEventArgs(DanceHttpExpansionContext context) : base(context)
        {
            this.Response = context.Response;
            this.ResponseTime = context.ResponseTime;
        }

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ResponseTime { get; private set; }

        /// <summary>
        /// 返回
        /// </summary>
        public string? Response { get; private set; }
    }
}
