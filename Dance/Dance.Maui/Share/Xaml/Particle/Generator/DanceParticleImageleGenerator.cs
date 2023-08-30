using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Dance.Maui
{
    /// <summary>
    /// 图片粒子生成器
    /// </summary>
    [ContentProperty(nameof(Images))]
    public class DanceParticleImageleGenerator : DanceParticleGeneratorBase
    {
        public DanceParticleImageleGenerator()
        {
            this.Images = new();
        }

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
        public static readonly BindableProperty WidthProperty =
            BindableProperty.Create(nameof(Width), typeof(DanceRangeFloat), typeof(DanceParticleImageleGenerator), new DanceRangeFloat(10, 20));

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
        public static readonly BindableProperty HeightProperty =
            BindableProperty.Create(nameof(Height), typeof(DanceRangeFloat), typeof(DanceParticleImageleGenerator), new DanceRangeFloat(10, 20));

        #endregion

        #region Images -- 图片集合

        /// <summary>
        /// 图片集合
        /// </summary>
        public DanceImageCollection Images
        {
            get { return (DanceImageCollection)GetValue(ImagesProperty); }
            set { SetValue(ImagesProperty, value); }
        }

        /// <summary>
        /// 图片集合
        /// </summary>
        public static readonly BindableProperty ImagesProperty =
            BindableProperty.Create(nameof(Images), typeof(DanceImageCollection), typeof(DanceParticleImageleGenerator), null);

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
                Height = this.Random.NextFloat(this.Height.MinValue, this.Height.MaxValue)
            };

            if (this.Images != null && this.Images.Count > 0)
            {
                DanceImage img = this.Images[this.Random.Next(0, this.Images.Count)];
                if (img.Tag is not SKImage)
                {
                    img.Tag = img.Uri == null ? null : CreateSKImage(img.Uri);
                }

                particle.Source = img.Tag as SKImage;
            }

            return particle;
        }

        /// <summary>
        /// 创建SKImage
        /// </summary>
        /// <param name="uri">地址</param>
        /// <returns>SKImage</returns>
        private static SKImage? CreateSKImage(Uri uri)
        {
            string str = uri.ToString().Trim().ToUpper();

            if (str.StartsWith("FILE"))
            {
                using FileStream fs = new(uri.LocalPath, FileMode.Open, FileAccess.Read);
                using SKBitmap bmp = SKBitmap.Decode(fs);
                return SKImage.FromBitmap(bmp);
            }
            else if (str.StartsWith("HTTP"))
            {
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
