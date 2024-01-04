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
    /// <param name="topic">主题</param>
    /// <param name="userProperties">用户数据</param>
    /// <param name="data">数据</param>
    public class DanceMqttTransferEventArgs(string topic, Dictionary<string, string>? userProperties, byte[]? data) : EventArgs
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { get; private set; } = topic;

        /// <summary>
        /// 用户数据
        /// </summary>
        public Dictionary<string, string>? UserProperties { get; private set; } = userProperties;

        /// <summary>
        /// 数据
        /// </summary>
        public byte[]? Data { get; private set; } = data;
    }
}