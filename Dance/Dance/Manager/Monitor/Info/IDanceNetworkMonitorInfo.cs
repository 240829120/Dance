using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 网络监视
    /// </summary>
    public interface IDanceNetworkMonitorInfo : IDanceMonitorInfo
    {
        /// <summary>
        /// 网络是否连接
        /// </summary>
        bool IsConnected { get; set; }
    }
}
