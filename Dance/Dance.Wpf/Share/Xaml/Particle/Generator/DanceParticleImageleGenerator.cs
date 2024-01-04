using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Resources;

namespace Dance.Wpf
{
    /// <summary>
    /// 图片粒子生成器
    /// </summary>
    [ContentProperty(nameof(Images))]
    public class DanceParticleImageleGenerator : DanceParticleGeneratorBase
    {
        public DanceParticleImageleGenerator()
        {
            this.Images = [];
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
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(DanceRangeFloat), typeof(DanceParticleImageleGenerator), new PropertyMetadata(new DanceRangeFloat(10, 20)));

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
            DependencyProperty.Register("Height", typeof(DanceRangeFloat), typeof(DanceParticleImageleGenerator), new PropertyMetadata(new DanceRangeFloat(10, 20)));

        #endregion

        #region Images -- 图片集合

        /// <summary>
        /// 图片集合
        /// </summary>
        public List<DanceParticleImageDefine> Images
        {
            get { return (List<DanceParticleImageDefine>)GetValue(ImagesProperty); }
            set { SetValue(ImagesProperty, value); }
        }

        /// <summary>
        /// 图片集合
        /// </summary>
        public static readonly DependencyProperty ImagesProperty =
            DependencyProperty.Register("Images", typeof(List<DanceParticleImageDefine>), typeof(DanceParticleImageleGenerator), new PropertyMetadata(null));

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
                DanceParticleImageDefine img = this.Images[this.Random.Next(0, this.Images.Count)];
                if (img.Uri != null && img.SKImage == null)
                {
                    img.SKImage = CreateSKImage(img.Uri);
                }

                particle.Source = img.SKImage;
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
            try
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
                    HttpClient client = new();
                    using Stream stream = client.GetAsync(uri).Result.Content.ReadAsStream();
                    using SKBitmap bmp = SKBitmap.Decode(stream);
                    return SKImage.FromBitmap(bmp);
                }
                else
                {
                    StreamResourceInfo info = Application.GetResourceStream(uri);
                    using SKBitmap bmp = SKBitmap.Decode(info.Stream);
                    info.Stream.Dispose();
                    return SKImage.FromBitmap(bmp);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return null;
            }
        }
    }
}
