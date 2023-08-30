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
    [ContentProperty(nameof(Layers))]
    public class DanceParticlePanel : SkiaSharp.Views.WPF.SKElement
    {
        public DanceParticlePanel()
        {
            this.Layers = new();
            this.IsHitTestVisible = false;

            this.PaintSurface += DanceParticlePanel_PaintSurface;

            this.Loaded += DanceParticlePanel_Loaded;
            this.Unloaded += DanceParticlePanel_Unloaded;
        }

        /// <summary>
        /// 上次渲染时间
        /// </summary>
        private TimeSpan LastRenderingTime;

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
        public static readonly DependencyProperty LayersProperty =
            DependencyProperty.Register("Layers", typeof(List<DanceParticleLayer>), typeof(DanceParticlePanel), new PropertyMetadata(null));

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
            if (!this.IsVisible)
                return;

            if (e is not RenderingEventArgs args)
                return;

            TimeSpan dt = args.RenderingTime - this.LastRenderingTime;
            this.LastRenderingTime = args.RenderingTime;

            foreach (DanceParticleLayer layer in this.Layers)
            {
                layer.Destory(dt);
                layer.Step(dt);
                layer.Generate(dt);
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
            foreach (DanceParticleLayer layer in this.Layers)
            {
                layer.Draw(e.Info.Size, e.Surface.Canvas);
            }
        }
    }
}
