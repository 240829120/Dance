using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt客户端
    /// </summary>
    public class DanceMqttClient : DanceMqttClientBase
    {
        /// <summary>
        /// Mqtt客户端
        /// </summary>
        /// <param name="option">设置</param>
        public DanceMqttClient(DanceMqttClientOption option) : base(option)
        {

        }

        // ===============================================================================================
        // Field

        /// <summary>
        /// 主题字典
        /// </summary>
        private readonly ConcurrentDictionary<string, DanceMqttTopicInfo> TopicInfoDic = new();

        /// <summary>
        /// 主题执行器集合
        /// </summary>
        private readonly ConcurrentDictionary<string, DanceMqttTopicExecuter> TopicExecuterDic = new();

        // ===============================================================================================
        // Event

        /// <summary>
        /// 发送后触发
        /// </summary>
        public event EventHandler<DanceMqttTransferEventArgs>? Sended;

        /// <summary>
        /// 接收后触发
        /// </summary>
        public event EventHandler<DanceMqttTransferEventArgs>? Received;

        // ===============================================================================================
        // Public Function

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="data">数据</param>
        public virtual async Task PublishAsync(string topic, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            byte[] buffer = Encoding.UTF8.GetBytes(json);

            await this.PublishAsync(topic, string.Empty, null, buffer);

            this.Sended?.Invoke(this, new(topic, string.Empty, null, json));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="clientId">客户端ID</param>
        /// <param name="data">数据</param>
        public virtual async Task PublishAsync(string topic, string clientId, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            Dictionary<string, string> userProperties = new()
            {
                [DanceMqttPropertyKeys.DANCE_CLIENT_ID] = clientId
            };

            await this.PublishAsync(topic, string.Empty, userProperties, buffer);

            this.Sended?.Invoke(this, new(topic, string.Empty, userProperties, json));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="topic">主题</param>
        /// <param name="responseTopic">返回主题</param>
        /// <param name="clientId">客户端ID</param>
        /// <param name="data">数据</param>
        /// <returns>返回</returns>
        public async Task<T?> PublishAsync<T>(string topic, string responseTopic, string clientId, object data) where T : new()
        {
            return await this.PublishAsync<T>(topic, responseTopic, clientId, data, this.Option.Timeout);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="topic">主题</param>
        /// <param name="responseTopic">返回主题</param>
        /// <param name="clientId">客户端ID</param>
        /// <param name="data">数据</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回</returns>
        public virtual async Task<T?> PublishAsync<T>(string topic, string responseTopic, string clientId, object data, TimeSpan timeout) where T : new()
        {
            string json = JsonConvert.SerializeObject(data);
            DanceMqttTopicInfo info = new(json);

            if (!this.TopicInfoDic.TryAdd(info.RequestID, info))
                throw new Exception($"add request error. id: {info.RequestID}, topic: {topic}, responseTopic: {responseTopic}, data: {json}");

            await this.PublishAsync(topic, clientId, data);

            while (!info.IsResponse && (DateTime.Now - info.RequestTime) < timeout)
            {
                await Task.Delay(500);
            }

            this.TopicInfoDic.TryRemove(info.RequestID, out _);

            if (string.IsNullOrWhiteSpace(info.ResponseData))
                return default;

            return JsonConvert.DeserializeObject<T>(info.ResponseData);
        }

        // ===============================================================================================
        // Protected Function

        /// <summary>
        /// 当接收消息时触发
        /// </summary>
        protected override Task OnReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                Dictionary<string, string> userProperties = e.ApplicationMessage.UserProperties?.ToDictionary(p => p.Name, p => p.Value) ?? new();

                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_REQUEST_ID, out string? requestId);

                string topic = e.ApplicationMessage.Topic;
                string responseTopic = e.ApplicationMessage.ResponseTopic;
                byte[] buffer = e.ApplicationMessage.PayloadSegment.ToArray();
                string json = Encoding.UTF8.GetString(buffer);

                // 普通数据
                if (string.IsNullOrWhiteSpace(requestId))
                {
                    this.TopicExecuterDic.TryGetValue(topic, out DanceMqttTopicExecuter? executer);
                    executer?.Execute(json);

                    return Task.CompletedTask;
                }

                // 拥有请求ID的数据
                this.TopicInfoDic.TryGetValue(requestId, out DanceMqttTopicInfo? info);
                if (info == null)
                    return Task.CompletedTask;

                info.ResponseData = json;
                info.ResponseTime = DateTime.Now;
                info.IsResponse = true;

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return Task.CompletedTask;
            }
        }
    }
}