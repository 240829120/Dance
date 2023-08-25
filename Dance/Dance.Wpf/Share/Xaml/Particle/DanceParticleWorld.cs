using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子世界
    /// </summary>
    public class DanceParticleWorld
    {
        /// <summary>
        /// 粒子集合
        /// </summary>
        public List<IDanceParticle> Particles { get; } = new();

        public void Step(TimeSpan dt)
        {

        }
    }
}
