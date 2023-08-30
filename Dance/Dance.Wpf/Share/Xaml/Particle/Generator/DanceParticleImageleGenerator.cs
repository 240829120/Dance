using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 图片粒子生成器
    /// </summary>
    public class DanceParticleImageleGenerator : DanceParticleGeneratorBase
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

        #region Source -- 源

        /// <summary>
        /// 源
        /// </summary>
        private SKImage? SKImage;

        /// <summary>
        /// 源
        /// </summary>
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// 源
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(DanceParticleImageleGenerator), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceParticleImageleGenerator generator)
                    return;

                if (e.NewValue is not Uri uri)
                    return;

                using FileStream fs = new(uri.LocalPath, FileMode.Open, FileAccess.Read);
                using SKBitmap bmp = SKBitmap.Decode(fs);
                generator.SKImage = SKImage.FromBitmap(bmp);
            })));

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>粒子</returns>
        public override IDanceParticle Generate()
        {
            DanceParticleImage particle = new()
            {
                Width = this.Random.NextFloat(this.Width.MinValue, this.Width.MaxValue),
                Height = this.Random.NextFloat(this.Height.MinValue, this.Height.MaxValue),
                Source = this.SKImage
            };

            return particle;
        }
    }
}
