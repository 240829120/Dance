using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// Bool类型动画处理扩展
    /// </summary>
    public static class DanceBoolAnimationProviderExpansion
    {
        /// <summary>
        /// Bool类型动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="property">属性</param>
        /// <param name="easing">过度函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder Bool(this IDanceAnimationBuilder builder, DependencyProperty property, IEasingFunction? easing, params DanceAnimationKeyFrame<bool>[] keyFrames)
        {
            return builder.Bool(property.Name, easing, keyFrames);
        }

        /// <summary>
        /// Bool类型动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="propertyPath">属性路径</param>
        /// <param name="easing">过度函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder Bool(this IDanceAnimationBuilder builder, string propertyPath, IEasingFunction? easing, params DanceAnimationKeyFrame<bool>[] keyFrames)
        {
            if (!builder.PropertyDic.TryGetValue(propertyPath, out IDanceAnimationBuilderProperty? value))
            {
                value = new DanceBoolAnimationBuilder(propertyPath);
                builder.PropertyDic.Add(propertyPath, value);
            }

            if (value is not DanceBoolAnimationBuilder item)
            {
                throw new Exception($"Create animation error. type: bool");
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
