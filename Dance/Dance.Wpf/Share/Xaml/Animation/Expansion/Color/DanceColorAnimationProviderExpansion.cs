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
    /// Color类型动画处理扩展
    /// </summary>
    public static class DanceColorAnimationProviderExpansion
    {
        /// <summary>
        /// Color类型动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="property">属性</param>
        /// <param name="easing">过度函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder Color(this IDanceAnimationBuilder builder, DependencyProperty property, IEasingFunction? easing, params DanceAnimationKeyFrame<Color>[] keyFrames)
        {
            return builder.Color(property.Name, easing, keyFrames);
        }

        /// <summary>
        /// Color类型动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="propertyPath">属性路径</param>
        /// <param name="easing">过度函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder Color(this IDanceAnimationBuilder builder, string propertyPath, IEasingFunction? easing, params DanceAnimationKeyFrame<Color>[] keyFrames)
        {
            if (!builder.PropertyDic.TryGetValue(propertyPath, out IDanceAnimationBuilderProperty? value))
            {
                value = new DanceColorAnimationBuilder(propertyPath);
                builder.PropertyDic.Add(propertyPath, value);
            }

            if (value is not DanceColorAnimationBuilder item)
            {
                throw new Exception($"Create animation error. type: Color");
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
