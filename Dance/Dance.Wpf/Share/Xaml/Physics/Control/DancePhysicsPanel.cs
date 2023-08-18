using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 物理引擎面板
    /// </summary>
    public class DancePhysicsPanel : Panel
    {
        /// <summary>
        /// 物理布局
        /// </summary>
        private DancePhysicsLayout? Layout;

        /// <summary>
        /// 布局子项
        /// </summary>
        /// <param name="finalSize">可用区域</param>
        /// <returns>布局结果</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            DancePhysicsLayout? layout = this.GetPhysicsLayout();

            if (layout == null)
                return finalSize;

            layout.World.Step(TimeSpan.FromMilliseconds(32));

            foreach (var item in this.Children)
            {
                if (item == null || item is not FrameworkElement visual)
                    continue;

                TransformGroup group = GetTransformGroup(visual);

                if (DancePhysicsLayout.GetBody(visual) is DanceBody bodyElement && bodyElement.Body != null)
                {
                    Rect rect = new(bodyElement.Body.Position.X - visual.DesiredSize.Width / 2f, bodyElement.Body.Position.Y - visual.DesiredSize.Height / 2f, visual.DesiredSize.Width, visual.DesiredSize.Height);

                    if (group.Children[2] is RotateTransform rotateTransform)
                    {
                        rotateTransform.Angle = bodyElement.Body.Rotation * 180f / MathF.PI;
                    }

                    visual.Arrange(rect);
                }
                else if (visual is DancePhysicsJointGraphicsDrawable graphicsView)
                {
                    visual.Arrange(new Rect(finalSize));
                    graphicsView.InvalidateVisual();
                }
                else
                {
                    visual.Arrange(new Rect(finalSize));
                }
            }

            return finalSize;
        }

        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="availableSize">可用区域</param>
        /// <returns>测量结果</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement item in this.Children)
            {
                item.Measure(availableSize);
            }

            return availableSize;
        }

        /// <summary>
        /// 获取物理布局
        /// </summary>
        /// <returns>物理布局</returns>
        private DancePhysicsLayout? GetPhysicsLayout()
        {
            this.Layout ??= XamlExpansion.GetVisualTreeParent<DancePhysicsLayout>(this);

            return this.Layout;
        }

        /// <summary>
        /// 获取布局分组
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>布局分组</returns>
        private static TransformGroup GetTransformGroup(FrameworkElement element)
        {
            if (element.RenderTransform is TransformGroup group)
                return group;

            group = new();
            group.Children.Add(new ScaleTransform());
            group.Children.Add(new SkewTransform());
            group.Children.Add(new RotateTransform());
            group.Children.Add(new TranslateTransform());

            element.RenderTransform = group;

            return group;
        }
    }
}
