using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D.Dynamics;

namespace Dance.Maui
{
    /// <summary>
    /// 碰撞体元素
    /// </summary>
    public abstract class DanceFixture : DancePhysics
    {
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
            BindableProperty.Create(nameof(Position), typeof(DancePoint), typeof(DanceFixture), DancePoint.Zero);

        #endregion

        #region Density -- 密度

        /// <summary>
        /// 密度
        /// </summary>
        public float Density
        {
            get => (float)GetValue(DensityProperty);
            set => SetValue(DensityProperty, value);
        }

        /// <summary>
        /// 密度
        /// </summary>
        public static readonly BindableProperty DensityProperty =
            BindableProperty.Create(nameof(Density), typeof(float), typeof(DanceCircleFixture), 1f);

        #endregion

        #region Restitution -- 弹力系数

        /// <summary>
        /// 弹力系数
        /// </summary>
        public float Restitution
        {
            get => (float)GetValue(RestitutionProperty);
            set => SetValue(RestitutionProperty, value);
        }

        /// <summary>
        /// 弹力系数
        /// </summary>
        public static readonly BindableProperty RestitutionProperty =
            BindableProperty.Create(nameof(Restitution), typeof(float), typeof(DanceCircleFixture), 0.1f);

        #endregion

        #region Friction -- 摩擦力系数

        /// <summary>
        /// 摩擦力系数
        /// </summary>
        public float Friction
        {
            get => (float)GetValue(FrictionProperty);
            set => SetValue(FrictionProperty, value);
        }

        /// <summary>
        /// 摩擦力系数
        /// </summary>
        public static readonly BindableProperty FrictionProperty =
            BindableProperty.Create(nameof(Friction), typeof(float), typeof(DanceCircleFixture), 0.4f);

        #endregion

        /// <summary>
        /// 获取碰撞体
        /// </summary>
        /// <returns>碰撞体</returns>
        public abstract Fixture GetFixture();
    }
}
