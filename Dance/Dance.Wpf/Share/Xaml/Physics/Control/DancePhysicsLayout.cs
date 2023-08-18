using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics.Joints;
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
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Collections;

namespace Dance.Wpf
{
    /// <summary>
    /// 物理布局
    /// </summary>
    public class DancePhysicsLayout : ItemsControl
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
            get { return (World)GetValue(WorldProperty); }
            set { SetValue(WorldProperty, value); }
        }

        /// <summary>
        /// 物理世界
        /// </summary>
        public static readonly DependencyProperty WorldProperty =
            DependencyProperty.Register("World", typeof(World), typeof(DancePhysicsLayout), new PropertyMetadata(null));

        #endregion

        #region Body -- 体元素

        /// <summary>
        /// 获取体元素
        /// </summary>
        /// <param name="obj">元素</param>
        /// <returns>值</returns>
        public static DanceBody GetBody(DependencyObject obj)
        {
            return (DanceBody)obj.GetValue(BodyProperty);
        }

        /// <summary>
        /// 设置体元素
        /// </summary>
        /// <param name="obj">元素</param>
        /// <param name="value">值</param>
        public static void SetBody(DependencyObject obj, DanceBody value)
        {
            obj.SetValue(BodyProperty, value);
        }

        /// <summary>
        /// 体元素
        /// </summary>
        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.RegisterAttached("Body", typeof(DanceBody), typeof(DancePhysicsLayout), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement visual)
                    return;

                if (visual.GetVisualTreeParent<DancePhysicsLayout>() is not DancePhysicsLayout layout)
                    return;

                if (e.OldValue is DanceBody oldValue)
                {
                    layout.RemoveBody(oldValue);
                }
                if (e.NewValue is DanceBody newValue)
                {
                    layout.AddBody(newValue);
                }
            })));

        #endregion

        #region Joints -- 关节元素集合

        /// <summary>
        /// 关节元素集合
        /// </summary>
        /// <param name="obj">元素</param>
        /// <returns>值</returns>
        public static DanceJointCollection GetJoints(DependencyObject obj)
        {
            return (DanceJointCollection)obj.GetValue(JointsProperty);
        }

        /// <summary>
        /// 设置关节元素集合
        /// </summary>
        /// <param name="obj">元素</param>
        /// <param name="value">值</param>
        public static void SetJoints(DependencyObject obj, DanceJointCollection value)
        {
            obj.SetValue(JointsProperty, value);
        }

        /// <summary>
        /// 关节元素集合
        /// </summary>
        public static readonly DependencyProperty JointsProperty =
            DependencyProperty.RegisterAttached("Joints", typeof(DanceJointCollection), typeof(DancePhysicsLayout), new DancePropertyMetadata(() => new DanceJointCollection(), new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement visual)
                    return;

                if (visual.GetVisualTreeParent<DancePhysicsLayout>() is not DancePhysicsLayout layout)
                    return;

                if (e.OldValue is DanceJointCollection oldValue)
                {
                    layout.RemoveJoints(oldValue);
                }
                if (e.NewValue is DanceJointCollection newValue)
                {
                    layout.AddJoints(newValue);
                }
            })));

        #endregion

        #region Controllers -- 控制器集合

        /// <summary>
        /// 设置控制器集合
        /// </summary>
        /// <param name="obj">元素</param>
        /// <returns>值</returns>
        public static DanceControllerCollection GetControllers(DependencyObject obj)
        {
            return (DanceControllerCollection)obj.GetValue(ControllersProperty);
        }

        /// <summary>
        /// 获取控制器集合
        /// </summary>
        /// <param name="obj">元素</param>
        /// <param name="value">值</param>
        public static void SetControllers(DependencyObject obj, DanceControllerCollection value)
        {
            obj.SetValue(ControllersProperty, value);
        }

        /// <summary>
        /// 获取控制器集合
        /// </summary>
        public static readonly DependencyProperty ControllersProperty =
            DependencyProperty.RegisterAttached("Controllers", typeof(DanceControllerCollection), typeof(DancePhysicsLayout), new DancePropertyMetadata(() => new DanceControllerCollection(), new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement visual)
                    return;

                if (visual.GetVisualTreeParent<DancePhysicsLayout>() is not DancePhysicsLayout layout)
                    return;

                if (e.OldValue is DanceControllerCollection oldValue)
                {
                    layout.RemoveControllers(oldValue);
                }
                if (e.NewValue is DanceControllerCollection newValue)
                {
                    layout.AddControllers(newValue);
                }

            })));

        #endregion

        #region IsRunning -- 是否正在运行

        /// <summary>
        /// 运行动画
        /// </summary>
        private Storyboard? RunningAnimation;

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
            DependencyProperty.Register("IsRunning", typeof(bool), typeof(DancePhysicsLayout), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePhysicsLayout layout)
                    return;

                if (e.NewValue is not bool isRunning)
                    return;

                if (isRunning)
                {
                    layout.RunningAnimation = new Storyboard();
                    layout.RunningAnimation.Children.Add(new DancePhysicsTimeLine(layout));
                    layout.RunningAnimation.Begin();
                }
                else
                {
                    layout.RunningAnimation?.Stop();
                    layout.RunningAnimation = null;
                }
            })));

        #endregion

        /// <summary>
        /// 项发生改变时触发
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            // 添加
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            {
                foreach (FrameworkElement item in e.NewItems)
                {
                    this.OnChildAdded(item);
                }
            }

            // 移出
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
            {
                foreach (FrameworkElement item in e.OldItems)
                {
                    this.OnChildRemoved(item);
                }
            }
        }

        /// <summary>
        /// 添加子元素
        /// </summary>
        private void OnChildAdded(FrameworkElement child)
        {
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
        private void OnChildRemoved(FrameworkElement child)
        {
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
