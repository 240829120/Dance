using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 监视信息
    /// </summary>
    public class DanceMonitorInfo : IDanceMonitorInfo, IDanceNetworkMonitorInfo
    {
        /// <summary>
        /// 网络是否连接
        /// </summary>
        public bool IsConnected { get; set; }
    }
}
