using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 动画构建属性
    /// </summary>
    public interface IDanceAnimationBuilderProperty
    {
        /// <summary>
        /// 绑定属性
        /// </summary>
        BindableProperty BindableProperty { get; }

        /// <summary>
        /// 缓动函数
        /// </summary>
        Easing? Easing { get; }

        /// <summary>
        /// 变换方法
        /// </summary>
        Func<double, object?>? Transform { get; }

        /// <summary>
        /// 动画结束时触发
        /// </summary>
        Action? Finished { get; }

        /// <summary>
        /// 获取最大时间
        /// </summary>
        /// <returns>最大时间</returns>
        TimeSpan GetMaxTime();
    }
}
