using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Maui
{
    /// <summary>
    /// 矩形粒子
    /// </summary>
    public class DanceParticleRectangle : DanceParticleBase
    {
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
            canvas.DrawRect(0, 0, this.Width, this.Height, this.Paint);
        }
    }
}
