using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Controllers;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Joints;

namespace Dance.Wpf
{
    /// <summary>
    /// 物理引擎控件
    /// </summary>
    public class DancePhysicsItemsControl : ItemsControl
    {
        static DancePhysicsItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DancePhysicsItemsControl), new FrameworkPropertyMetadata(typeof(DancePhysicsItemsControl)));
        }

        /// <summary>
        /// 物理世界
        /// </summary>
        internal World World = new(new Vector2(0, 10));

        #region IsRunning -- 是否正在运行

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register("IsRunning", typeof(bool), typeof(DancePhysicsItemsControl), new PropertyMetadata(true));

        #endregion

        #region StepSpeed -- 步骤速度

        /// <summary>
        /// 步骤速度
        /// </summary>
        public double StepSpeed
        {
            get { return (double)GetValue(StepSpeedProperty); }
            set { SetValue(StepSpeedProperty, value); }
        }

        /// <summary>
        /// 步骤速度
        /// </summary>
        public static readonly DependencyProperty StepSpeedProperty =
            DependencyProperty.Register("StepSpeed", typeof(double), typeof(DancePhysicsItemsControl), new PropertyMetadata(1d));

        #endregion

        #region Gravity -- 重力

        /// <summary>
        /// 重力
        /// </summary>
        public DanceVector Gravity
        {
            get { return (DanceVector)GetValue(GravityProperty); }
            set { SetValue(GravityProperty, value); }
        }

        /// <summary>
        /// 重力
        /// </summary>
        public static readonly DependencyProperty GravityProperty =
            DependencyProperty.Register("Gravity", typeof(DanceVector), typeof(DancePhysicsItemsControl), new PropertyMetadata(new DanceVector(0, 10), new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePhysicsItemsControl itemsControl)
                    return;

                if (e.NewValue is not DanceVector vector)
                    return;

                itemsControl.World.Gravity = new Vector2(vector.X, vector.Y);

            })));

        #endregion

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is DancePhysicsItem container)
            {
                container.Owner = this;

                return true;
            }
            else
            {
                return false;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DancePhysicsItem()
            {
                Owner = this
            };
        }

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
    }
}
