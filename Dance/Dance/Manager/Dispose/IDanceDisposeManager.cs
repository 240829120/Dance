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
    public interface IDanceDisposeManager : IDisposable
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="disposable">可释放对象</param>
        void AddObject(IDisposable disposable);
    }
}
