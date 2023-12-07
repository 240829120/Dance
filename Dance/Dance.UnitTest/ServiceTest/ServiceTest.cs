using Dance.Art;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.UnitTest
{
    [DanceServiceRoute]
    public class Test1Service
    {
        [DanceServiceRoute]
        public int Add(int a, int b)
        {
            return a + b;
        }

    }

    [DanceServiceRoute]
    public class Test2Service
    {
        [DanceServiceRoute("add/add1/add2/add3")]
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    [TestClass]
    public class ServiceTest
    {


        [TestMethod]
        public void SingleTest()
        {
            DanceServiceManager serviceManager = new();

            serviceManager.AddService(this.GetType().Assembly);

            var r1 = serviceManager.Invoke("Test1/Add", new object[] { 1, 2 });
            var r2 = serviceManager.Invoke("Test2/add/add1/add2/add3", new object[] { 1, 2 });

            var r3 = serviceManager.Invoke("Test2/add/add1/add2/add3", new string[] { "1", "2" });
        }
    }
}
