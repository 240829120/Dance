using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件领域
    /// </summary>
    /// <param name="lifescope">插件生命周期</param>
    /// <param name="pluginInfo">插件信息</param>
    public class DancePluginDomain(IDancePluginLifescope lifescope, IDancePluginInfo pluginInfo) : DanceDomainBase<DancePluginDomain>
    {
        /// <summary>
        /// 插件信息
        /// </summary>
        public IDancePluginInfo PluginInfo { get; private set; } = pluginInfo;

        /// <summary>
        /// 插件生命周期
        /// </summary>
        public IDancePluginLifescope Lifescope { get; private set; } = lifescope;

        /// <summary>
        /// 插件是否初始化完成
        /// </summary>
        public bool IsInitialized { get; internal set; }
    }
}
