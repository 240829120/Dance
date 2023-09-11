using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt属性键
    /// </summary>
    public static class DanceMqttPropertyKeys
    {
        /// <summary>
        /// 消息类型
        /// <see cref="DanceMqttMessageType"/>
        /// </summary>
        public const string DANCE_MESSAGE_TYPE = "DANCE_MESSAGE_TYPE";

        /// <summary>
        /// 发送客户端ID
        /// </summary>
        public const string DANCE_SEND_CLIENT_ID = "DANCE_SEND_CLIENT_ID";

        /// <summary>
        /// 定向发送客户端ID
        /// </summary>
        public const string DANCE_TARGET_CLIENT_ID = "DANCE_TARGET_CLIENT_ID";

        /// <summary>
        /// 路由
        /// </summary>
        public const string DANCE_ROUTE = "DANCE_ROUTE";

        /// <summary>
        /// 请求ID
        /// </summary>
        public const string DANCE_REQUEST_ID = "DANCE_REQUEST_ID";
    }
}
