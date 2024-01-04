using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 物理控件项
    /// </summary>
    public class DancePhysicsItem : ContentControl
    {
        static DancePhysicsItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DancePhysicsItem), new FrameworkPropertyMetadata(typeof(DancePhysicsItem)));
        }

        public DancePhysicsItem()
        {
            this.Joints = [];
            this.Controllers = [];
        }

        /// <summary>
        /// 所属
        /// </summary>
        internal DancePhysicsItemsControl? Owner;

        #region Body -- 刚体

        /// <summary>
        /// 刚体
        /// </summary>
        public DanceBody Body
        {
            get { return (DanceBody)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        /// <summary>
        /// 刚体
        /// </summary>
        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register("Body", typeof(DanceBody), typeof(DancePhysicsItem), new PropertyMetadata(null));

        #endregion

        #region Joints -- 连接点集合

        /// <summary>
        /// 连接点集合
        /// </summary>
        public List<DanceJoint> Joints
        {
            get { return (List<DanceJoint>)GetValue(JointsProperty); }
            set { SetValue(JointsProperty, value); }
        }

        /// <summary>
        /// 连接点集合
        /// </summary>
        public static readonly DependencyProperty JointsProperty =
            DependencyProperty.Register("Joints", typeof(List<DanceJoint>), typeof(DancePhysicsItem), new PropertyMetadata(null));

        #endregion

        #region Controllers -- 控制器集合

        /// <summary>
        /// 控制器集合
        /// </summary>
        public List<DanceController> Controllers
        {
            get { return (List<DanceController>)GetValue(ControllersProperty); }
            set { SetValue(ControllersProperty, value); }
        }

        /// <summary>
        /// 控制器集合
        /// </summary>
        public static readonly DependencyProperty ControllersProperty =
            DependencyProperty.Register("Controllers", typeof(List<DanceController>), typeof(DancePhysicsItem), new PropertyMetadata(null));

        #endregion
    }
}
