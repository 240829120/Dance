using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Dance.Maui
{
    /// <summary>
    /// 粒子层
    /// </summary>
    [ContentProperty(nameof(Controllers))]
    public class DanceParticleLayer : BindableObject
    {
        public DanceParticleLayer()
        {
            this.Controllers = new();
        }

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
        public static readonly BindableProperty ControllersProperty =
            BindableProperty.Create(nameof(Controllers), typeof(List<DanceParticleControllerBase>), typeof(DanceParticleLayer), null);

        #endregion

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="dt">渲染时间</param>
        public void Generate(TimeSpan dt)
        {
            foreach (IDanceParticleController controller in this.Controllers)
            {
                controller.Generate(dt);
            }
        }

        /// <summary>
        /// 步骤
        /// </summary>
        /// <param name="dt">时间</param>
        public void Step(TimeSpan dt)
        {
            foreach (IDanceParticleController controller in this.Controllers)
            {
                controller.Step(dt);
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="dt">渲染时间</param>
        public void Destory(TimeSpan dt)
        {
            foreach (IDanceParticleController controller in this.Controllers)
            {
                controller.Destory(dt);
            }
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="size">绘制区域</param>
        /// <param name="canvas">画布</param>
        public void Draw(SKSize size, SKCanvas canvas)
        {
            foreach (IDanceParticleController controller in this.Controllers)
            {
                controller.Draw(size, canvas);
            }
        }
    }
}
