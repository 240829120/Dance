using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;

namespace Dance.Wpf
{
    /// <summary>
    /// 体元素
    /// </summary>
    [ContentProperty(nameof(Fixtures))]
    public class DanceBody : DancePhysics
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object lock_object = new();

        /// <summary>
        /// 体对象
        /// </summary>
        public Body? Body { get; private set; }

        public DanceBody()
        {
            this.Fixtures = [];
        }

        #region BodyType -- 类型

        /// <summary>
        /// 类型
        /// </summary>
        public BodyType BodyType
        {
            get { return (BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(BodyType), typeof(DanceBody), new PropertyMetadata(BodyType.Dynamic));

        #endregion

        #region Fixtures -- 碰撞体集合

        /// <summary>
        /// 碰撞体集合
        /// </summary>
        public List<DanceFixture> Fixtures
        {
            get { return (List<DanceFixture>)GetValue(FixturesProperty); }
            set { SetValue(FixturesProperty, value); }
        }

        /// <summary>
        /// 碰撞体集合
        /// </summary>
        public static readonly DependencyProperty FixturesProperty =
            DependencyProperty.Register("Fixtures", typeof(List<DanceFixture>), typeof(DanceBody), new PropertyMetadata(null));

        #endregion

        #region LinearVelocity -- 线速度

        /// <summary>
        /// 线速度
        /// </summary>
        public DanceVector LinearVelocity
        {
            get { return (DanceVector)GetValue(LinearVelocityProperty); }
            set { SetValue(LinearVelocityProperty, value); }
        }

        /// <summary>
        /// 线速度
        /// </summary>
        public static readonly DependencyProperty LinearVelocityProperty =
            DependencyProperty.Register("LinearVelocity", typeof(DanceVector), typeof(DanceBody), new PropertyMetadata(DanceVector.Zero));

        #endregion

        #region AngularVelocity -- 角速度

        /// <summary>
        /// 角速度
        /// </summary>
        public float AngularVelocity
        {
            get { return (float)GetValue(AngularVelocityProperty); }
            set { SetValue(AngularVelocityProperty, value); }
        }

        /// <summary>
        /// 角速度
        /// </summary>
        public static readonly DependencyProperty AngularVelocityProperty =
            DependencyProperty.Register("AngularVelocity", typeof(float), typeof(DanceBody), new PropertyMetadata(0f));

        #endregion

        #region LinearDamping -- 线速度阻尼

        /// <summary>
        /// 线速度阻尼
        /// </summary>
        public float LinearDamping
        {
            get { return (float)GetValue(LinearDampingProperty); }
            set { SetValue(LinearDampingProperty, value); }
        }

        /// <summary>
        /// 线速度阻尼
        /// </summary>
        public static readonly DependencyProperty LinearDampingProperty =
            DependencyProperty.Register("LinearDamping", typeof(float), typeof(DanceBody), new PropertyMetadata(0f));

        #endregion

        #region AngularDamping -- 角速度阻尼

        /// <summary>
        /// 角速度阻尼
        /// </summary>
        public float AngularDamping
        {
            get { return (float)GetValue(AngularDampingProperty); }
            set { SetValue(AngularDampingProperty, value); }
        }

        /// <summary>
        /// 角速度阻尼
        /// </summary>
        public static readonly DependencyProperty AngularDampingProperty =
            DependencyProperty.Register("AngularDamping", typeof(float), typeof(DanceBody), new PropertyMetadata(0f));

        #endregion

        #region Position -- 位置

        /// <summary>
        /// 位置
        /// </summary>
        public DancePoint Position
        {
            get { return (DancePoint)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// 位置
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(DancePoint), typeof(DanceBody), new PropertyMetadata(DancePoint.Zero));

        #endregion

        #region Rotation -- 旋转角度

        /// <summary>
        /// 旋转角度
        /// </summary>
        public float Rotation
        {
            get { return (float)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(float), typeof(DanceBody), new PropertyMetadata(0f));

        #endregion

        #region IgnoreGravity -- 忽略重力

        /// <summary>
        /// 忽略重力
        /// </summary>
        public bool IgnoreGravity
        {
            get { return (bool)GetValue(IgnoreGravityProperty); }
            set { SetValue(IgnoreGravityProperty, value); }
        }

        /// <summary>
        /// 忽略重力
        /// </summary>
        public static readonly DependencyProperty IgnoreGravityProperty =
            DependencyProperty.Register("IgnoreGravity", typeof(bool), typeof(DanceBody), new PropertyMetadata(false));

        #endregion

        /// <summary>
        /// 获取或创建体
        /// </summary>
        /// <returns>体</returns>
        public Body GetOrCreateBody()
        {
            if (this.Body != null)
                return this.Body;

            lock (this.lock_object)
            {
                if (this.Body != null)
                    return this.Body;

                this.Body = new()
                {
                    BodyType = this.BodyType,
                    LinearVelocity = new Vector2(this.LinearVelocity.X, this.LinearVelocity.Y),
                    AngularVelocity = this.AngularVelocity,
                    LinearDamping = this.LinearDamping,
                    AngularDamping = this.AngularDamping,
                    Position = new Vector2(this.Position.X, this.Position.Y),
                    Rotation = this.Rotation,
                    IgnoreGravity = this.IgnoreGravity,
                };

                foreach (DanceFixture fixtureElement in this.Fixtures)
                {
                    Body.Add(fixtureElement.GetFixture());
                }

                return Body;
            }
        }
    }
}
