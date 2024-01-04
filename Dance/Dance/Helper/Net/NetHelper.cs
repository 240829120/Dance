using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 网络辅助类
    /// </summary>
    public static class NetHelper
    {
        /// <summary>
        /// 获取可用的TCP端口
        /// </summary>
        /// <returns>TCP端口</returns>
        public static int GetActiveProt(int from = 10000, int to = 20000)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            List<IPEndPoint> endPoints =
            [
                .. properties.GetActiveTcpListeners(),
                .. properties.GetActiveUdpListeners(),
                .. properties.GetActiveTcpConnections().Select(p => p.LocalEndPoint),
            ];

            List<IPEndPoint> query = [.. endPoints.Where(p => p.Port >= from && p.Port <= to).OrderBy(p => p.Port)];

            int target = from;

            foreach (IPEndPoint ep in query)
            {
                if (ep.Port > target)
                    return target;

                target = ep.Port + 1;
            }

            return target;
        }
    }
}
