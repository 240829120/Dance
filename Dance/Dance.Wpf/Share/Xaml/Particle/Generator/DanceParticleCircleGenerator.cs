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

        #region Colors -- 颜色集合

        /// <summary>
        /// 颜色集合
        /// </summary>
        public DanceColorCollection Colors
        {
            get { return (DanceColorCollection)GetValue(ColorsProperty); }
            set { SetValue(ColorsProperty, value); }
        }

        /// <summary>
        /// 颜色集合
        /// </summary>
        public static readonly DependencyProperty ColorsProperty =
            DependencyProperty.Register("Colors", typeof(DanceColorCollection), typeof(DanceParticleCircleGenerator), new PropertyMetadata(null));

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

            if (this.Colors != null && this.Colors.Count > 0)
            {
                DanceColor color = this.Colors[this.Random.Next(0, this.Colors.Count)];
                particle.Paint.Color = new((byte)(255 * color.R), (byte)(255 * color.G), (byte)(255 * color.B), (byte)(255 * color.A));
            }

            return particle;
        }
    }
}
