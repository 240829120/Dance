using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Common
{
    /// <summary>
    /// Mqtt客户端扩展
    /// </summary>
    public static class DanceMqttClientExpansion
    {
        // ===================================================================================================
        // AddTopicAction -- 添加主题

        /// <summary>
        /// 添加主题行为
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="action">行为</param>
        public static void AddTopicAction(this DanceMqttClient client, string topic, Action<string> action)
        {
            client.AddTopicAction(new DanceMqttTopicAction(topic, string.Empty, action));
        }

        /// <summary>
        /// 添加主题行为
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="action">行为</param>
        public static void AddTopicAction(this DanceMqttClient client, string topic, string route, Action<string> action)
        {
            client.AddTopicAction(new DanceMqttTopicAction(topic, route, action));
        }

        /// <summary>
        /// 添加主题行为
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="func">行为</param>
        public static void AddTopicFunc(this DanceMqttClient client, string topic, Func<string, string> func)
        {
            client.AddTopicAction(new DanceMqttTopicAction(topic, string.Empty, func));
        }

        /// <summary>
        /// 添加主题行为
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="func">行为</param>
        public static void AddTopicFunc(this DanceMqttClient client, string topic, string route, Func<string, string> func)
        {
            client.AddTopicAction(new DanceMqttTopicAction(topic, route, func));
        }

        // ===================================================================================================
        // 字符串

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        public static async Task PublishStringAsync(this DanceMqttClient client, string topic, string route, string targetClientId, string data, Encoding encoding)
        {
            await client.PublishAsync(topic, route, targetClientId, DanceMqttMessageType.MESSAGE, encoding.GetBytes(data));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        public static async Task PublishStringAsync(this DanceMqttClient client, string topic, string route, string targetClientId, string data)
        {
            await client.PublishAsync(topic, route, targetClientId, DanceMqttMessageType.MESSAGE, Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="data">数据</param>
        public static async Task PublishStringAsync(this DanceMqttClient client, string topic, string route, string data)
        {
            await client.PublishAsync(topic, route, string.Empty, DanceMqttMessageType.MESSAGE, Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="data">数据</param>
        public static async Task PublishStringAsync(this DanceMqttClient client, string topic, string data)
        {
            await client.PublishAsync(topic, string.Empty, string.Empty, DanceMqttMessageType.MESSAGE, Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时</param>
        /// <returns>返回数据</returns>
        public static async Task<string> RequestStringAsync(this DanceMqttClient client, string topic, string route, string targetClientId, string data, Encoding encoding, TimeSpan timeout)
        {
            byte[] buffer = encoding.GetBytes(data);
            byte[]? responseBuffer = await client.RequestAsync(topic, route, targetClientId, buffer, timeout);

            if (responseBuffer == null)
                return string.Empty;

            return encoding.GetString(responseBuffer);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <returns>返回数据</returns>
        public static async Task<string> RequestStringAsync(this DanceMqttClient client, string topic, string route, string targetClientId, string data, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(data);
            byte[]? responseBuffer = await client.RequestAsync(topic, route, targetClientId, buffer, client.Option.Timeout);

            if (responseBuffer == null)
                return string.Empty;

            return encoding.GetString(responseBuffer);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <returns>返回数据</returns>
        public static async Task<string> RequestStringAsync(this DanceMqttClient client, string topic, string route, string targetClientId, string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            byte[]? responseBuffer = await client.RequestAsync(topic, route, targetClientId, buffer, client.Option.Timeout);

            if (responseBuffer == null)
                return string.Empty;

            return Encoding.UTF8.GetString(responseBuffer);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <returns>返回数据</returns>
        public static async Task<string> RequestStringAsync(this DanceMqttClient client, string topic, string targetClientId, string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            byte[]? responseBuffer = await client.RequestAsync(topic, string.Empty, targetClientId, buffer, client.Option.Timeout);

            if (responseBuffer == null)
                return string.Empty;

            return Encoding.UTF8.GetString(responseBuffer);
        }

        // ===================================================================================================
        // Json

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        public static async Task PublishJsonAsync(this DanceMqttClient client, string topic, string route, string targetClientId, object data, Encoding encoding)
        {
            await client.PublishAsync(topic, route, targetClientId, DanceMqttMessageType.MESSAGE, encoding.GetBytes(JsonConvert.SerializeObject(data)));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        public static async Task PublishJsonAsync(this DanceMqttClient client, string topic, string route, string targetClientId, object data)
        {
            await client.PublishAsync(topic, route, targetClientId, DanceMqttMessageType.MESSAGE, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="data">数据</param>
        public static async Task PublishJsonAsync(this DanceMqttClient client, string topic, string route, object data)
        {
            await client.PublishAsync(topic, route, string.Empty, DanceMqttMessageType.MESSAGE, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="data">数据</param>
        public static async Task PublishJsonAsync(this DanceMqttClient client, string topic, object data)
        {
            await client.PublishAsync(topic, string.Empty, string.Empty, DanceMqttMessageType.MESSAGE, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时</param>
        /// <returns>返回数据</returns>
        public static async Task<string> RequestJsonAsync(this DanceMqttClient client, string topic, string route, string targetClientId, object data, Encoding encoding, TimeSpan timeout)
        {
            byte[] buffer = encoding.GetBytes(JsonConvert.SerializeObject(data));
            byte[]? responseBuffer = await client.RequestAsync(topic, route, targetClientId, buffer, timeout);

            if (responseBuffer == null)
                return string.Empty;

            return encoding.GetString(responseBuffer);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <returns>返回数据</returns>
        public static async Task<string> RequestJsonAsync(this DanceMqttClient client, string topic, string route, string targetClientId, object data, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(JsonConvert.SerializeObject(data));
            byte[]? responseBuffer = await client.RequestAsync(topic, route, targetClientId, buffer, client.Option.Timeout);

            if (responseBuffer == null)
                return string.Empty;

            return encoding.GetString(responseBuffer);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <returns>返回数据</returns>
        public static async Task<string> RequestJsonAsync(this DanceMqttClient client, string topic, string route, string targetClientId, object data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            byte[]? responseBuffer = await client.RequestAsync(topic, route, targetClientId, buffer, client.Option.Timeout);

            if (responseBuffer == null)
                return string.Empty;

            return Encoding.UTF8.GetString(responseBuffer);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="topic">主题</param>
        /// <param name="route">路由</param>
        /// <param name="targetClientId">目标客户端ID</param>
        /// <param name="data">数据</param>
        /// <returns>返回数据</returns>
        public static async Task<string> RequestJsonAsync(this DanceMqttClient client, string topic, string targetClientId, object data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            byte[]? responseBuffer = await client.RequestAsync(topic, string.Empty, targetClientId, buffer, client.Option.Timeout);

            if (responseBuffer == null)
                return string.Empty;

            return Encoding.UTF8.GetString(responseBuffer);
        }
    }
}
