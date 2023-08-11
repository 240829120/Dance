using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 延时管理器
    /// </summary>
    public interface IDanceDelayManager : IDisposable
    {
        /// <summary>
        /// 启动
        /// </summary>
        void Start();

        /// <summary>
        /// 延时执行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="seconds">延时秒数</param>
        /// <param name="action">行为</param>
        void Wait(string key, double seconds, Action action);

        /// <summary>
        /// 延时执行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="duration">延时</param>
        /// <param name="action">行为</param>
        void Wait(string key, TimeSpan duration, Action action);

        /// <summary>
        /// 释放延时执行
        /// </summary>
        /// <param name="key">键</param>
        void Release(string key);
    }
}
