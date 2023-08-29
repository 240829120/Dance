using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 圆形粒子生成器
    /// </summary>
    public class DanceParticleCircleGenerator : DanceParticleGeneratorBase
    {
        #region Radius -- 半径

        /// <summary>
        /// 半径
        /// </summary>
        public DanceRangeFloat Radius
        {
            get { return (DanceRangeFloat)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        /// <summary>
        /// 半径
        /// </summary>
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(DanceRangeFloat), typeof(DanceParticleCircleGenerator), new PropertyMetadata(new DanceRangeFloat(10, 20)));

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>粒子</returns>
        public override IDanceParticle Generate()
        {
            DanceParticleCircle particle = new()
            {
                Radius = this.Random.NextFloat(this.Radius.MinValue, this.Radius.MaxValue)
            };

            return particle;
        }
    }
}
