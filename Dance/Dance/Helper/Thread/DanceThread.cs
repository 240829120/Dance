using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using log4net;

namespace Dance
{
    /// <summary>
    /// 线程
    /// </summary>
    public class DanceThread : DanceObject
    {
        /// <summary>
        /// 安全线程
        /// </summary>
        /// <param name="action">行为</param>
        public DanceThread(Action<DanceThreadContext> action)
        {
            this.Action = action;
        }

        /// <summary>
        /// 上下文
        /// </summary>
        public DanceThreadContext Context { get; private set; } = new();

        /// <summary>
        /// 行为
        /// </summary>
        public Action<DanceThreadContext>? Action { get; private set; }

        /// <summary>
        /// 线程
        /// </summary>
        private Thread? Thread;

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this.Context.IsCancel = false;
            this.Context.Exception = null;

            this.Thread = new(this.ExecuteAction)
            {
                IsBackground = true
            };
            this.Thread.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this.Context.IsCancel = true;
            this.Thread = null;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Stop();
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        private void ExecuteAction()
        {
            try
            {
                if (this.Action == null)
                    return;

                this.Action(this.Context);
            }
            catch (Exception ex)
            {
                this.Context.Exception = ex;
                log.Error(ex);
            }
        }
    }
}
