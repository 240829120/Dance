using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件信息
    /// </summary>
    public interface IDancePluginInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        string ID { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
    }
}
