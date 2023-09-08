using System;
using System.Collections.Generic;
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
            DanceMqttClientOption option1 = new()
            {
                ClientID = "test1",
                Url = "127.0.0.1",
                UserName = "sub_client",
                Password = "$6$U+qg0/32F0g2Fh+n$fBPSkq/rfNyEQ/TkEjRgwGTTVBpvNhKSyGShovH9KHewsvJ731tD5Zx26IHhR5RYCICt0L9qBW0/KK31UkCliw==",
                Port = 1883
            };

            DanceMqttClient client1 = new DanceMqttClient(option1);
            client1.ConnectAsync().Wait();
            client1.SubscribeAsync("test_topic").Wait();

            DanceMqttClientOption option2 = new()
            {
                ClientID = "test2",
                Url = "127.0.0.1",
                UserName = "admin",
                Password = "$7$101$YcKFPlhb8KcZxy9F$6Aud4qBEODZP43ZWUJ2DYsTZFmDXns9YRzk3B+Ef+dQyw5EmVUHoKvGMFL8iuSypXQMnqA7oQmJcq5BQkfYUIA==",
                Port = 1883
            };

            DanceMqttClient client2 = new DanceMqttClient(option2);
            client2.ConnectAsync().Wait();
            client2.PublishAsync("test_topic", "123").Wait();

            Task.Delay(10000).Wait();
        }
    }
}
