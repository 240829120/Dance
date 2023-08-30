using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Dance.Maui
{
    /// <summary>
    /// 粒子面板
    /// </summary>
    [ContentProperty(nameof(Layers))]
    public class DanceParticlePanel : SKCanvasView
    {
        public DanceParticlePanel()
        {
            this.Layers = new();

            this.PaintSurface += DanceParticlePanel_PaintSurface;

            this.Loaded += DanceParticlePanel_Loaded;
            this.Unloaded += DanceParticlePanel_Unloaded;
        }

        /// <summary>
        /// 上次渲染时间
        /// </summary>
        private TimeSpan LastRenderingTime;

        /// <summary>
        /// 瞬时FPS
        /// </summary>
        private float FPS;

        /// <summary>
        /// 调试画刷
        /// </summary>
        private static readonly SKPaint DEBUG_PAINT = new() { IsAntialias = true, Color = SKColors.Red, Style = SKPaintStyle.Fill };

        /// <summary>
        /// 运行动画
        /// </summary>
        private Animation? RunningAnimation;

        /// <summary>
        /// 运行动画时间监视器
        /// </summary>
        private Stopwatch? RunningAnimationStopwatch;

        #region Layers -- 粒子层集合

        /// <summary>
        /// 粒子层集合
        /// </summary>
        public List<DanceParticleLayer> Layers
        {
            get { return (List<DanceParticleLayer>)GetValue(LayersProperty); }
            set { SetValue(LayersProperty, value); }
        }

        /// <summary>
        /// 粒子层集合
        /// </summary>
        public static readonly BindableProperty LayersProperty =
            BindableProperty.Create(nameof(Layers), typeof(List<DanceParticleLayer>), typeof(DanceParticleLayer), null);

        #endregion

        #region IsShowDebugInfo -- 是否显示调试信息

        /// <summary>
        /// 是否显示调试信息
        /// </summary>
        public bool IsShowDebugInfo
        {
            get { return (bool)GetValue(IsShowDebugInfoProperty); }
            set { SetValue(IsShowDebugInfoProperty, value); }
        }

        /// <summary>
        /// 粒子层集合
        /// </summary>
        public static readonly BindableProperty IsShowDebugInfoProperty =
            BindableProperty.Create(nameof(IsShowDebugInfo), typeof(bool), typeof(DanceParticleLayer), null);

        #endregion

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceParticlePanel_Loaded(object? sender, EventArgs e)
        {
            this.RunningAnimation = new(v =>
            {
                if (this.IsVisible && this.RunningAnimationStopwatch != null)
                {
                    TimeSpan dt = this.RunningAnimationStopwatch.Elapsed - this.LastRenderingTime;
                    this.LastRenderingTime = this.RunningAnimationStopwatch.Elapsed;
                    this.FPS = (float)(1f / dt.TotalSeconds);

                    foreach (DanceParticleLayer layer in this.Layers)
                    {
                        layer.Destory(dt);
                        layer.Step(dt);
                        layer.Generate(dt);
                    }

                    this.InvalidateSurface();
                }

            }, 0, double.MaxValue, null, null);

            this.RunningAnimationStopwatch = new Stopwatch();
            this.RunningAnimationStopwatch?.Restart();
            this.RunningAnimation.Commit(this, "RunningAnimation", 16, uint.MaxValue);
        }

        /// <summary>
        /// 卸载
        /// </summary>
        private void DanceParticlePanel_Unloaded(object? sender, EventArgs e)
        {
            this.RunningAnimation?.Dispose();
            this.RunningAnimation = null;
            this.RunningAnimationStopwatch?.Stop();
            this.RunningAnimationStopwatch = null;
        }

        /// <summary>
        /// 绘制
        /// </summary>
        private void DanceParticlePanel_PaintSurface(object? sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
        {
            if (!this.IsVisible)
                return;

            e.Surface.Canvas.Clear();
            foreach (DanceParticleLayer layer in this.Layers)
            {
                layer.Draw(e.Info.Size, e.Surface.Canvas);
            }

            if (this.IsShowDebugInfo)
            {
                this.DrawDebugInfo(e.Surface.Canvas);
            }
        }

        /// <summary>
        /// 绘制调试信息
        /// </summary>
        /// <param name="canvas"></param>
        private void DrawDebugInfo(SKCanvas canvas)
        {
            canvas.ResetMatrix();

            int count = 0;

            foreach (DanceParticleLayer layer in this.Layers)
            {
                foreach (IDanceParticleController controller in layer.Controllers)
                {
                    count += controller.GetParticleCount();
                }
            }

            canvas.DrawText($"Particle Count: {count}", 10, 20, DEBUG_PAINT);
            canvas.DrawText($"FPS : {(int)Math.Round(this.FPS)}", 10, 40, DEBUG_PAINT);
        }
    }
}
