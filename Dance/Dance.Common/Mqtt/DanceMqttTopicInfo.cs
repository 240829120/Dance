using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Common
{
    /// <summary>
    /// Mqtt主题信息
    /// </summary>
    /// <remarks>
    /// Mqtt主题信息
    /// </remarks>
    /// <param name="requestData">请求数据</param>
    public class DanceMqttTopicInfo(byte[]? requestData) : DanceObject, IDisposable
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        public string RequestID { get; private set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 请求数据
        /// </summary>
        public byte[]? RequestData { get; private set; } = requestData;

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime? ResponseTime { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public byte[]? ResponseData { get; set; }

        /// <summary>
        /// 是否返回
        /// </summary>
        public bool IsResponse { get; set; }
    }
}
