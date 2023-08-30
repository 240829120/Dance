using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子
    /// </summary>
    public abstract class DanceParticleBase : DanceObject, IDanceParticle
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime GeneratTime { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// X轴平移速度
        /// </summary>
        public float TranslateSpeedX { get; set; }

        /// <summary>
        /// Y轴平移速度
        /// </summary>
        public float TranslateSpeedY { get; set; }

        /// <summary>
        /// X轴旋转
        /// </summary>
        public float RotateX { get; set; }

        /// <summary>
        /// Y轴旋转
        /// </summary>
        public float RotateY { get; set; }

        /// <summary>
        /// Z轴旋转
        /// </summary>
        public float RotateZ { get; set; }

        /// <summary>
        /// X轴旋转速度
        /// </summary>
        public float RotateSpeedX { get; set; }

        /// <summary>
        /// Y轴旋转速度
        /// </summary>
        public float RotateSpeedY { get; set; }

        /// <summary>
        /// Z轴旋转速度
        /// </summary>
        public float RotateSpeedZ { get; set; }

        /// <summary>
        /// 画笔
        /// </summary>
        public SKPaint Paint { get; set; } = new SKPaint() { IsAntialias = true, Color = SKColors.Red, Style = SKPaintStyle.Fill };

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="size">可绘制区域</param>
        /// <param name="canvas">绘制上下文</param>
        public abstract void Draw(SKSize size, SKCanvas canvas);

        /// <summary>
        /// 更新透明度
        /// </summary>
        /// <param name="opacityChangeTime">透明度改变时间点</param>
        public void UpdatePaintAlpha(TimeSpan opacityChangeTime)
        {
            TimeSpan dt = this.Duration - (DateTime.Now - this.GeneratTime);
            if (dt >= opacityChangeTime)
                return;

            byte a = (byte)(255 * (dt.Ticks / (double)opacityChangeTime.Ticks));
            this.Paint.Color = new SKColor(this.Paint.Color.Red, this.Paint.Color.Green, this.Paint.Color.Blue, a);
        }
    }
}
