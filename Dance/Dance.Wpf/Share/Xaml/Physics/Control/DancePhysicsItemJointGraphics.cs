using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using tainicom.Aether.Physics2D.Dynamics.Joints;

namespace Dance.Wpf
{
    /// <summary>
    /// 物理关节绘制
    /// </summary>
    public class DancePhysicsItemJointGraphics : DancePhysicsItem
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.Owner == null)
                return;

            foreach (Joint joint in this.Owner.World.JointList)
            {
                if (joint.Tag is DanceJoint element)
                {
                    element.Draw(this.Owner.World, joint, drawingContext, new System.Windows.Rect(0, 0, this.ActualWidth, this.ActualHeight));
                }
            }
        }
    }
}
