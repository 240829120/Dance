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
    /// 粒子面板
    /// </summary>
    public class DanceParticlePanel : Control
    {
        public DanceParticlePanel()
        {
            this.Loaded += DanceParticlePanel_Loaded;
            this.Unloaded += DanceParticlePanel_Unloaded;
        }

        /// <summary>
        /// 世界
        /// </summary>
        internal DanceParticleWorld World = new();

        #region Generator -- 构建器

        /// <summary>
        /// 构建器
        /// </summary>
        public IDanceParticleGenerator Generator
        {
            get { return (IDanceParticleGenerator)GetValue(GeneratorProperty); }
            set { SetValue(GeneratorProperty, value); }
        }

        /// <summary>
        /// 构建器
        /// </summary>
        public static readonly DependencyProperty GeneratorProperty =
            DependencyProperty.Register("Generator", typeof(IDanceParticleGenerator), typeof(DanceParticlePanel), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="drawingContext">绘制上下文</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.Particles == null || this.Particles.Count == 0)
                return;

            foreach (IDanceParticle particle in this.Particles)
            {
                particle.Draw(this.DesiredSize, drawingContext);
            }
        }

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

        }

    }
}
