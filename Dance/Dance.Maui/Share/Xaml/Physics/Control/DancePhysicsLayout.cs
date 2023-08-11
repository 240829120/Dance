using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics.Joints;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;

namespace Dance.Maui
{
    /// <summary>
    /// 物理布局
    /// </summary>
    public class DancePhysicsLayout : Layout
    {
        public DancePhysicsLayout()
        {
            this.World = new(new Vector2(0, 10));
            this.World.JointRemoved += this.OnJointRemoved;
        }

        /// <summary>
        /// 关节信息
        /// </summary>
        internal ConcurrentDictionary<Joint, DanceJoint> JointDic = new();

        #region World -- 物理世界

        /// <summary>
        /// 物理世界
        /// </summary>
        public World World
        {
            get => (World)GetValue(WorldProperty);
            private set => SetValue(WorldProperty, value);
        }

        /// <summary>
        /// 物理世界
        /// </summary>
        public static readonly BindableProperty WorldProperty =
            BindableProperty.Create(nameof(World), typeof(World), typeof(DancePhysicsLayout), null);

        #endregion

        #region Body -- 体元素

        /// <summary>
        /// 获取体元素
        /// </summary>
        /// <param name="view">元素</param>
        /// <returns>值</returns>
        public static DanceBody GetBody(BindableObject view)
        {
            return (DanceBody)view.GetValue(BodyProperty);
        }

        /// <summary>
        /// 设置体元素
        /// </summary>
        /// <param name="view">元素</param>
        /// <param name="value">值</param>
        public static void SetBody(BindableObject view, DanceBody value)
        {
            view.SetValue(BodyProperty, value);
        }

        /// <summary>
        /// 体元素
        /// </summary>
        public static readonly BindableProperty BodyProperty =
            BindableProperty.CreateAttached("Body", typeof(DanceBody), typeof(DancePhysicsLayout), null, BindingMode.OneWay,
                validateValue: new BindableProperty.ValidateValueDelegate((o, v) => v == null || v is DanceBody),
                propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((o, _old, _new) =>
                {
                    if (o is not VisualElement visual)
                        return;

                    if (visual.GetVisualElementParent<DancePhysicsLayout>() is not DancePhysicsLayout layout)
                        return;

                    if (_old is DanceBody oldValue)
                    {
                        layout.RemoveBody(oldValue);
                    }
                    if (_new is DanceBody newValue)
                    {
                        layout.AddBody(newValue);
                    }
                }));

        #endregion

        #region Joints -- 关节元素集合

        /// <summary>
        /// 关节元素集合
        /// </summary>
        /// <param name="view">元素</param>
        /// <returns>值</returns>
        public static IList<DanceJoint> GetJoints(BindableObject view)
        {
            return (IList<DanceJoint>)view.GetValue(JointsProperty);
        }

        /// <summary>
        /// 关节元素集合
        /// </summary>
        /// <param name="view">元素</param>
        /// <param name="value">值</param>
        public static void SetJoints(BindableObject view, IList<DanceJoint> value)
        {
            view.SetValue(JointsProperty, value);
        }

        /// <summary>
        /// 关节元素集合
        /// </summary>
        public static readonly BindableProperty JointsProperty =
            BindableProperty.CreateAttached("Joints", typeof(IList<DanceJoint>), typeof(DancePhysicsLayout), null, BindingMode.OneWay,
                validateValue: new BindableProperty.ValidateValueDelegate((o, v) => v == null || v is IList<DanceJoint>),
                defaultValueCreator: b => new List<DanceJoint>(),
                propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((o, _old, _new) =>
                {
                    if (o is not VisualElement visual)
                        return;

                    if (visual.GetVisualElementParent<DancePhysicsLayout>() is not DancePhysicsLayout layout)
                        return;

                    if (_old is IList<DanceJoint> oldValue)
                    {
                        layout.RemoveJoints(oldValue);
                    }
                    if (_new is IList<DanceJoint> newValue)
                    {
                        layout.AddJoints(newValue);
                    }
                }));

        #endregion

        #region Controllers -- 控制器集合

        /// <summary>
        /// 控制器集合
        /// </summary>
        /// <param name="view">元素</param>
        /// <returns>值</returns>
        public static IList<DanceController> GetControllers(BindableObject view)
        {
            return (IList<DanceController>)view.GetValue(ControllersProperty);
        }

        /// <summary>
        /// 控制器集合
        /// </summary>
        /// <param name="view">元素</param>
        /// <param name="value">值</param>
        public static void SetControllers(BindableObject view, IList<DanceController> value)
        {
            view.SetValue(ControllersProperty, value);
        }

        /// <summary>
        /// 控制器集合
        /// </summary>
        public static readonly BindableProperty ControllersProperty =
            BindableProperty.CreateAttached("Controllers", typeof(IList<DanceController>), typeof(DancePhysicsLayout), null, BindingMode.OneWay,
                validateValue: new BindableProperty.ValidateValueDelegate((o, v) => v == null || v is IList<DanceController>),
                defaultValueCreator: b => new List<DanceController>(),
                propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((o, _old, _new) =>
                {
                    if (o is not VisualElement visual)
                        return;

                    if (visual.GetVisualElementParent<DancePhysicsLayout>() is not DancePhysicsLayout layout)
                        return;

                    if (_old is IList<DanceController> oldValue)
                    {
                        layout.RemoveControllers(oldValue);
                    }
                    if (_new is IList<DanceController> newValue)
                    {
                        layout.AddControllers(newValue);
                    }
                }));

        #endregion

        #region IsRunning -- 是否正在运行

        /// <summary>
        /// 运行动画
        /// </summary>
        private Animation? RunningAnimation;

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
            BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(DancePhysicsLayout), false, BindingMode.OneWay,
                propertyChanged: (s, o, n) =>
                {
                    if (s is not DancePhysicsLayout layout)
                        return;

                    if (n is not bool isRunning)
                        return;

                    if (isRunning)
                    {
                        layout.RunningAnimation = new(v =>
                        {
                            if (layout.IsVisible)
                            {
                                layout.InvalidateMeasure();
                            }

                        }, 0, double.MaxValue, null, null);

                        layout.RunningAnimation.Commit(layout, "IsRunning", 16, uint.MaxValue);
                    }
                    else
                    {
                        layout.RunningAnimation?.Dispose();
                        layout.RunningAnimation = null;
                    }
                });

        #endregion

        /// <summary>
        /// 创建布局管理器
        /// </summary>
        /// <returns>布局管理器</returns>
        protected override ILayoutManager CreateLayoutManager()
        {
            return new DancePhysicsLayoutManager(this);
        }

        /// <summary>
        /// 添加子元素
        /// </summary>
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            if (GetBody(child) is DanceBody bodyElement)
            {
                this.AddBody(bodyElement);
            }

            if (GetJoints(child) is IList<DanceJoint> jointElements)
            {
                this.AddJoints(jointElements);
            }

            if (GetControllers(child) is IList<DanceController> controllerElements)
            {
                this.AddControllers(controllerElements);
            }
        }

        /// <summary>
        /// 移除子元素
        /// </summary>
        protected override void OnChildRemoved(Element child, int oldLogicalIndex)
        {
            base.OnChildRemoved(child, oldLogicalIndex);

            if (GetBody(child) is DanceBody bodyElement)
            {
                this.RemoveBody(bodyElement);
            }

            if (GetJoints(child) is IList<DanceJoint> jointElements)
            {
                this.RemoveJoints(jointElements);
            }

            if (GetControllers(child) is IList<DanceController> controllerElements)
            {
                this.RemoveControllers(controllerElements);
            }
        }

        /// <summary>
        /// 添加体
        /// </summary>
        /// <param name="bodyElement">体元素</param>
        private void AddBody(DanceBody bodyElement)
        {
            this.World.Add(bodyElement.GetOrCreateBody());
        }

        /// <summary>
        /// 移除体
        /// </summary>
        /// <param name="bodyElement">体元素</param>
        private void RemoveBody(DanceBody bodyElement)
        {
            this.World.Remove(bodyElement.GetOrCreateBody());
        }

        /// <summary>
        /// 添加关节
        /// </summary>
        /// <param name="jointElements">关节元素集合</param>
        private void AddJoints(IList<DanceJoint> jointElements)
        {
            foreach (DanceJoint jointElement in jointElements)
            {
                Joint? joint = jointElement.GetOrCreateJoint(this.World);
                if (joint == null)
                    continue;

                this.JointDic.TryAdd(joint, jointElement);
            }
        }

        /// <summary>
        /// 移除关节
        /// </summary>
        /// <param name="jointElements">关节元素集合</param>
        private void RemoveJoints(IList<DanceJoint> jointElements)
        {
            foreach (DanceJoint jointElement in jointElements)
            {
                Joint? joint = jointElement.GetOrCreateJoint(this.World);
                if (joint == null)
                    continue;

                this.World.Remove(joint);
            }
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="controllerElements">控制器元素集合</param>
        private void AddControllers(IList<DanceController> controllerElements)
        {
            foreach (DanceController controllerElement in controllerElements)
            {
                this.World.Add(controllerElement.GetOrCreateController(this.World));
            }
        }

        /// <summary>
        /// 移除控制器
        /// </summary>
        /// <param name="controllerElements">控制器元素集合</param>
        private void RemoveControllers(IList<DanceController> controllerElements)
        {
            foreach (DanceController controllerElement in controllerElements)
            {
                this.World.Remove(controllerElement.GetOrCreateController(this.World));
            }
        }

        /// <summary>
        /// 关节移除时触发
        /// </summary>
        private void OnJointRemoved(World sender, Joint joint)
        {
            this.JointDic.TryRemove(joint, out _);
        }
    }
}
