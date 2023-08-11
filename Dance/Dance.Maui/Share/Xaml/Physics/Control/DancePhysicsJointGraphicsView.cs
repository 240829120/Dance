using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 物理关节绘制视图
    /// </summary>
    public class DancePhysicsJointGraphicsView : GraphicsView
    {
        public DancePhysicsJointGraphicsView()
        {
            this.Drawable = new DancePhysicsJointGraphicsDrawable();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (this.Parent is DancePhysicsLayout layout && this.Drawable is DancePhysicsJointGraphicsDrawable drawable)
            {
                drawable.Layout = layout;
            }
        }
    }
}
