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
    public interface IDanceBlockingManager : IDisposable
    {
        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="key">键</param>
        void Wait(string key);

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="key">键</param>
        void Release(string key);
    }
}
