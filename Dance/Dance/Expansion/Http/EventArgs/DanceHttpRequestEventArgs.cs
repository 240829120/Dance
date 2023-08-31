using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Http请求事件参数
    /// </summary>
    public class DanceHttpRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Http请求事件参数
        /// </summary>
        /// <param name="context">Http扩展上下文</param>
        public DanceHttpRequestEventArgs(DanceHttpExpansionContext context)
        {
            this.ID = context.ID;
            this.Url = context.Url;
            this.Request = context.Request;
            this.RequestTime = context.RequestTime;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public long ID { get; private set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; private set; }

        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        public string? Request { get; private set; }
    }
}
