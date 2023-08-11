using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 动画处理器基类
    /// </summary>
    public abstract class DanceAnimationProviderBase<T>
    {
        /// <summary>
        /// Double动画
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="property">依赖属性</param>
        /// <param name="easing">缓动函数</param>
        /// <param name="keyFrames">关键帧</param>
        public virtual void Create(IDanceAnimationBuilder builder, BindableProperty property, Easing? easing, params DanceAnimationKeyFrame<T>[] keyFrames)
        {
            if (!builder.PropertyDic.TryGetValue(property, out IDanceAnimationBuilderProperty? value))
            {
                value = new DanceAnimationBuilderProperty<T>(property);
                builder.PropertyDic.Add(property, value);
            }
            if (value is not DanceAnimationBuilderProperty<T> item)
            {
                throw new Exception($"Create animation error. type: {this.GetType()}");
            }

            item.BindableProperty = property;
            item.Transform = t => Transform(builder, item, t);
            item.Finished = () => Finished(builder, item);
            item.Easing = easing;
            foreach (var keyFrame in keyFrames)
            {
                item.KeyFrames.Add(keyFrame.Time, keyFrame);
            }
        }

        /// <summary>
        /// 变换方法
        /// </summary>
        /// <param name="builder">构建器</param>
        /// <param name="item">构建项</param>
        /// <param name="t">时间</param>
        /// <returns>值</returns>
        protected T Transform(IDanceAnimationBuilder builder, DanceAnimationBuilderProperty<T> item, double t)
        {
            if (builder.Duration == null || item.KeyFrames.Count == 0)
                return (T)item.BindableProperty.DefaultValue;

            TimeSpan time = t * builder.Duration.Value;

            DanceAnimationKeyFrame<T> to = item.KeyFrames.FirstOrDefault(p => time <= p.Key).Value ?? item.KeyFrames.Last().Value;
            DanceAnimationKeyFrame<T> from = item.KeyFrames.LastOrDefault(p => time >= p.Key).Value ?? to;

            if (from.Value is not T fromValue)
            {
                return (T)item.BindableProperty.DefaultValue;
            }
            if (to.Value is not T toValue)
            {
                return (T)item.BindableProperty.DefaultValue;
            }

            if (from.Time == to.Time)
            {
                return toValue;
            }

            double tFrom = from.Time / builder.Duration.Value;
            double tPosition = t - tFrom;
            double tPositionIn = tPosition / ((to.Time - from.Time) / builder.Duration.Value);

            return this.Transform(builder, tPositionIn, fromValue, toValue);
        }

        /// <summary>
        /// 转化
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="t">值所占百分比</param>
        /// <param name="from">开始值</param>
        /// <param name="to">结束值</param>
        /// <returns>转化后的值</returns>
        protected abstract T Transform(IDanceAnimationBuilder builder, double t, T from, T to);

        /// <summary>
        /// 动画结束
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="item">构建项</param>
        protected virtual void Finished(IDanceAnimationBuilder builder, DanceAnimationBuilderProperty<T> item)
        {
            DanceAnimationKeyFrame<T>? frame = item.KeyFrames.LastOrDefault().Value;
            if (frame == null)
                return;

            builder.Element.SetValue(item.BindableProperty, frame.Value);
        }
    }
}
