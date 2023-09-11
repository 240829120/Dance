using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dance;
using Dance.Mqtt;

namespace Dance.UnitTest
{
    /// <summary>
    /// Mqtt测试
    /// </summary>
    [TestClass]
    public class MqttTest
    {
        [TestMethod]
        public void SampleTest()
        {
            DanceMqttClientOption option1 = new("test1")
            {
                Url = "127.0.0.1",
                UserName = "admin",
                Password = "public",
                Port = 1883
            };

            DanceMqttClient client1 = new DanceMqttClient(option1);
            client1.AddTopicFunc("test_topic", "index/test_topic", m => $"response: {m}");

            client1.ConnectAsync().Wait();
            client1.SubscribeAsync("test_topic").Wait();

            DanceMqttClientOption option2 = new("test2")
            {
                Url = "127.0.0.1",
                UserName = "admin",
                Password = "public",
                Port = 1883
            };

            DanceMqttClient client2 = new DanceMqttClient(option2);
            client2.ConnectAsync().Wait();
            client2.SubscribeAsync("test_topic").Wait();
            string response = client2.RequestStringAsync("test_topic", "index/test_topic", "test1", "123").Result;


            Debug.WriteLine(response);

            Task.Delay(10000).Wait();
        }
    }
}
