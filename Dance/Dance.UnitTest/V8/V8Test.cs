using Dance.Common;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ClearScript.JavaScript;

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
            using (var engine = new V8ScriptEngine("test1", V8ScriptEngineFlags.EnableDynamicModuleImports | V8ScriptEngineFlags.EnableDebugging))
            {
                engine.DocumentSettings.SearchPath = string.Join(";",
                    Path.Combine(@"E:\Projects\Test\Dance\Dance\Dance.UnitTest\V8")
                );
                engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;
                //engine.AddRestrictedHostObject("host", new ExtendedHostFunctions());
                //engine.AddHostType("Student", typeof(Student));
                //engine.AddHostType("Random", typeof(Random));
                engine.SuppressExtensionMethodEnumeration = true;
                engine.AllowReflection = true;
                string command = @"
import { student } from 'v8.js'

student.Name;
";
                var obj = engine.Evaluate(new DocumentInfo { Category = ModuleCategory.Standard }, command);
                var a = engine.Script;
            }
        }
    }
}
