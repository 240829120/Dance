using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        /// 主题行为集合
        /// </summary>
        private readonly ConcurrentDictionary<string, DanceMqttTopicActionBase> TopicActionDic = new();

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
        /// 添加主题行为
        /// </summary>
        /// <param name="action">主题行为</param>
        public virtual void AddTopicAction(DanceMqttTopicActionBase action)
        {
            this.TopicActionDic[$"[{action.Topic}]{action.Route}"] = action;
        }

        /// <summary>
        /// 移除主题行为
        /// </summary>
        /// <param name="topic">主题</param>
        public virtual void RemoveTopicAction(string topic)
        {
            this.TopicActionDic.TryRemove(topic, out _);
        }

        // ------------------------------------------------------------------------------------------

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="buffer">二进制数据</param>
        public async Task PublishAsync(string topic, string route, string targetClientId, string messageType, byte[] buffer)
        {
            await this.PublishAsync(topic, route, targetClientId, messageType, string.Empty, buffer);
        }

        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="buffer">数据</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回</returns>
        public virtual async Task<byte[]?> RequestAsync(string topic, string route, string targetClientId, byte[] buffer, TimeSpan timeout)
        {
            if (this.MqttClient == null)
                return null;

            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException(nameof(topic));

            if (string.IsNullOrWhiteSpace(targetClientId))
                throw new ArgumentNullException(nameof(targetClientId));

            DanceMqttTopicInfo info = new(buffer);

            if (!this.TopicInfoDic.TryAdd(info.RequestID, info))
                throw new Exception($"add request error. id: {info.RequestID}, topic: {topic}");

            await this.PublishAsync(topic, route, targetClientId, DanceMqttMessageType.REQUEST, info.RequestID, buffer);

            while (!info.IsResponse && (DateTime.Now - info.RequestTime) < timeout)
            {
                await Task.Delay(500);
            }

            this.TopicInfoDic.TryRemove(info.RequestID, out _);

            return info.ResponseData;
        }

        // ===============================================================================================
        // Protected Function

        /// <summary>
        /// 当接收消息时触发
        /// </summary>
        protected override async Task OnReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                Dictionary<string, string> userProperties = e.ApplicationMessage.UserProperties?.ToDictionary(p => p.Name, p => p.Value) ?? new();
                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_MESSAGE_TYPE, out string? messageType);
                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_REQUEST_ID, out string? requestId);
                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_ROUTE, out string? route);
                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_SEND_CLIENT_ID, out string? sendClientId);
                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_TARGET_CLIENT_ID, out string? targetClientId);

                string topic = e.ApplicationMessage.Topic;
                string key = $"[{topic}]{route}";
                byte[]? buffer = e.ApplicationMessage.PayloadSegment.ToArray();

                // 触发接收到消息
                this.Received?.Invoke(this, new(topic, userProperties, buffer));

                // 没有消息类型不处理
                if (string.IsNullOrWhiteSpace(topic) || string.IsNullOrWhiteSpace(messageType))
                    return;

                // 如果指定目标不是自己则不处理
                if (!string.IsNullOrWhiteSpace(targetClientId) && !string.Equals(this.Option.ClientID, targetClientId))
                    return;

                // 消息数据
                if (string.Equals(DanceMqttMessageType.MESSAGE, messageType))
                {
                    this.TopicActionDic.TryGetValue(key, out DanceMqttTopicActionBase? action);
                    _ = action?.Execute(e.ApplicationMessage.PayloadSegment.ToArray());

                    return;
                }

                // 处理请求数据
                if (string.Equals(DanceMqttMessageType.REQUEST, messageType) && !string.IsNullOrWhiteSpace(requestId))
                {
                    this.TopicActionDic.TryGetValue(key, out DanceMqttTopicActionBase? action);
                    byte[]? responseBuffer = action?.Execute(buffer) ?? Array.Empty<byte>();

                    await this.PublishAsync(topic, route ?? string.Empty, sendClientId ?? string.Empty, DanceMqttMessageType.RESPONSE, requestId, responseBuffer);

                    return;
                }

                // 处理返回数据
                if (string.Equals(DanceMqttMessageType.RESPONSE, messageType) && !string.IsNullOrWhiteSpace(requestId))
                {
                    this.TopicInfoDic.TryGetValue(requestId, out DanceMqttTopicInfo? info);
                    if (info == null)
                        return;

                    info.ResponseData = e.ApplicationMessage.PayloadSegment.ToArray();
                    info.ResponseTime = DateTime.Now;
                    info.IsResponse = true;

                    return;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="requestId">请求ID</param>
        /// <param name="buffer">二进制数据</param>
        private async Task PublishAsync(string topic, string route, string targetClientId, string messageType, string requestId, byte[] buffer)
        {
            if (this.MqttClient == null)
                return;

            Dictionary<string, string> userProperties = new()
            {
                [DanceMqttPropertyKeys.DANCE_MESSAGE_TYPE] = messageType,
                [DanceMqttPropertyKeys.DANCE_ROUTE] = route,
                [DanceMqttPropertyKeys.DANCE_SEND_CLIENT_ID] = this.Option.ClientID,
                [DanceMqttPropertyKeys.DANCE_TARGET_CLIENT_ID] = targetClientId,
                [DanceMqttPropertyKeys.DANCE_REQUEST_ID] = requestId,
            };

            MqttApplicationMessage msg = new()
            {
                Topic = topic,
                PayloadSegment = buffer,
                UserProperties = new()
            };

            foreach (var kv in userProperties)
            {
                msg.UserProperties.Add(new(kv.Key, kv.Value));
            }

            await this.MqttClient.PublishAsync(msg);

            this.Sended?.Invoke(this, new(topic, userProperties, buffer));
        }
    }
}