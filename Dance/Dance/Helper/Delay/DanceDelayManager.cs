using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 延时管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceDelayManager))]
    public class DanceDelayManager : DanceObject, IDanceDelayManager
    {
        /// <summary>
        /// 缓存池
        /// </summary>
        private readonly Dictionary<string, DanceDelayInfo> pool = new(100);

        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object lock_object = new();

        /// <summary>
        /// 延时线程
        /// </summary>
        private DanceThread? safeThread;

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            this.safeThread = new DanceThread(this.Execute);
            this.safeThread.Start();
        }

        /// <summary>
        /// 延时执行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="delaySeconds">延时秒数</param>
        /// <param name="action">行为</param>
        public void Wait(string key, double delaySeconds, Action action)
        {
            this.Wait(key, TimeSpan.FromSeconds(delaySeconds), action);
        }

        /// <summary>
        /// 延时执行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="delayTime">延时时间</param>
        /// <param name="action">行为</param>
        public void Wait(string key, TimeSpan delayTime, Action action)
        {
            lock (this.lock_object)
            {
                if (this.pool.TryGetValue(key, out DanceDelayInfo? info))
                {
                    info.Action = action;
                    return;
                }

                info = new()
                {
                    Key = key,
                    Action = action,
                    BeginTime = DateTime.Now,
                    DelayTime = delayTime
                };
                info.ExecuteTime = info.BeginTime + info.DelayTime;

                this.pool.Add(key, info);
            }
        }

        /// <summary>
        /// 释放延时执行
        /// </summary>
        /// <param name="key">键</param>
        public void Release(string key)
        {
            lock (this.lock_object)
            {
                if (!this.pool.TryGetValue(key, out DanceDelayInfo? info))
                    return;

                this.pool.Remove(key);

                info.Dispose();
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.safeThread?.Dispose();
            this.safeThread = null;

            lock (this.lock_object)
            {
                foreach (DanceDelayInfo info in this.pool.Values)
                {
                    info.Dispose();
                }

                this.pool.Clear();
            }
        }

        /// <summary>
        /// 执行延时任务
        /// </summary>
        /// <param name="context">安全线程上下文</param>
        [DebuggerNonUserCode]
        private void Execute(DanceThreadContext context)
        {
            while (!context.IsCancel)
            {
                lock (this.lock_object)
                {
                    DateTime now = DateTime.Now;
                    List<DanceDelayInfo> removeList = new();
                    foreach (DanceDelayInfo info in this.pool.Values)
                    {
                        if (info.ExecuteTime > now)
                            continue;

                        removeList.Add(info);

                        Task.Run(() =>
                        {
                            try
                            {
                                info.Action?.Invoke();
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex);
                            }
                        });
                    }

                    foreach (DanceDelayInfo info in removeList)
                    {
                        if (string.IsNullOrWhiteSpace(info.Key))
                            continue;

                        this.pool.Remove(info.Key);
                    }
                }

                Thread.Sleep(200);
            }
        }
    }
}
