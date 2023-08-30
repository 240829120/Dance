using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 粒子生成器
    /// </summary>
    public interface IDanceParticleGenerator
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>粒子</returns>
        IDanceParticle Generate();
    }
}
