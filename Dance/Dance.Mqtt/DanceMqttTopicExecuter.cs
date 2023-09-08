using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// 主题执行器
    /// </summary>
    public class DanceMqttTopicExecuter : DanceObject, IDisposable
    {
        /// <summary>
        /// 主题执行器
        /// </summary>
        /// <param name="topic">路由</param>
        /// <param name="execute">执行器</param>
        public DanceMqttTopicExecuter(string topic, Func<string, string> execute)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException(nameof(topic));

            this.Topic = topic;
            this.Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { get; private set; }

        /// <summary>
        /// 执行器
        /// </summary>
        public Func<string, string> Execute { get; private set; }
    }
}