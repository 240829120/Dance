using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using nkast.Aether.Physics2D.Dynamics;

namespace Dance.Wpf
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
            get { return (DancePoint)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// 位置
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(DancePoint), typeof(DanceFixture), new PropertyMetadata(DancePoint.Zero));

        #endregion

        #region Density -- 密度

        /// <summary>
        /// 密度
        /// </summary>
        public float Density
        {
            get { return (float)GetValue(DensityProperty); }
            set { SetValue(DensityProperty, value); }
        }

        /// <summary>
        /// 密度
        /// </summary>
        public static readonly DependencyProperty DensityProperty =
            DependencyProperty.Register("Density", typeof(float), typeof(DanceFixture), new PropertyMetadata(1f));

        #endregion

        #region Restitution -- 弹力系数

        /// <summary>
        /// 弹力系数
        /// </summary>
        public float Restitution
        {
            get { return (float)GetValue(RestitutionProperty); }
            set { SetValue(RestitutionProperty, value); }
        }

        /// <summary>
        /// 弹力系数
        /// </summary>
        public static readonly DependencyProperty RestitutionProperty =
            DependencyProperty.Register("Restitution", typeof(float), typeof(DanceFixture), new PropertyMetadata(0.1f));

        #endregion

        #region Friction -- 摩擦力系数

        /// <summary>
        /// 摩擦力系数
        /// </summary>
        public float Friction
        {
            get { return (float)GetValue(FrictionProperty); }
            set { SetValue(FrictionProperty, value); }
        }

        /// <summary>
        /// 摩擦力系数
        /// </summary>
        public static readonly DependencyProperty FrictionProperty =
            DependencyProperty.Register("Friction", typeof(float), typeof(DanceFixture), new PropertyMetadata(0.4f));

        #endregion

        /// <summary>
        /// 获取碰撞体
        /// </summary>
        /// <returns>碰撞体</returns>
        public abstract Fixture GetFixture();
    }
}
