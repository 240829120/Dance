using Dance.Common;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.UnitTest
{
    /// <summary>
    /// V8 引擎测试
    /// </summary>
    [TestClass]
    public class V8Test
    {
        [TestMethod]
        public void RunTest()
        {
            using (var engine = new V8ScriptEngine("test1", V8ScriptEngineFlags.EnableDebugging))
            {
                engine.AddRestrictedHostObject("host", new ExtendedHostFunctions());
                engine.AddHostType("Student", typeof(Student));
                engine.AddHostType("Random", typeof(Random));
                engine.SuppressExtensionMethodEnumeration = true;
                engine.AllowReflection = true;

                string command = @"
function f1(a,b) {
    return a + b;
}

var student = host.newObj(Student);
student.Name = '张三';
";
                var obj = engine.Evaluate(command);
                string result = engine.ExecuteCommand("f1(5,6);");

            }
        }
    }
}
