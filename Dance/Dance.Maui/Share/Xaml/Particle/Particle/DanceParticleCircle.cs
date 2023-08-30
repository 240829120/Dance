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
    /// 圆形粒子
    /// </summary>
    public class DanceParticleCircle : DanceParticleBase
    {
        /// <summary>
        /// 半径
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="size">可绘制区域</param>
        /// <param name="canvas">绘制上下文</param>
        public override void Draw(SKSize size, SKCanvas canvas)
        {
            canvas.DrawCircle(0, 0, this.Radius, this.Paint);
        }
    }
}