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
    /// <param name="action">行为</param>
    public class DanceThread(Action<DanceThreadContext> action) : DanceObject
    {
        /// <summary>
        /// 上下文
        /// </summary>
        public DanceThreadContext? Context { get; private set; }

        /// <summary>
        /// 行为
        /// </summary>
        public Action<DanceThreadContext> Action { get; private set; } = action;

        /// <summary>
        /// 线程
        /// </summary>
        private Thread? Thread;

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (this.Context != null && !this.Context.IsCancel)
            {
                throw new Exception("dance thread is running.");
            }

            this.Context = new();
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
            if (this.Context != null)
            {
                this.Context.IsCancel = true;
            }

            this.Context = null;
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
                if (this.Context == null)
                    return;

                this.Action(this.Context);
            }
            catch (Exception ex)
            {
                if (this.Context != null)
                {
                    this.Context.Exception = ex;
                }

                log.Error(ex);
            }
        }
    }
}
