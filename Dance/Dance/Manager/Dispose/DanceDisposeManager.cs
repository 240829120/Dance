using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Manager
{
    /// <summary>
    /// 释放管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceDisposeManager))]
    public class DanceDisposeManager : DanceObject, IDanceDisposeManager
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="disposable">可释放对象</param>
        public void AddObject(IDisposable disposable)
        {
            lock (this.Caches)
            {
                this.Caches.Add(new WeakReference<IDisposable>(disposable));
            }
        }

        /// <summary>
        /// 缓存集合
        /// </summary>
        private readonly List<WeakReference<IDisposable>> Caches = [];

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            lock (this.Caches)
            {
                foreach (WeakReference<IDisposable> item in this.Caches)
                {
                    item.TryGetTarget(out IDisposable? disposable);
                    try
                    {
                        disposable?.Dispose();
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }

                this.Caches.Clear();
            }
        }
    }
}
