using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 监视执行器
    /// </summary>
    public interface IDanceMonitorExecuter : IDisposable
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info">监视信息</param>
        void Update(IDanceMonitorInfo? info);
    }
}
