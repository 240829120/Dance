using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 线性粒子控制器
    /// </summary>
    public class DanceParticleControllerLinner : DanceParticleControllerBase
    {
        #region PointX -- X点

        /// <summary>
        /// X点
        /// </summary>
        public DancePoint PointX
        {
            get { return (DancePoint)GetValue(PointXProperty); }
            set { SetValue(PointXProperty, value); }
        }

        /// <summary>
        /// X点
        /// </summary>
        public static readonly DependencyProperty PointXProperty =
            DependencyProperty.Register("PointX", typeof(DancePoint), typeof(DanceParticleControllerLinner), new PropertyMetadata(DancePoint.Zero));

        #endregion

        #region PointY -- Y点

        /// <summary>
        /// Y点
        /// </summary>
        public DancePoint PointY
        {
            get { return (DancePoint)GetValue(PointYProperty); }
            set { SetValue(PointYProperty, value); }
        }

        /// <summary>
        /// Y点
        /// </summary>
        public static readonly DependencyProperty PointYProperty =
            DependencyProperty.Register("PointY", typeof(DancePoint), typeof(DanceParticleControllerLinner), new PropertyMetadata(new DancePoint(800, 0)));

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
                particle.X = this.Random.NextFloat(this.PointX.X, this.PointY.X);
                particle.Y = this.Random.NextFloat(this.PointX.Y, this.PointY.Y);

                particle.TranslateSpeedX = this.Random.NextFloat(this.TranslateSpeedX.MinValue, this.TranslateSpeedX.MaxValue);
                particle.TranslateSpeedY = this.Random.NextFloat(this.TranslateSpeedY.MinValue, this.TranslateSpeedY.MaxValue);

                particle.RotateSpeedX = this.Random.NextFloat(this.RotationSpeedX.MinValue, this.RotationSpeedX.MaxValue);
                particle.RotateSpeedY = this.Random.NextFloat(this.RotationSpeedY.MinValue, this.RotationSpeedY.MaxValue);
                particle.RotateSpeedZ = this.Random.NextFloat(this.RotationSpeedZ.MinValue, this.RotationSpeedZ.MaxValue);
            }

            return list;
        }
    }
}
