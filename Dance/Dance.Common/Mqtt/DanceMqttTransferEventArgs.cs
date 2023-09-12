using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Common
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
        /// <param name="userProperties">用户数据</param>
        /// <param name="data">数据</param>
        public DanceMqttTransferEventArgs(string topic, Dictionary<string, string>? userProperties, byte[]? data)
        {
            this.Topic = topic;
            this.UserProperties = userProperties;
            this.Data = data;
        }

        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { get; private set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        public Dictionary<string, string>? UserProperties { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public byte[]? Data { get; private set; }
    }
}