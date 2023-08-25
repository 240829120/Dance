using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子
    /// </summary>
    public abstract class DanceParticleBase : DanceObject, IDanceParticle
    {
        /// <summary>
        /// 位置
        /// </summary>
        public DancePoint Position { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public DanceVector Vector { get; set; }

        /// <summary>
        /// 角速度
        /// </summary>
        public DanceVector AngularVelocity { get; set; }


    }
}
