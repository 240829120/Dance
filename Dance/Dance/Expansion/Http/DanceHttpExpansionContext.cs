using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Http扩展上下文
    /// </summary>
    public class DanceHttpExpansionContext
    {
        /// <summary>
        /// Http扩展上下文
        /// </summary>
        /// <param name="url">Url地址</param>
        public DanceHttpExpansionContext(string url)
        {
            this.Url = url;
        }

        /// <summary>
        /// 总请求ID
        /// </summary>
        private static long TOTAL_REQUEST_ID;

        /// <summary>
        /// 编号
        /// </summary>
        public long ID { get; private set; } = TOTAL_REQUEST_ID++;

        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; private set; }

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ResponseTime { get; private set; }

        /// <summary>
        /// 报错时间
        /// </summary>
        public DateTime ErrorTime { get; private set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        public string? Request { get; private set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string? Response { get; private set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public Exception? Error { get; private set; }

        /// <summary>
        /// 创建Http请求事件参数
        /// </summary>
        /// <param name="request">请求数据</param>
        /// <returns>Http请求事件参数</returns>
        public DanceHttpRequestEventArgs CreateRequestEventArgs(string? request)
        {
            this.Request = request;
            this.RequestTime = DateTime.Now;

            return new(this);
        }

        /// <summary>
        /// 创建Http返回事件参数
        /// </summary>
        /// <param name="response">返回数据</param>
        /// <returns>Http返回事件参数</returns>
        public DanceHttpResponseEventArgs CreateResponseEventArgs(string? response)
        {
            this.Response = response;
            this.ResponseTime = DateTime.Now;

            return new(this);
        }

        /// <summary>
        /// 创建Http错误事件参数
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns>Http错误事件参数</returns>
        public DanceHttpErrorEventArgs CreateErrorEventArgs(Exception? error)
        {
            this.Error = error;
            this.ErrorTime = DateTime.Now;

            return new(this);
        }
    }
}
