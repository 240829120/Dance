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
    /// <param name="context">Http扩展上下文</param>
    public class DanceHttpRequestEventArgs(DanceHttpExpansionContext context) : EventArgs
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long ID { get; private set; } = context.ID;

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; private set; } = context.RequestTime;

        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; private set; } = context.Url;

        /// <summary>
        /// 请求数据
        /// </summary>
        public string? Request { get; private set; } = context.Request;
    }
}
