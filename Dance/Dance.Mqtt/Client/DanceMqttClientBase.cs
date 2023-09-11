using log4net;
using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt客户端基类
    /// </summary>
    public abstract class DanceMqttClientBase : DanceObject, IDanceMqttClient
    {
        /// <summary>
        /// Mqtt客户端基类
        /// </summary>
        /// <param name="option">设置</param>
        public DanceMqttClientBase(DanceMqttClientOption option)
        {
            this.Option = option;
            this.MqttFactory = new(new DanceMqttLog(this));
        }

        // ===============================================================================================
        // Field

        /// <summary>
        /// MQTT客户端
        /// </summary>
        protected IMqttClient? MqttClient;

        /// <summary>
        /// Mqtt工厂
        /// </summary>
        protected MqttFactory MqttFactory;

        // ===============================================================================================
        // Event

        /// <summary>
        /// 连接断开
        /// </summary>
        public event EventHandler<EventArgs>? Disconnected;

        /// <summary>
        /// 开始连接
        /// </summary>
        public event EventHandler<EventArgs>? Connecting;

        /// <summary>
        /// 连接结束
        /// </summary>
        public event EventHandler<EventArgs>? Connected;

        // ===============================================================================================
        // Property

        /// <summary>
        /// 参数
        /// </summary>
        public DanceMqttClientOption Option { get; private set; }

        // ===============================================================================================
        // Public Function

        /// <summary>
        /// 链接服务
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync()
        {
#if DEBUG
            log.Debug($"============================================");
            log.Debug($"MQTT 开始链接");
            log.Debug($"Client ID: {this.Option.ClientID}");
            log.Debug($"User Name: {this.Option.UserName}");
            log.Debug($"Password:  {this.Option.Password}");
            log.Debug($"============================================");
#endif

            this.MqttClient = this.MqttFactory.CreateMqttClient();
            var option = new MqttClientOptionsBuilder().WithTcpServer(this.Option.Url, this.Option.Port)
                                                       .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                                                       .WithClientId(this.Option.ClientID)
                                                       .WithCredentials(this.Option.UserName, this.Option.Password)
                                                       .WithWillQualityOfServiceLevel(this.Option.Level)
                                                       .WithKeepAlivePeriod(this.Option.KeepAlive)
                                                       .WithTimeout(this.Option.Timeout)
                                                       .Build();

            this.MqttClient.ApplicationMessageReceivedAsync += OnReceivedAsync;
            this.MqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            this.MqttClient.ConnectingAsync += MqttClient_ConnectingAsync;
            this.MqttClient.ConnectedAsync += MqttClient_ConnectedAsync;

            await this.MqttClient.ConnectAsync(option, CancellationToken.None);
        }

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="topics">主题</param>
        public async Task SubscribeAsync(params string[] topics)
        {
            if (this.MqttClient == null)
                throw new Exception("mqtt client is null.");

            if (!this.MqttClient.IsConnected)
                throw new Exception("mqtt client is not connected.");

            var builder = this.MqttFactory.CreateSubscribeOptionsBuilder();
            foreach (string topic in topics)
            {
                if (string.IsNullOrWhiteSpace(topic))
                    continue;

                builder = builder.WithTopicFilter(topic);
            }

            var option = builder.Build();

            await this.MqttClient.SubscribeAsync(option, CancellationToken.None);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="topics">主题</param>
        public async Task UnsubscribeAsync(params string[] topics)
        {
            if (this.MqttClient == null)
                throw new Exception("mqtt client is null.");

            if (!this.MqttClient.IsConnected)
                throw new Exception("mqtt client is not connected.");

            var builder = this.MqttFactory.CreateUnsubscribeOptionsBuilder();

            foreach (string topic in topics)
            {
                if (string.IsNullOrWhiteSpace(topic))
                    continue;

                builder = builder.WithTopicFilter(topic);
            }

            var option = builder.Build();

            await this.MqttClient.UnsubscribeAsync(option, CancellationToken.None);
        }

        /// <summary>
        /// 是否链接
        /// </summary>
        /// <returns>是否链接</returns>
        public bool IsConnected()
        {
            return this.MqttClient != null && this.MqttClient.IsConnected;
        }

        // ===============================================================================================
        // Protected Function

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            if (this.MqttClient == null)
                return;

            IMqttClient client = this.MqttClient;
            this.MqttClient = null;

            client.DisconnectAsync().Wait();
            client.Dispose();
        }

        /// <summary>
        /// 当接收消息时触发
        /// </summary>
        protected abstract Task OnReceivedAsync(MqttApplicationMessageReceivedEventArgs e);

        // ===============================================================================================
        // Private Function

        /// <summary>
        /// 链接断开
        /// </summary>
        private Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            // 触发链接断开事件
            this.Disconnected?.Invoke(this, new EventArgs());

            return Task.CompletedTask;
        }

        /// <summary>
        /// 链接完成
        /// </summary>
        private Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            this.Connected?.Invoke(this, new EventArgs());

            return Task.CompletedTask;
        }

        /// <summary>
        /// 开始链接
        /// </summary>
        private Task MqttClient_ConnectingAsync(MqttClientConnectingEventArgs arg)
        {
            this.Connecting?.Invoke(this, new EventArgs());

            return Task.CompletedTask;
        }
    }
}
