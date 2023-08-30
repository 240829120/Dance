using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 球粒子控制器
    /// </summary>
    public class DanceParticleControllerBall : DanceParticleControllerBase
    {
        #region Position -- 位置

        /// <summary>
        /// 位置
        /// </summary>
        public DancePoint3 Position
        {
            get { return (DancePoint3)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// 位置
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(DancePoint3), typeof(DanceParticleControllerBall), new PropertyMetadata(DancePoint3.Zero));

        #endregion

        #region Radius -- 半径

        /// <summary>
        /// 半径
        /// </summary>
        public float Radius
        {
            get { return (float)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        /// <summary>
        /// 半径
        /// </summary>
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(float), typeof(DanceParticleControllerBall), new PropertyMetadata(100f));

        #endregion

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="dt">渲染时间</param>
        /// <returns>新生成的粒子</returns>
        public override IList<IDanceParticle> Generate(TimeSpan dt)
        {
            IList<IDanceParticle> list = base.Generate(dt);

            foreach (IDanceParticle particle in list)
            {
                float radius = this.Random.NextFloat(0, this.Radius);
                Vector3 vector = new(this.Random.NextFloat(-1, 1), this.Random.NextFloat(-1, 1), this.Random.NextFloat(-1, 1));
                Vector3 point = radius * Vector3.Normalize(vector);

                particle.X = this.Position.X + point.X;
                particle.Y = this.Position.Y + point.Y;
                particle.Z = this.Position.Z + point.Z;

                particle.TranslateSpeedX = this.Random.NextFloat(this.TranslateSpeedX.MinValue, this.TranslateSpeedX.MaxValue);
                particle.TranslateSpeedY = this.Random.NextFloat(this.TranslateSpeedY.MinValue, this.TranslateSpeedY.MaxValue);
                particle.TranslateSpeedZ = this.Random.NextFloat(this.TranslateSpeedZ.MinValue, this.TranslateSpeedZ.MaxValue);

                particle.RotateSpeedX = this.Random.NextFloat(this.RotationSpeedX.MinValue, this.RotationSpeedX.MaxValue);
                particle.RotateSpeedY = this.Random.NextFloat(this.RotationSpeedY.MinValue, this.RotationSpeedY.MaxValue);
                particle.RotateSpeedZ = this.Random.NextFloat(this.RotationSpeedZ.MinValue, this.RotationSpeedZ.MaxValue);
            }

            return list;
        }
    }
}
