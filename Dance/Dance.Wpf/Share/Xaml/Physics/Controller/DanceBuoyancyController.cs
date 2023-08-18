using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Controllers;
using tainicom.Aether.Physics2D.Dynamics;

namespace Dance.Wpf
{
    /// <summary>
    /// 浮力控制器
    /// </summary>
    public class DanceBuoyancyController : DanceController
    {
        /// <summary>
        /// 控制器
        /// </summary>
        public BuoyancyController? Controller { get; private set; }

        #region WorldLowerBound -- 世界下限

        /// <summary>
        /// 世界下限
        /// </summary>
        public DancePoint WorldLowerBound
        {
            get { return (DancePoint)GetValue(WorldLowerBoundProperty); }
            set { SetValue(WorldLowerBoundProperty, value); }
        }

        /// <summary>
        /// 世界下限
        /// </summary>
        public static readonly DependencyProperty WorldLowerBoundProperty =
            DependencyProperty.Register("WorldLowerBound", typeof(DancePoint), typeof(DanceBuoyancyController), new PropertyMetadata(DancePoint.Zero));

        #endregion

        #region WorldUpperBound -- 世界上限

        /// <summary>
        /// 世界上限
        /// </summary>
        public DancePoint WorldUpperBound
        {
            get { return (DancePoint)GetValue(WorldUpperBoundProperty); }
            set { SetValue(WorldUpperBoundProperty, value); }
        }

        /// <summary>
        /// 世界上限
        /// </summary>
        public static readonly DependencyProperty WorldUpperBoundProperty =
            DependencyProperty.Register("WorldUpperBound", typeof(DancePoint), typeof(DanceBuoyancyController), new PropertyMetadata(DancePoint.Zero));

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
            DependencyProperty.Register("Density", typeof(float), typeof(DanceBuoyancyController), new PropertyMetadata(1.5f));

        #endregion

        #region LinearDragCoefficient -- 流体线性阻力系数

        /// <summary>
        /// 流体线性阻力系数
        /// </summary>
        public float LinearDragCoefficient
        {
            get { return (float)GetValue(LinearDragCoefficientProperty); }
            set { SetValue(LinearDragCoefficientProperty, value); }
        }

        /// <summary>
        /// 流体线性阻力系数
        /// </summary>
        public static readonly DependencyProperty LinearDragCoefficientProperty =
            DependencyProperty.Register("LinearDragCoefficient", typeof(float), typeof(DanceBuoyancyController), new PropertyMetadata(0.7f));

        #endregion

        #region RotationalDragCoefficient -- 流体旋转阻力系数

        /// <summary>
        /// 流体旋转阻力系数
        /// </summary>
        public float RotationalDragCoefficient
        {
            get { return (float)GetValue(RotationalDragCoefficientProperty); }
            set { SetValue(RotationalDragCoefficientProperty, value); }
        }

        /// <summary>
        /// 流体旋转阻力系数
        /// </summary>
        public static readonly DependencyProperty RotationalDragCoefficientProperty =
            DependencyProperty.Register("RotationalDragCoefficient", typeof(float), typeof(DanceBuoyancyController), new PropertyMetadata(0.8f));

        #endregion

        /// <summary>
        /// 获取或创建控制器
        /// </summary>
        /// <param name="world">物理世界</param>
        /// <returns>控制器</returns>
        public override Controller GetOrCreateController(World world)
        {
            if (this.Controller != null)
                return this.Controller;

            lock (this.lock_object)
            {
                if (this.Controller != null)
                    return this.Controller;

                Vector2 lower = new(this.WorldLowerBound.X, this.WorldLowerBound.Y);
                Vector2 upper = new(this.WorldUpperBound.X, this.WorldUpperBound.Y);

                this.Controller = new BuoyancyController(new AABB(lower, upper), this.Density, this.LinearDragCoefficient, this.RotationalDragCoefficient, world.Gravity);
            }

            return this.Controller;
        }
    }
}