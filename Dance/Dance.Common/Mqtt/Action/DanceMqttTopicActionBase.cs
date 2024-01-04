using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Common
{
    /// <summary>
    /// 主题行为
    /// </summary>
    /// <remarks>
    /// 主题行为
    /// </remarks>
    /// <param name="topic">主题</param>
    /// <param name="route">路由</param>
    public abstract class DanceMqttTopicActionBase(string topic, string route) : DanceObject
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { get; private set; } = topic;

        /// <summary>
        /// 路由
        /// </summary>
        public string Route { get; private set; } = route;

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>执行结果</returns>
        public abstract byte[]? Execute(byte[]? data);
    }
}
