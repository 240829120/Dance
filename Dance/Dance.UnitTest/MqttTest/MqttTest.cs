using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dance;
using Dance.Common;

namespace Dance.UnitTest
{
    /// <summary>
    /// Mqtt测试
    /// </summary>
    [TestClass]
    public class MqttTest
    {
        public MqttTest()
        {
            DanceMqttClientOption option1 = new("test1")
            {
                Url = "127.0.0.1",
                UserName = "admin",
                Password = "public",
                Port = 1883
            };

            DanceMqttClientOption option2 = new("test2")
            {
                Url = "127.0.0.1",
                UserName = "admin",
                Password = "public",
                Port = 1883
            };

            DanceMqttClientOption option3 = new("test3")
            {
                Url = "127.0.0.1",
                UserName = "admin",
                Password = "public",
                Port = 1883
            };

            this.Client1 = new DanceMqttClient(option1);
            this.Client2 = new DanceMqttClient(option2);
            this.Client3 = new DanceMqttClient(option3);
        }

        [TestInitialize]
        public void Initialize()
        {
            this.Client1.ConnectAsync().Wait();
            this.Client2.ConnectAsync().Wait();
            this.Client3.ConnectAsync().Wait();

            this.Client1.SubscribeAsync("test_topic").Wait();
            this.Client2.SubscribeAsync("test_topic").Wait();
            this.Client3.SubscribeAsync("test_topic").Wait();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.Client1?.Dispose();
            this.Client2?.Dispose();
            this.Client3?.Dispose();
        }

        private readonly DanceMqttClient Client1;

        private readonly DanceMqttClient Client2;

        private readonly DanceMqttClient Client3;

        [TestMethod]
        public void RequestTest1()
        {
            this.Client1.AddTopicFunc("test_topic", "index/RequestTest1", m => $"response: {m}");

            string response = this.Client2.RequestStringAsync("test_topic", "index/RequestTest1", "test1", "123").Result;
            Debug.WriteLine($"RequestTest1 __ response: {response}");

            Assert.AreEqual("response: 123", response);
        }

        [TestMethod]
        public void PublishTest1()
        {
            string msg1 = string.Empty;
            string msg2 = string.Empty;
            string msg3 = string.Empty;

            this.Client1.AddTopicAction("test_topic", m => msg1 = m);
            this.Client2.AddTopicAction("test_topic", m => msg2 = m);
            this.Client3.AddTopicAction("test_topic", m => msg3 = m);

            this.Client1.PublishStringAsync("test_topic", "this is a try.").Wait();

            Task.Delay(3000).Wait();

            Assert.AreEqual(msg1, "this is a try.");
            Assert.AreEqual(msg2, "this is a try.");
            Assert.AreEqual(msg3, "this is a try.");
        }

        [TestMethod]
        public void PublishTest2()
        {
            string msg1 = string.Empty;
            string msg2 = string.Empty;
            string msg3 = string.Empty;

            this.Client1.AddTopicAction("test_topic", m => msg1 = m);
            this.Client2.AddTopicAction("test_topic", m => msg2 = m);
            this.Client3.AddTopicAction("test_topic", m => msg3 = m);

            this.Client1.PublishStringAsync("test_topic", string.Empty, this.Client2.Option.ClientID, "this is a try.").Wait();

            Task.Delay(3000).Wait();

            Assert.AreEqual(msg1, string.Empty);
            Assert.AreEqual(msg2, "this is a try.");
            Assert.AreEqual(msg3, string.Empty);
        }

        [TestMethod]
        public void PublishTest3()
        {
            string msg1 = string.Empty;
            string msg2 = string.Empty;
            string msg2_route = string.Empty;
            string msg3 = string.Empty;

            this.Client1.AddTopicAction("test_topic", m => msg1 = m);
            this.Client2.AddTopicAction("test_topic", m => msg2 = m);
            this.Client2.AddTopicAction("test_topic", "index/PublishTest3", m => msg2_route = m);
            this.Client3.AddTopicAction("test_topic", m => msg3 = m);

            this.Client1.PublishStringAsync("test_topic", "index/PublishTest3", this.Client2.Option.ClientID, "this is a try.").Wait();

            Task.Delay(3000).Wait();

            Assert.AreEqual(msg1, string.Empty);
            Assert.AreEqual(msg2, string.Empty);
            Assert.AreEqual(msg2_route, "this is a try.");
            Assert.AreEqual(msg3, string.Empty);
        }
    }
}
