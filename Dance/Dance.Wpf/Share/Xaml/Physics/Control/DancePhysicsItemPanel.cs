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
    /// 物理引擎布局容器
    /// </summary>
    public class DancePhysicsItemPanel : Panel
    {
        public DancePhysicsItemPanel()
        {
            this.Loaded += DancePhysicsItemPanel_Loaded;
            this.Unloaded += DancePhysicsItemPanel_Unloaded;
        }

        #region Owner -- 所属

        /// <summary>
        /// 所属
        /// </summary>
        public DancePhysicsItemsControl? Owner
        {
            get { return (DancePhysicsItemsControl?)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        /// <summary>
        /// 所属
        /// </summary>
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(DancePhysicsItemsControl), typeof(DancePhysicsItemPanel), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 更新时间
        /// </summary>
        private TimeSpan UpdatingTime;

        /// <summary>
        /// 渲染时间
        /// </summary>
        private TimeSpan RenderingTime;

        /// <summary>
        /// 子项元素改变
        /// </summary>
        /// <param name="visualAdded">添加</param>
        /// <param name="visualRemoved">移除</param>
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            if (this.Owner == null)
            {
                this.Owner = XamlExpansion.GetVisualTreeParent<DancePhysicsItemsControl>(this);
            }

            if (visualRemoved is DancePhysicsItem removed)
            {
                this.Owner?.RemovePhysicsItem(removed);
            }

            if (visualAdded is DancePhysicsItem added)
            {
                this.Owner?.AddPhysicsItem(added);
            }
        }

        /// <summary>
        /// 布局子项
        /// </summary>
        /// <param name="finalSize">可用区域</param>
        /// <returns>布局结果</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Owner == null || !this.Owner.IsRunning)
                return finalSize;

            this.Owner.World.Step((this.UpdatingTime - this.RenderingTime) * this.Owner.StepSpeed);
            this.RenderingTime = this.UpdatingTime;

            foreach (var item in this.Children)
            {
                if (item == null || item is not DancePhysicsItem visual)
                    continue;

                if (item is DancePhysicsItemJointGraphics graphicsView)
                {
                    graphicsView.Arrange(new Rect(finalSize));
                    graphicsView.InvalidateVisual();
                    continue;
                }

                RotateTransform rotateTransform = GetRotateTransform(visual);

                if (visual.Body == null || visual.Body.Body == null)
                {
                    visual.Arrange(new(finalSize));
                    continue;
                }

                Rect rect = new(visual.Body.Body.Position.X - visual.DesiredSize.Width / 2f, visual.Body.Body.Position.Y - visual.DesiredSize.Height / 2f, visual.DesiredSize.Width, visual.DesiredSize.Height);

                rotateTransform.Angle = visual.Body.Body.Rotation * 180f / MathF.PI;

                visual.Arrange(rect);
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
        /// 获取布局分组
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>布局分组</returns>
        private static RotateTransform GetRotateTransform(FrameworkElement element)
        {
            if (element.RenderTransform is RotateTransform rotate)
                return rotate;

            element.RenderTransformOrigin = new Point(0.5, 0.5);
            element.RenderTransform = rotate = new RotateTransform();

            return rotate;
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void DancePhysicsItemPanel_Loaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        /// <summary>
        /// 卸载
        /// </summary>
        private void DancePhysicsItemPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            if (!this.IsVisible)
                return;

            if (e is not RenderingEventArgs args)
                return;

            this.UpdatingTime = args.RenderingTime;

            this.InvalidateVisual();
        }
    }
}
