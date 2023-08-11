using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// String类型动画处理扩展
    /// </summary>
    public static class DanceStringAnimationProviderExpansion
    {
        /// <summary>
        /// String类型动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="property">属性</param>
        /// <param name="easing">过度函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder String(this IDanceAnimationBuilder builder, DependencyProperty property, IEasingFunction? easing, params DanceAnimationKeyFrame<string>[] keyFrames)
        {
            return builder.String(property.Name, easing, keyFrames);
        }

        /// <summary>
        /// String类型动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="propertyPath">属性路径</param>
        /// <param name="easing">过度函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder String(this IDanceAnimationBuilder builder, string propertyPath, IEasingFunction? easing, params DanceAnimationKeyFrame<string>[] keyFrames)
        {
            if (!builder.PropertyDic.TryGetValue(propertyPath, out IDanceAnimationBuilderProperty? value))
            {
                value = new DanceStringAnimationBuilder(propertyPath);
                builder.PropertyDic.Add(propertyPath, value);
            }

            if (value is not DanceStringAnimationBuilder item)
            {
                throw new Exception($"Create animation error. type: string");
            }

            item.Easing = easing;
            foreach (var keyFrame in keyFrames)
            {
                item.KeyFrames.Add(keyFrame.Time, keyFrame);
            }

            return builder;
        }
    }
}
