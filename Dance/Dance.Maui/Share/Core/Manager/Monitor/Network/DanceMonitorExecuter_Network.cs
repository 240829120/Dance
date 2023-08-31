using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 监视执行器 -- 网络
    /// </summary>
    public partial class DanceMonitorExecuter_Network : DanceObject, IDanceMonitorExecuter
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info">监视信息</param>
        public void Update(IDanceMonitorInfo? info)
        {
            if (info is not IDanceNetworkMonitorInfo monitor)
                return;

            monitor.IsConnected = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }
    }
}
