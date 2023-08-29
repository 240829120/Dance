using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子生成器基类
    /// </summary>
    public abstract class DanceParticleGeneratorBase : DependencyObject, IDanceParticleGenerator
    {
        /// <summary>
        /// 随机数
        /// </summary>
        protected Random Random = new();

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>粒子</returns>
        public abstract IDanceParticle Generate();
    }
}
