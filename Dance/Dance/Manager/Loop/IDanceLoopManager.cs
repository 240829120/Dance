using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 循环管理器
    /// </summary>
    public interface IDanceLoopManager : IDisposable
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="intervalSec">间隔（单位：秒）</param>
        /// <param name="action">行为</param>
        void Register(string key, double intervalSec, Action action);

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="key">键</param>
        void UnRegister(string key);

        /// <summary>
        /// 开始
        /// </summary>
        void Start();

        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
    }
}
