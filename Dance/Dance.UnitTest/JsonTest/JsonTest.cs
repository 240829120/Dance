using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dance.UnitTest
{
    public abstract class StudentBase : IDanceJsonObject
    {
        public string PART_DanceObjectType => this.GetType().AssemblyQualifiedName ?? string.Empty;

        public string? ID { get; set; }
    }

    public class BoyStudent : StudentBase
    {
        public BoyStudent() { }

        public string? BoyName { get; set; }
    }

    public class GirlStudent : StudentBase
    {
        public GirlStudent() { }

        public string? GirlName { get; set; }
    }

    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void SingletonTest()
        {
            List<StudentBase> list = new()
            {
                new BoyStudent()
                {
                    ID = "1",
                    BoyName = "Boy"
                },
                new GirlStudent()
                {
                    ID = "2",
                    GirlName = "Girl"
                }
            };

            string json = JsonConvert.SerializeObject(list);

            var dest = JsonConvert.DeserializeObject<List<StudentBase>>(json, new DanceJsonObjectConverter());
        }

        [TestMethod]
        public void HttpTest()
        {
            string url = "http://127.0.0.1:8082/output/WriteLine";
            object data = new
            {
                msg = "你好"
            };
            var a = DanceHttpHelper.Post(url, data, null, null).Result;
        }
    }
}
