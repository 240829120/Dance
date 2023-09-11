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
    partial class DanceMqttTopicAction : DanceMqttTopicActionBase
    {
        /// <summary>
        /// 主题行为
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="action">行为</param>
        public DanceMqttTopicAction(string topic, string route, Action<string> action) : base(topic, route)
        {
            this.Func = m =>
            {
                action(m);
                return string.Empty;
            };
        }

        /// <summary>
        /// 主题行为
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="func">行为</param>
        public DanceMqttTopicAction(string topic, string route, Func<string, string> func) : base(topic, route)
        {
            this.Func = func;
        }

        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 行为
        /// </summary>
        public Func<string, string> Func { get; private set; }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>执行结果</returns>
        public override byte[]? Execute(byte[]? data)
        {
            if (data == null)
                return null;

            string request = this.Encoding.GetString(data);
            string response = this.Func(request);

            return this.Encoding.GetBytes(response);
        }
    }
}
