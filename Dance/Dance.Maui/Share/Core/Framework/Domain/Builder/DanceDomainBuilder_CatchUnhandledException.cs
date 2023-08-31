using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using log4net;

namespace Dance.Maui
{
    /// <summary>
    /// 领域构建器 -- 捕获未处理异常
    /// </summary>
    public class DanceDomainBuilder_CatchUnhandledException : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "捕获未处理异常";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {

        }

        /// <summary>
        /// 应用程序未处理异常
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.ExceptionObject);
            log.Error(e.ExceptionObject);
        }
    }
}
