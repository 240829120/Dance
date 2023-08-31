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

namespace Dance.Wpf
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
            if (Application.Current != null)
            {
                Application.Current.DispatcherUnhandledException -= App_DispatcherUnhandledException;
                Application.Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
            }

            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            System.Windows.Forms.Application.ThreadException -= Application_ThreadException;
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;
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

        /// <summary>
        /// WPF应用程序未处理异常
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.Exception.Message);
            log.Error(e.Exception);
            e.Handled = true;
        }

        /// <summary>
        /// Winform应用程序未处理异常
        /// </summary>
        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Debug.WriteLine(e.Exception);
            log.Error(e.Exception);
        }
    }
}
