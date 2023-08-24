using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件生命周期
    /// </summary>
    public interface IDancePluginLifescope : IDisposable
    {
        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        IDancePluginInfo Register();

        /// <summary>
        /// 初始化插件
        /// </summary>
        void Initialize();
    }
}