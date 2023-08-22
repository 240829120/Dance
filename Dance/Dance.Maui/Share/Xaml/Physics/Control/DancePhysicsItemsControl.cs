using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Controllers;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Joints;

namespace Dance.Maui
{
    /// <summary>
    /// 物理引擎控件
    /// </summary>
    public class DancePhysicsItemsControl : Layout
    {
        public DancePhysicsItemsControl()
        {
            this.Loaded += DancePhysicsItemsControl_Loaded;
            this.Unloaded += DancePhysicsItemsControl_Unloaded;
        }

        /// <summary>
        /// 物理世界
        /// </summary>
        internal World World = new(new Vector2(0, 10));

        /// <summary>
        /// 运行动画
        /// </summary>
        private Animation? RunningAnimation;

        /// <summary>
        /// 运行动画时间监视器
        /// </summary>
        private Stopwatch? RunningAnimationStopwatch;

        /// <summary>
        /// 布局管理器
        /// </summary>
        private DancePhysicsItemLayoutManager? LayoutManager;

        #region IsRunning -- 是否正在运行

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public static readonly BindableProperty IsRunningProperty =
            BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(DancePhysicsItemsControl), true);

        #endregion

        #region StepSpeed -- 步骤速度

        /// <summary>
        /// 步骤速度
        /// </summary>
        public double StepSpeed
        {
            get => (double)GetValue(StepSpeedProperty);
            set => SetValue(StepSpeedProperty, value);
        }

        /// <summary>
        /// 步骤速度
        /// </summary>
        public static readonly BindableProperty StepSpeedProperty =
            BindableProperty.Create(nameof(StepSpeed), typeof(double), typeof(DancePhysicsItemsControl), 1d);

        #endregion

        #region Gravity -- 重力

        /// <summary>
        /// 步骤速度
        /// </summary>
        public DanceVector Gravity
        {
            get => (DanceVector)GetValue(GravityProperty);
            set => SetValue(GravityProperty, value);
        }

        /// <summary>
        /// 步骤速度
        /// </summary>
        public static readonly BindableProperty GravityProperty =
            BindableProperty.Create(nameof(Gravity), typeof(DanceVector), typeof(DancePhysicsItemsControl), new DanceVector(0, 10), propertyChanged: (b, o, n) =>
            {
                if (b is not DancePhysicsItemsControl itemsControl)
                    return;

                if (n is not DanceVector vector)
                    return;

                itemsControl.World.Gravity = new Vector2(vector.X, vector.Y);
            });

        #endregion

        /// <summary>
        /// 添加物理项
        /// </summary>
        /// <param name="item">项</param>
        internal void AddPhysicsItem(DancePhysicsItem item)
        {
            if (item.Body != null)
            {
                this.World.Add(item.Body.GetOrCreateBody());
            }

            foreach (DanceJoint joint in item.Joints)
            {
                Joint? entity = joint.GetOrCreateJoint(this.World);
                if (entity != null)
                {
                    entity.Tag = joint;
                }
            }

            foreach (DanceController controller in item.Controllers)
            {
                Controller entity = controller.GetOrCreateController(this.World);
                this.World.Add(entity);
            }
        }

        /// <summary>
        /// 移除物理项
        /// </summary>
        /// <param name="item"></param>
        internal void RemovePhysicsItem(DancePhysicsItem item)
        {
            if (item.Body != null)
            {
                this.World.Remove(item.Body.GetOrCreateBody());
            }

            foreach (DanceJoint joint in item.Joints)
            {
                this.World.Remove(joint.GetOrCreateJoint(this.World));
            }

            foreach (DanceController controller in item.Controllers)
            {
                this.World.Remove(controller.GetOrCreateController(this.World));
            }
        }

        /// <summary>
        /// 添加子项
        /// </summary>
        /// <param name="child">子项</param>
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            if (child is not DancePhysicsItem item)
                return;

            this.AddPhysicsItem(item);
        }

        /// <summary>
        /// 移除子项
        /// </summary>
        /// <param name="child">子项</param>
        /// <param name="oldLogicalIndex">逻辑位置</param>
        protected override void OnChildRemoved(Element child, int oldLogicalIndex)
        {
            base.OnChildRemoved(child, oldLogicalIndex);

            if (child is not DancePhysicsItem item)
                return;

            this.RemovePhysicsItem(item);
        }

        /// <summary>
        /// 获取布局管理器
        /// </summary>
        /// <returns>布局管理器</returns>
        protected override ILayoutManager CreateLayoutManager()
        {
            this.LayoutManager = new(this);
            return this.LayoutManager;
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void DancePhysicsItemsControl_Loaded(object? sender, EventArgs e)
        {
            if (this.RunningAnimation != null)
                return;

            this.RunningAnimation = new(v =>
            {
                if (this.IsVisible && this.IsRunning && this.LayoutManager != null && this.RunningAnimationStopwatch != null)
                {
                    this.LayoutManager.UpdatingTime = this.RunningAnimationStopwatch.Elapsed;
                    this.InvalidateMeasure();
                }

            }, 0, double.MaxValue, null, null);

            this.RunningAnimationStopwatch = new Stopwatch();
            this.RunningAnimationStopwatch?.Restart();
            this.RunningAnimation.Commit(this, "RunningAnimation", 16, uint.MaxValue);
        }

        /// <summary>
        /// 卸载
        /// </summary>
        private void DancePhysicsItemsControl_Unloaded(object? sender, EventArgs e)
        {
            this.RunningAnimation?.Dispose();
            this.RunningAnimation = null;
            this.RunningAnimationStopwatch?.Stop();
            this.RunningAnimationStopwatch = null;
        }
    }
}