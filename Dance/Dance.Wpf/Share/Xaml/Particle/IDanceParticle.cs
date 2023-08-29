using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子
    /// </summary>
    public interface IDanceParticle : IDisposable
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime GeneratTime { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        TimeSpan Duration { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        float X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        float Y { get; set; }

        /// <summary>
        /// X轴平移速度
        /// </summary>
        float TranslateSpeedX { get; set; }

        /// <summary>
        /// Y轴平移速度
        /// </summary>
        float TranslateSpeedY { get; set; }

        /// <summary>
        /// X轴旋转
        /// </summary>
        float RotateX { get; set; }

        /// <summary>
        /// Y轴旋转
        /// </summary>
        float RotateY { get; set; }

        /// <summary>
        /// Z轴旋转
        /// </summary>
        float RotateZ { get; set; }

        /// <summary>
        /// X轴旋转速度
        /// </summary>
        float RotateSpeedX { get; set; }

        /// <summary>
        /// Y轴旋转速度
        /// </summary>
        float RotateSpeedY { get; set; }

        /// <summary>
        /// Z轴旋转速度
        /// </summary>
        float RotateSpeedZ { get; set; }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="size">可绘制区域</param>
        /// <param name="canvas">绘制上下文</param>
        void Draw(SKSize size, SKCanvas canvas);
    }
}