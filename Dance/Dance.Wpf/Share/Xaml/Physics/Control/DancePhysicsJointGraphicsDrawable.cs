using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 物理关节绘制
    /// </summary>
    public class DancePhysicsJointGraphicsDrawable : Control
    {
        /// <summary>
        /// 物理布局
        /// </summary>
        public DancePhysicsLayout? Layout { get; set; }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.Layout == null)
                return;

            foreach (var kv in Layout.JointDic)
            {
                kv.Value.Draw(this.Layout.World, kv.Key, drawingContext, new System.Windows.Rect(0, 0, this.ActualWidth, this.ActualHeight));
            }
        }
    }
}
