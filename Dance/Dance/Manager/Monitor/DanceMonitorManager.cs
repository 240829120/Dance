using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 监视管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceMonitorManager))]
    public class DanceMonitorManager : DanceObject, IDanceMonitorManager
    {
        /// <summary>
        /// 更新线程
        /// </summary>
        private DanceThread? UpdateThread;

        /// <summary>
        /// 监视执行器集合
        /// </summary>
        public List<IDanceMonitorExecuter> Executers { get; } = new();

        /// <summary>
        /// 更新间隔
        /// </summary>
        public TimeSpan UpdateInterval { get; set; } = TimeSpan.FromSeconds(2);

        /// <summary>
        /// 监视信息
        /// </summary>
        public IDanceMonitorInfo? MonitorInfo { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            foreach (IDanceMonitorExecuter executer in this.Executers)
            {
                executer.Initialize();
            }
        }

        /// <summary>
        /// 开始监视
        /// </summary>
        public void Start()
        {
            if (this.UpdateThread != null)
                return;

            this.UpdateThread = new(this.Update);
            this.UpdateThread.Start();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.UpdateThread?.Stop();

            foreach (IDanceMonitorExecuter executer in this.Executers)
            {
                executer.Dispose();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="context"></param>
        private void Update(DanceThreadContext context)
        {
            while (!context.IsCancel)
            {
                foreach (IDanceMonitorExecuter executer in this.Executers)
                {
                    try
                    {
                        executer.Update(this.MonitorInfo);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }

                Thread.Sleep(2000);
            }
        }
    }
}
