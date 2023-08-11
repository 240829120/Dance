using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 物理关节绘制
    /// </summary>
    public class DancePhysicsJointGraphicsDrawable : IDrawable
    {
        /// <summary>
        /// 物理布局
        /// </summary>
        public DancePhysicsLayout? Layout { get; set; }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="canvas">画布</param>
        /// <param name="dirtyRect">区域</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (this.Layout == null)
                return;

            foreach (var kv in Layout.JointDic)
            {
                kv.Value.Draw(this.Layout.World, kv.Key, canvas, dirtyRect);
            }
        }
    }
}
