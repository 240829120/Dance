using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 矩形粒子
    /// </summary>
    public class DanceRectangleParticle : DanceParticleBase
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
            canvas.DrawRect(this.Position.X, this.Position.Y, this.Width, this.Height, this.Paint);
        }
    }
}
