using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 阻塞管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceBlockingManager))]
    public class DanceBlockingManager : DanceObject, IDanceBlockingManager
    {
        /// <summary>
        /// 阻塞信息字典
        /// </summary>
        private readonly Dictionary<string, DanceBlockingInfo> BlockingInfoDic = [];

        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="key">键</param>
        public void Wait(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            DanceBlockingInfo? info = null;
            lock (this.BlockingInfoDic)
            {
                if (!this.BlockingInfoDic.TryGetValue(key, out info))
                {
                    info = new DanceBlockingInfo(key);

                    this.BlockingInfoDic[key] = info;
                }
            }

            while (!info.IsReleased)
            {
                Task.Delay(500).Wait();
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="key">键</param>
        public void Release(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            this.BlockingInfoDic.TryGetValue(key, out DanceBlockingInfo? info);
            if (info == null)
                return;

            info.IsReleased = true;

            lock (this.BlockingInfoDic)
            {
                this.BlockingInfoDic.Remove(key);
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            lock (this.BlockingInfoDic)
            {
                foreach (var kv in this.BlockingInfoDic)
                {
                    kv.Value.IsReleased = true;
                }

                this.BlockingInfoDic.Clear();
            }
        }
    }
}
