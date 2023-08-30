using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 盒子粒子控制器
    /// </summary>
    public class DanceParticleControllerBox : DanceParticleControllerBase
    {
        #region X -- X取值范围

        /// <summary>
        /// X取值范围
        /// </summary>
        public DanceRangeFloat X
        {
            get { return (DanceRangeFloat)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        /// <summary>
        /// X取值范围
        /// </summary>
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(DanceRangeFloat), typeof(DanceParticleControllerBox), new PropertyMetadata(new DanceRangeFloat(10, 20)));

        #endregion

        #region Y -- Y取值范围

        /// <summary>
        /// Y取值范围
        /// </summary>
        public DanceRangeFloat Y
        {
            get { return (DanceRangeFloat)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        /// <summary>
        /// Y取值范围
        /// </summary>
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(DanceRangeFloat), typeof(DanceParticleControllerBox), new PropertyMetadata(new DanceRangeFloat(10, 20)));



        #endregion

        #region Z -- Z取值范围

        /// <summary>
        /// Z取值范围
        /// </summary>
        public DanceRangeFloat Z
        {
            get { return (DanceRangeFloat)GetValue(ZProperty); }
            set { SetValue(ZProperty, value); }
        }

        /// <summary>
        /// Z取值范围
        /// </summary>
        public static readonly DependencyProperty ZProperty =
            DependencyProperty.Register("Z", typeof(DanceRangeFloat), typeof(DanceParticleControllerBox), new PropertyMetadata(new DanceRangeFloat(10, 20)));

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
                particle.X = this.Random.NextFloat(this.X.MinValue, this.X.MaxValue);
                particle.Y = this.Random.NextFloat(this.Y.MinValue, this.Y.MaxValue);
                particle.Z = this.Random.NextFloat(this.Z.MinValue, this.Z.MaxValue);

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
