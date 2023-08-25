using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;

namespace Dance.Maui
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

        #region BodyType -- 类型

        /// <summary>
        /// 类型
        /// </summary>
        public BodyType BodyType
        {
            get => (BodyType)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        /// <summary>
        /// 类型
        /// </summary>
        public static readonly BindableProperty WidthProperty =
            BindableProperty.Create(nameof(BodyType), typeof(BodyType), typeof(DanceBody), BodyType.Dynamic);

        #endregion

        #region Fixtures -- 碰撞体集合

        /// <summary>
        /// 碰撞体集合
        /// </summary>
        public IList<DanceFixture> Fixtures
        {
            get => (IList<DanceFixture>)GetValue(FixturesProperty);
            set => SetValue(FixturesProperty, value);
        }

        /// <summary>
        /// 碰撞体集合
        /// </summary>
        public static readonly BindableProperty FixturesProperty =
            BindableProperty.Create(nameof(Fixtures), typeof(IList<DanceFixture>), typeof(DanceBody), defaultValueCreator: b => new List<DanceFixture>());

        #endregion

        #region LinearVelocity -- 线速度

        /// <summary>
        /// 线速度
        /// </summary>
        public DanceVector LinearVelocity
        {
            get => (DanceVector)GetValue(LinearVelocityProperty);
            set => SetValue(LinearVelocityProperty, value);
        }

        /// <summary>
        /// 线速度
        /// </summary>
        public static readonly BindableProperty LinearVelocityProperty =
            BindableProperty.Create(nameof(LinearVelocity), typeof(DanceVector), typeof(DanceBody), DanceVector.Zero);

        #endregion

        #region AngularVelocity -- 角速度

        /// <summary>
        /// 角速度
        /// </summary>
        public float AngularVelocity
        {
            get => (float)GetValue(AngularVelocityProperty);
            set => SetValue(AngularVelocityProperty, value);
        }

        /// <summary>
        /// 角速度
        /// </summary>
        public static readonly BindableProperty AngularVelocityProperty =
            BindableProperty.Create(nameof(AngularVelocity), typeof(float), typeof(DanceBody), 0f);

        #endregion

        #region LinearDamping -- 线速度阻尼

        /// <summary>
        /// 线速度阻尼
        /// </summary>
        public float LinearDamping
        {
            get => (float)GetValue(LinearDampingProperty);
            set => SetValue(LinearDampingProperty, value);
        }

        /// <summary>
        /// 线速度阻尼
        /// </summary>
        public static readonly BindableProperty LinearDampingProperty =
            BindableProperty.Create(nameof(LinearDamping), typeof(float), typeof(DanceBody), 0f);

        #endregion

        #region AngularDamping -- 角速度阻尼

        /// <summary>
        /// 角速度阻尼
        /// </summary>
        public float AngularDamping
        {
            get => (float)GetValue(AngularDampingProperty);
            set => SetValue(AngularDampingProperty, value);
        }

        /// <summary>
        /// 角速度阻尼
        /// </summary>
        public static readonly BindableProperty AngularDampingProperty =
            BindableProperty.Create(nameof(AngularDamping), typeof(float), typeof(DanceBody), 0f);

        #endregion

        #region Position -- 位置

        /// <summary>
        /// 位置
        /// </summary>
        public DancePoint Position
        {
            get => (DancePoint)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        /// <summary>
        /// 位置
        /// </summary>
        public static readonly BindableProperty PositionProperty =
            BindableProperty.Create(nameof(Position), typeof(DancePoint), typeof(DanceBody), DancePoint.Zero);

        #endregion

        #region Rotation -- 旋转角度

        /// <summary>
        /// 旋转角度
        /// </summary>
        public float Rotation
        {
            get => (float)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public static readonly BindableProperty RotationProperty =
            BindableProperty.Create(nameof(Rotation), typeof(float), typeof(DanceBody), 0f);

        #endregion

        #region IgnoreGravity -- 忽略重力

        /// <summary>
        /// 忽略重力
        /// </summary>
        public bool IgnoreGravity
        {
            get => (bool)GetValue(IgnoreGravityProperty);
            set => SetValue(IgnoreGravityProperty, value);
        }

        /// <summary>
        /// 忽略重力
        /// </summary>
        public static readonly BindableProperty IgnoreGravityProperty =
            BindableProperty.Create(nameof(IgnoreGravity), typeof(bool), typeof(DanceBody), false);

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
                    IgnoreGravity = this.IgnoreGravity
                };

                this.Body.ResetMassData();

                foreach (DanceFixture fixtureElement in this.Fixtures)
                {
                    Body.Add(fixtureElement.GetFixture());
                }

                return Body;
            }
        }
    }
}
