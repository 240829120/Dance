using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Maui
{
    /// <summary>
    /// 物理控件项
    /// </summary>
    public class DancePhysicsItem : ContentView
    {
        public DancePhysicsItem()
        {
            this.Joints = [];
            this.Controllers = [];
        }

        #region Owner -- 所属

        /// <summary>
        /// 所属
        /// </summary>
        public DancePhysicsItemsControl? Owner
        {
            get => (DancePhysicsItemsControl?)GetValue(OwnerProperty);
            set => SetValue(OwnerProperty, value);
        }

        /// <summary>
        /// 所属
        /// </summary>
        public static readonly BindableProperty OwnerProperty =
            BindableProperty.Create(nameof(Owner), typeof(DancePhysicsItemsControl), typeof(DancePhysicsItem), null);

        #endregion

        #region Body -- 刚体

        /// <summary>
        /// 刚体
        /// </summary>
        public DanceBody? Body
        {
            get => (DanceBody?)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        /// <summary>
        /// 刚体
        /// </summary>
        public static readonly BindableProperty BodyProperty =
            BindableProperty.Create(nameof(Body), typeof(DanceBody), typeof(DancePhysicsItem), null);

        #endregion

        #region Joints -- 连接点集合

        /// <summary>
        /// 连接点集合
        /// </summary>
        public List<DanceJoint> Joints
        {
            get => (List<DanceJoint>)GetValue(JointsProperty);
            set => SetValue(JointsProperty, value);
        }

        /// <summary>
        /// 连接点集合
        /// </summary>
        public static readonly BindableProperty JointsProperty =
            BindableProperty.Create(nameof(Joints), typeof(List<DanceJoint>), typeof(DancePhysicsItem), null);

        #endregion

        #region Controllers -- 控制器集合

        /// <summary>
        /// 连接点集合
        /// </summary>
        public List<DanceController> Controllers
        {
            get => (List<DanceController>)GetValue(ControllersProperty);
            set => SetValue(ControllersProperty, value);
        }

        /// <summary>
        /// 连接点集合
        /// </summary>
        public static readonly BindableProperty ControllersProperty =
            BindableProperty.Create(nameof(Controllers), typeof(List<DanceController>), typeof(DancePhysicsItem), null);

        #endregion
    }
}
