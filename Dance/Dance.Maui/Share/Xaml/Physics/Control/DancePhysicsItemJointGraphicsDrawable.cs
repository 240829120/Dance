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
    public class DancePhysicsItemJointGraphicsDrawable : IDrawable
    {
        /// <summary>
        /// 物理关节绘制
        /// </summary>
        /// <param name="graphics">绘制控件</param>
        public DancePhysicsItemJointGraphicsDrawable(DancePhysicsItemJointGraphics graphics)
        {
            this.Graphics = graphics;
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        internal DancePhysicsItemJointGraphics Graphics;

        /// <summary>
        /// 所属
        /// </summary>
        internal DancePhysicsItemsControl? Owner;

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="canvas">画布</param>
        /// <param name="dirtyRect">区域</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<DancePhysicsItemsControl>(this.Graphics);

            if (this.Owner == null)
                return;

            foreach (Joint joint in this.Owner.World.JointList)
            {
                if (joint.Tag is DanceJoint element)
                {
                    element.Draw(this.Owner.World, joint, canvas, new RectF(0, 0, (float)this.Owner.DesiredSize.Width, (float)this.Owner.DesiredSize.Height));
                }
            }
        }
    }
}
