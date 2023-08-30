using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子控制器
    /// </summary>
    public interface IDanceParticleController
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="dt">渲染时间</param>
        /// <returns>新生成的粒子</returns>
        IList<IDanceParticle> Generate(TimeSpan dt);

        /// <summary>
        /// 步骤
        /// </summary>
        /// <param name="dt">渲染时间</param>
        void Step(TimeSpan dt);

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="dt">渲染时间</param>
        void Destory(TimeSpan dt);

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="size">绘制区域</param>
        /// <param name="canvas">画布</param>
        void Draw(SKSize size, SKCanvas canvas);

        /// <summary>
        /// 获取粒子数量
        /// </summary>
        /// <returns>粒子数量</returns>
        int GetParticleCount();
    }
}
