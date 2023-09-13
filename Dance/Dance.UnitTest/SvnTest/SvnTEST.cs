using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dance.Common;
using SharpSvn;

namespace Dance.UnitTest
{
    /// <summary>
    /// Svn测试
    /// </summary>
    [TestClass]
    public class SvnTEST
    {
        [TestMethod]
        public void CheckOutTest()
        {
            DanceSvnClientOption option = new("https://10.0.96.254/svn/SVN_TEST_STORAGE/", @"e:\SVN_TEST2")
            {
                UserName = "admin",
                Password = "public"
            };

            DanceSvnClient client = new(option);
            client.Logging += (s, e) => { Debug.WriteLine(e); };
            bool r = client.CheckOut();
            client.FlushLog();

            Assert.IsTrue(r);

            client.Dispose();
        }
    }
}
