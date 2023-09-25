using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子面板
    /// </summary>
    [ContentProperty(nameof(Controllers))]
    public class DanceParticlePanel : SkiaSharp.Views.WPF.SKElement
    {
        public DanceParticlePanel()
        {
            this.Controllers = new();

            this.PaintSurface += DanceParticlePanel_PaintSurface;

            this.Loaded += DanceParticlePanel_Loaded;
            this.Unloaded += DanceParticlePanel_Unloaded;
        }

        /// <summary>
        /// 上次渲染时间
        /// </summary>
        private TimeSpan? LastRenderingTime;

        /// <summary>
        /// 瞬时FPS
        /// </summary>
        private float FPS;

        /// <summary>
        /// 调试画刷
        /// </summary>
        private static readonly SKPaint DEBUG_PAINT = new() { IsAntialias = true, Color = SKColors.Red, Style = SKPaintStyle.Fill };

        #region Controllers -- 粒子控制器

        /// <summary>
        /// 粒子控制器
        /// </summary>
        public List<DanceParticleControllerBase> Controllers
        {
            get { return (List<DanceParticleControllerBase>)GetValue(ControllersProperty); }
            set { SetValue(ControllersProperty, value); }
        }

        /// <summary>
        /// 粒子控制器
        /// </summary>
        public static readonly DependencyProperty ControllersProperty =
            DependencyProperty.Register("Controllers", typeof(List<DanceParticleControllerBase>), typeof(DanceParticlePanel), new PropertyMetadata(null));

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
        /// 是否显示调试信息
        /// </summary>
        public static readonly DependencyProperty IsShowDebugInfoProperty =
            DependencyProperty.Register("IsShowDebugInfo", typeof(bool), typeof(DanceParticlePanel), new PropertyMetadata(false));

        #endregion

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceParticlePanel_Loaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        /// <summary>
        /// 卸载
        /// </summary>
        private void DanceParticlePanel_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            if (!this.IsVisible || e is not RenderingEventArgs args)
            {
                this.LastRenderingTime = null;
                return;
            }

            if (this.LastRenderingTime == null)
            {
                this.LastRenderingTime = args.RenderingTime;
                return;
            }

            TimeSpan dt = args.RenderingTime - this.LastRenderingTime.Value;
            this.LastRenderingTime = args.RenderingTime;
            this.FPS = (float)(1f / dt.TotalSeconds);

            foreach (IDanceParticleController controller in this.Controllers)
            {
                controller.Destory(dt);
                controller.Step(dt);
                controller.Generate(dt);
            }

            this.InvalidateVisual();
        }

        /// <summary>
        /// 绘制
        /// </summary>
        private void DanceParticlePanel_PaintSurface(object? sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            if (!this.IsVisible)
                return;

            e.Surface.Canvas.Clear();
            foreach (IDanceParticleController controller in this.Controllers)
            {
                controller.Draw(e.Info.Size, e.Surface.Canvas);
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
            foreach (IDanceParticleController controller in this.Controllers)
            {
                count += controller.GetParticleCount();
            }

            canvas.DrawText($"Particle Count: {count}", 10, 20, DEBUG_PAINT);
            canvas.DrawText($"FPS : {(int)Math.Round(this.FPS)}", 10, 40, DEBUG_PAINT);
        }
    }
}
