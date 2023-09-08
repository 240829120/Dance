using MQTTnet.Formatter;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt客户端设置
    /// </summary>
    public class DanceMqttClientOption
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string? ClientID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 心跳间隔
        /// </summary>
        public TimeSpan Heartbeat { get; set; } = TimeSpan.FromSeconds(2);

        /// <summary>
        /// 保活时间
        /// </summary>
        public TimeSpan KeepAlive { get; set; } = TimeSpan.FromSeconds(60);

        /// <summary>
        /// 超时时间
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// 等级
        /// </summary>
        public MqttQualityOfServiceLevel Level { get; set; } = MqttQualityOfServiceLevel.ExactlyOnce;
    }
}
