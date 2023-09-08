using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt传输事件参数
    /// </summary>
    public class DanceMqttTransferEventArgs : EventArgs
    {
        /// <summary>
        /// Mqtt消息发送事件参数
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="responseTopic">返回主题</param>
        /// <param name="userProperties">用户数据</param>
        /// <param name="json">Json数据</param>
        public DanceMqttTransferEventArgs(string topic, string responseTopic, Dictionary<string, string>? userProperties, string json)
        {
            this.Topic = topic;
            this.ResponseTopic = responseTopic;
            this.UserProperties = userProperties;
            this.Json = json;
        }

        /// <summary>
        /// 返回主题
        /// </summary>
        public string ResponseTopic { get; private set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { get; private set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        public Dictionary<string, string>? UserProperties { get; private set; }

        /// <summary>
        /// Json数据
        /// </summary>
        public string Json { get; private set; }
    }
}