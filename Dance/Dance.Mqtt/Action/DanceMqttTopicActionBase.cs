using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// 主题行为
    /// </summary>
    public abstract class DanceMqttTopicActionBase : DanceObject
    {
        /// <summary>
        /// 主题行为
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="action">行为</param>
        public DanceMqttTopicActionBase(string topic, string route)
        {
            this.Topic = topic;
            this.Route = route;
        }

        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { get; private set; }

        /// <summary>
        /// 路由
        /// </summary>
        public string Route { get; private set; }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>执行结果</returns>
        public abstract byte[]? Execute(byte[]? data);
    }
}
