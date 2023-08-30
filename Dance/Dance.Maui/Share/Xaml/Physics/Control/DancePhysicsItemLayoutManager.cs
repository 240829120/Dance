using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 物理引擎面板布局管理器
    /// </summary>
    public class DancePhysicsItemLayoutManager : ILayoutManager
    {
        /// <summary>
        /// 物理引擎面板布局管理器
        /// </summary>
        /// <param name="owner">所属</param>
        public DancePhysicsItemLayoutManager(DancePhysicsItemsControl owner)
        {
            this.Owner = owner;
        }

        /// <summary>
        /// 所属
        /// </summary>
        internal DancePhysicsItemsControl Owner;

        /// <summary>
        /// 更新时间
        /// </summary>
        internal TimeSpan UpdatingTime;

        /// <summary>
        /// 渲染时间
        /// </summary>
        private TimeSpan RenderingTime;

        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="bounds">可用区域</param>
        /// <returns>使用区域</returns>
        public Size ArrangeChildren(Rect bounds)
        {
            this.Owner.World.Step((this.UpdatingTime - this.RenderingTime) * this.Owner.StepSpeed);
            this.RenderingTime = this.UpdatingTime;

            foreach (IView view in this.Owner.Children)
            {
                if (view == null)
                    continue;

                if (view is DancePhysicsItemJointGraphics graphicsView)
                {
                    view.Arrange(bounds);
                    graphicsView.Invalidate();
                    continue;
                }

                if (view is not DancePhysicsItem visual || visual.Body == null || visual.Body.Body == null)
                {
                    view.Arrange(bounds);
                    continue;
                }

                visual.AnchorX = 0.5;
                visual.AnchorY = 0.5;

                Rect rect = new(visual.Body.Body.Position.X - visual.DesiredSize.Width / 2f, visual.Body.Body.Position.Y - visual.DesiredSize.Height / 2f, visual.DesiredSize.Width, visual.DesiredSize.Height);

                visual.Rotation = visual.Body.Body.Rotation * 180f / MathF.PI;
                view.Arrange(rect);
            }

            return bounds.Size;
        }

        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="widthConstraint">可用宽度</param>
        /// <param name="heightConstraint">可用高度</param>
        /// <returns>使用区域</returns>
        public Size Measure(double widthConstraint, double heightConstraint)
        {
            foreach (IView item in this.Owner.Children)
            {
                item.Measure(widthConstraint, heightConstraint);
            }

            return new Size(widthConstraint, heightConstraint);
        }
    }
}
