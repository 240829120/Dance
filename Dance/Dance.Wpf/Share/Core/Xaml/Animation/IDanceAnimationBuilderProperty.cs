using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 动画构建属性
    /// </summary>
    public interface IDanceAnimationBuilderProperty
    {
        /// <summary>
        /// 绑定属性
        /// </summary>
        string PropertyPath { get; set; }

        /// <summary>
        /// 缓动函数
        /// </summary>
        IEasingFunction? Easing { get; set; }

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns>建时间线</returns>
        Timeline Build();
    }
}
