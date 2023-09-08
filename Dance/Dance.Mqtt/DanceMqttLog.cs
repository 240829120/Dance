using MQTTnet.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt日志
    /// </summary>
    public class DanceMqttLog : IMqttNetLogger
    {
        /// <summary>
        /// Mqtt日志
        /// </summary>
        /// <param name="owner">所有者</param>
        public DanceMqttLog(object owner)
        {
            this.Owner = owner;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 所有者
        /// </summary>
        public object Owner { get; private set; }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="source">源</param>
        /// <param name="message">消息</param>
        /// <param name="parameters">参数</param>
        /// <param name="exception">异常</param>
        public void Publish(MqttNetLogLevel logLevel, string source, string message, object[] parameters, Exception exception)
        {
            string format = (string.IsNullOrWhiteSpace(message) || parameters == null || parameters.Length == 0) ? message : string.Format(message, parameters);

            DanceLog.TriggerLogging(this.Owner, new DanceLogEventArgs("mqtt", $"hashcode: {this.GetHashCode()} level: {logLevel}, source: {source}, message: {format}", exception));
        }
    }
}
