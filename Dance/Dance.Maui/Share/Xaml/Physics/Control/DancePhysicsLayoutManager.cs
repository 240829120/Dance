using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D.Dynamics;

namespace Dance.Maui
{
    /// <summary>
    /// 物理布局管理器
    /// </summary>
    public class DancePhysicsLayoutManager : LayoutManager
    {
        /// <summary>
        /// 物理布局管理器
        /// </summary>
        /// <param name="layout">布局</param>
        public DancePhysicsLayoutManager(DancePhysicsLayout layout) : base(layout)
        {

        }

        /// <summary>
        /// 布局子项
        /// </summary>
        /// <param name="bounds">区域</param>
        /// <returns>最终布局所用区域</returns>
        public override Size ArrangeChildren(Rect bounds)
        {
            if (this.Layout is not DancePhysicsLayout layout)
                return bounds.Size;

            layout.World.Step(TimeSpan.FromMilliseconds(32));

            foreach (var item in this.Layout)
            {
                if (item == null || item is not VisualElement visual)
                    continue;

                if (DancePhysicsLayout.GetBody(visual) is DanceBody bodyElement && bodyElement.Body != null)
                {
                    Rect rect = new(bodyElement.Body.Position.X - visual.DesiredSize.Width / 2f, bodyElement.Body.Position.Y - visual.DesiredSize.Height / 2f, visual.DesiredSize.Width, visual.DesiredSize.Height);
                    visual.Rotation = bodyElement.Body.Rotation * 180f / MathF.PI;
                    item.Arrange(rect);
                }
                else if (visual is GraphicsView graphicsView)
                {
                    item.Arrange(bounds);
                    graphicsView.Invalidate();
                }
                else
                {
                    item.Arrange(bounds);
                }
            }

            return bounds.Size;
        }

        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="widthConstraint">宽度</param>
        /// <param name="heightConstraint">高度</param>
        /// <returns>测量结果</returns>
        public override Size Measure(double widthConstraint, double heightConstraint)
        {
            foreach (var item in this.Layout)
            {
                item.Measure(widthConstraint, heightConstraint);
            }

            return new Size(widthConstraint, heightConstraint);
        }
    }
}
