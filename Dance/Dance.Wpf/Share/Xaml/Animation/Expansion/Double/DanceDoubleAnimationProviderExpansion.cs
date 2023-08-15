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
    /// Double类型动画处理扩展
    /// </summary>
    public static class DanceDoubleAnimationProviderExpansion
    {
        /// <summary>
        /// Double类型动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="property">属性</param>
        /// <param name="easing">过度函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder Double(this IDanceAnimationBuilder builder, DependencyProperty property, IEasingFunction? easing, params DanceAnimationKeyFrame<double>[] keyFrames)
        {
            return builder.Double(property.Name, easing, keyFrames);
        }

        /// <summary>
        /// Double类型动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="propertyPath">属性路径</param>
        /// <param name="easing">过度函数</param>
        /// <param name="keyFrames">关键帧</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder Double(this IDanceAnimationBuilder builder, string propertyPath, IEasingFunction? easing, params DanceAnimationKeyFrame<double>[] keyFrames)
        {
            if (!builder.PropertyDic.TryGetValue(propertyPath, out IDanceAnimationBuilderProperty? value))
            {
                value = new DanceDoubleAnimationBuilder(propertyPath);
                builder.PropertyDic.Add(propertyPath, value);
            }

            if (value is not DanceDoubleAnimationBuilder item)
            {
                throw new Exception($"Create animation error. type: double");
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
