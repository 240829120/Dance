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
        public List<DanceParticleImageDefine> Images
        {
            get { return (List<DanceParticleImageDefine>)GetValue(ImagesProperty); }
            set { SetValue(ImagesProperty, value); }
        }

        /// <summary>
        /// 图片集合
        /// </summary>
        public static readonly BindableProperty ImagesProperty =
            BindableProperty.Create(nameof(Images), typeof(List<DanceParticleImageDefine>), typeof(DanceParticleImageleGenerator), null);

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
                    img.SKImage = CreateSKImage(img);
                }

                particle.Source = img.SKImage;
            }

            return particle;
        }

        /// <summary>
        /// 创建SKImage
        /// </summary>
        /// <param name="define">图片粒子定义</param>
        /// <returns>SKImage</returns>
        private static SKImage? CreateSKImage(DanceParticleImageDefine define)
        {
            try
            {
                string str = define.Uri.ToString().Trim().ToUpper();

                if (str.StartsWith("FILE"))
                {
                    using FileStream fs = new(define.Uri.LocalPath, FileMode.Open, FileAccess.Read);
                    using SKBitmap bmp = SKBitmap.Decode(fs);
                    return SKImage.FromBitmap(bmp);
                }
                else if (str.StartsWith("HTTP"))
                {
                    HttpClient client = new();
                    using Stream stream = client.GetAsync(define.Uri).Result.Content.ReadAsStream();
                    using SKBitmap bmp = SKBitmap.Decode(stream);
                    return SKImage.FromBitmap(bmp);
                }
                else
                {
                    if (Application.Current == null)
                        return null;

                    Assembly? assembly;
                    if (string.IsNullOrWhiteSpace(define.AssemblyName))
                    {
                        assembly = Assembly.GetEntryAssembly();
                    }
                    else
                    {
                        assembly = Assembly.Load(define.AssemblyName);
                    }

                    if (assembly == null)
                        return null;

                    using Stream? stream = assembly.GetManifestResourceStream(define.Uri.ToString());
                    if (stream == null)
                        return null;

                    using SKBitmap bmp = SKBitmap.Decode(stream);
                    return SKImage.FromBitmap(bmp);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return null;
        }
    }
}
