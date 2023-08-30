using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 矩形粒子生成器
    /// </summary>
    public class DanceParticleRectangleGenerator : DanceParticleGeneratorBase
    {
        #region Width -- 宽度

        /// <summary>
        /// 宽度
        /// </summary>
        public DanceRangeFloat Width
        {
            get { return (DanceRangeFloat)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(DanceRangeFloat), typeof(DanceParticleRectangleGenerator), new PropertyMetadata(new DanceRangeFloat(10, 20)));

        #endregion

        #region Height -- 高度

        /// <summary>
        /// 高度
        /// </summary>
        public DanceRangeFloat Height
        {
            get { return (DanceRangeFloat)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(DanceRangeFloat), typeof(DanceParticleRectangleGenerator), new PropertyMetadata(new DanceRangeFloat(10, 20)));

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
            DependencyProperty.Register("Colors", typeof(DanceColorCollection), typeof(DanceParticleRectangleGenerator), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>粒子</returns>
        public override IDanceParticle Generate()
        {
            DanceParticleRectangle particle = new()
            {
                Width = this.Random.NextFloat(this.Width.MinValue, this.Width.MaxValue),
                Height = this.Random.NextFloat(this.Height.MinValue, this.Height.MaxValue)
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
