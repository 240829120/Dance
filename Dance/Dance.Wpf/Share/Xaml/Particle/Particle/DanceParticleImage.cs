using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using SkiaSharp;

namespace Dance.Wpf
{
    /// <summary>
    /// 图片粒子
    /// </summary>
    public class DanceParticleImage : DanceParticleBase
    {
        /// <summary>
        /// 源
        /// </summary>
        public SKImage? Source { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="size">可绘制区域</param>
        /// <param name="canvas">绘制上下文</param>
        public override void Draw(SKSize size, SKCanvas canvas)
        {
            float left = this.X - this.Width / 2f;
            float right = this.X + this.Width / 2f;
            float top = this.Y - this.Height / 2f;
            float bottom = this.Y + this.Height / 2f;

            canvas.DrawImage(this.Source, new SKRect(left, top, right, bottom));
        }
    }
}