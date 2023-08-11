using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Dance.Wpf
{
    /// <summary>
    /// 领域构建器 -- 单例启动
    /// </summary>
    public class DanceDomainBuilder_SingletonProcess : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "单例启动";

        /// <summary>
        /// 进程启动事件参数等待量
        /// </summary>
        private EventWaitHandle? ProgramStarted;

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            string applicationName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            this.ProgramStarted = new EventWaitHandle(false, EventResetMode.AutoReset, applicationName, out bool createNew);
            if (!createNew)
            {
                ProgramStarted.Set();
                throw new Exception("Process is already running.");
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {

        }
    }
}
