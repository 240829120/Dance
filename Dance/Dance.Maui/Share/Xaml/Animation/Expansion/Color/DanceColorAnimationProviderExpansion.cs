using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// Color动画处理器扩展
    /// </summary>
    public static class DanceColorAnimationProviderExpansion
    {
        /// <summary>
        /// 处理器
        /// </summary>
        private readonly static DanceColorAnimationProvider Provider = new();

        /// <summary>
        /// Color动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="property">依赖属性</param>
        /// <param name="easing">缓动函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder Color(this IDanceAnimationBuilder builder, BindableProperty property, Easing? easing, params DanceAnimationKeyFrame<Color>[] keyFrames)
        {
            Provider.Create(builder, property, easing, keyFrames);
            return builder;
        }
    }
}
