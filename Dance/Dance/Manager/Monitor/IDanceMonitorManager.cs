using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 监视管理器
    /// </summary>
    public interface IDanceMonitorManager : IDisposable
    {
        /// <summary>
        /// 监视信息
        /// </summary>
        IDanceMonitorInfo? MonitorInfo { get; set; }

        /// <summary>
        /// 监视执行器集合
        /// </summary>
        List<IDanceMonitorExecuter> Executers { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();

        /// <summary>
        /// 开始监视
        /// </summary>
        void Start();
    }
}
