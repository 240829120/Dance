using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art
{
    /// <summary>
    /// 服务路由
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DanceServiceRouteAttribute : Attribute
    {
        /// <summary>
        /// 服务路由
        /// </summary>
        public DanceServiceRouteAttribute()
        {
        }

        /// <summary>
        /// 服务路由
        /// </summary>
        /// <param name="route">路由</param>
        public DanceServiceRouteAttribute(string route)
        {
            this.Route = route;
        }

        /// <summary>
        /// 路由
        /// </summary>
        public string? Route { get; private set; }
    }
}
