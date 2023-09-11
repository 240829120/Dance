using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt消息类型
    /// </summary>
    public static class DanceMqttMessageType
    {
        /// <summary>
        /// 消息数据
        /// </summary>
        public const string MESSAGE = "MESSAGE";

        /// <summary>
        /// 请求数据
        /// </summary>
        public const string REQUEST = "REQUEST";

        /// <summary>
        /// 返回数据
        /// </summary>
        public const string RESPONSE = "RESPONSE";
    }
}
