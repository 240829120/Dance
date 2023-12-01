using HelixToolkit.SharpDX.Core.Model.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 节点分组扩展
    /// </summary>
    public static class DanceGroupNode3DExpansion
    {
        /// <summary>
        /// 获取所属分组
        /// </summary>
        /// <param name="node">场景节点</param>
        /// <returns>所属分组</returns>
        public static DanceGroupNode3D? GetOnwer(this SceneNode node)
        {
            if (node is DanceGroupNode3D owner)
                return owner;

            return GetOnwer(node.Parent);
        }
    }
}
