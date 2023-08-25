using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nkast.Aether.Physics2D.Dynamics.Joints;

namespace Dance.Maui
{
    /// <summary>
    /// 物理关节绘制
    /// </summary>
    public class DancePhysicsItemJointGraphics : GraphicsView
    {
        public DancePhysicsItemJointGraphics()
        {
            this.Drawable = new DancePhysicsItemJointGraphicsDrawable(this);
        }
    }
}
