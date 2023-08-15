using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 动画信息
    /// </summary>
    public class DanceAnimationInfo
    {
        /// <summary>
        /// 动画信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="animation">动画</param>
        public DanceAnimationInfo(string name, Storyboard animation)
        {
            this.Name = name;
            this.Animation = animation;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 动画
        /// </summary>
        public Storyboard Animation { get; private set; }
    }
}
