using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    public interface IDancePluginManager : IDisposable
    {
        /// <summary>
        /// 插件领域集合
        /// </summary>
        IReadOnlyList<DancePluginDomain> PluginDomains { get; }

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="assemblyPrefix">程序集前缀</param>
        void LoadPlugin(string assemblyPrefix);

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="assemblys">程序集</param>
        void LoadPlugin(params Assembly[] assemblys);

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="lifescopes">插件生命周期</param>
        void LoadPlugin(params IDancePluginLifescope[] lifescopes);

        /// <summary>
        /// 初始化插件
        /// </summary>
        /// <param name="pluginIds">插件ID集合</param>
        void InitializePlugin(params string[] pluginIds);
    }
}
