using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// Thickness动画处理器扩展
    /// </summary>
    public static class DanceThicknessAnimationProviderExpansion
    {
        /// <summary>
        /// 处理器
        /// </summary>
        private readonly static DanceThicknessAnimationProvider Provider = new();

        /// <summary>
        /// Thickness动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="property">依赖属性</param>
        /// <param name="easing">缓动函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder Thickness(this IDanceAnimationBuilder builder, BindableProperty property, Easing? easing, params DanceAnimationKeyFrame<Thickness>[] keyFrames)
        {
            Provider.Create(builder, property, easing, keyFrames);
            return builder;
        }
    }
}
