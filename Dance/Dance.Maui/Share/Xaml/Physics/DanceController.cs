using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D.Controllers;
using tainicom.Aether.Physics2D.Dynamics;

namespace Dance.Maui
{
    /// <summary>
    /// 控制器元素
    /// </summary>
    public abstract class DanceController : DancePhysics
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        protected object lock_object = new();

        /// <summary>
        /// 获取或创建控制器
        /// </summary>
        /// <param name="world">物理世界</param>
        /// <returns>控制器</returns>
        public abstract Controller GetOrCreateController(World world);
    }
}
