using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using tainicom.Aether.Physics2D.Dynamics;

namespace Dance.Wpf
{
    /// <summary>
    /// 物理元素
    /// </summary>
    public abstract class DancePhysics : DependencyObject
    {
        /// <summary>
        /// 所属世界
        /// </summary>
        internal World? OwnerWorld { get; set; }
    }
}
