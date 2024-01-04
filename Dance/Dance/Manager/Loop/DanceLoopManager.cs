using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Dance
{
    /// <summary>
    /// 循环管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceLoopManager))]
    public class DanceLoopManager : DanceObject, IDanceLoopManager
    {
        /// <summary>
        /// 循环信息池
        /// </summary>
        private readonly Dictionary<string, DanceLoopInfo> pool = [];

        /// <summary>
        /// 循环线程
        /// </summary>
        private DanceThread? thread;

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="intervalSec">间隔（单位：秒）</param>
        /// <param name="action">行为</param>
        public void Register(string key, double intervalSec, Action action)
        {
            lock (this.pool)
            {
                this.pool[key] = new()
                {
                    Key = key,
                    WaitTime = TimeSpan.FromSeconds(intervalSec),
                    BeginTime = DateTime.Now,
                    Action = action,
                    LastTriggerTime = DateTime.Now
                };
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="key">键</param>
        public void UnRegister(string key)
        {
            lock (this.pool)
            {
                this.pool.Remove(key);
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this.Stop();

            this.thread = new(this.Execute);
            this.thread.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this.thread?.Stop();
            this.thread = null;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Stop();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context">上下文</param>
        private void Execute(DanceThreadContext context)
        {
            while (!context.IsCancel)
            {
                lock (this.pool)
                {
                    DateTime now = DateTime.Now;

                    foreach (DanceLoopInfo item in this.pool.Values)
                    {
                        if (item == null)
                            continue;

                        if (now >= item.LastTriggerTime + item.WaitTime)
                        {
                            try
                            {
                                item.Action?.Invoke();
                                item.LastTriggerTime = now;
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex);
                            }
                        }
                    }
                }

                Thread.Sleep(200);
            }
        }
    }
}
