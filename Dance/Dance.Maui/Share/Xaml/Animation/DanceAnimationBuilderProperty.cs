using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 动画构建器项
    /// </summary>
    public class DanceAnimationBuilderProperty<T> : IDanceAnimationBuilderProperty
    {
        /// <summary>
        /// 动画构建器项
        /// </summary>
        /// <param name="bindableProperty">关联属性</param>
        public DanceAnimationBuilderProperty(BindableProperty bindableProperty)
        {
            this.BindableProperty = bindableProperty;
        }

        /// <summary>
        /// 绑定属性
        /// </summary>
        public BindableProperty BindableProperty { get; set; }

        /// <summary>
        /// 关键帧
        /// </summary>
        public SortedList<TimeSpan, DanceAnimationKeyFrame<T>> KeyFrames { get; } = new();

        /// <summary>
        /// 变换方法
        /// </summary>
        public Func<double, object?>? Transform { get; set; }

        /// <summary>
        /// 动画结束时触发
        /// </summary>
        public Action? Finished { get; set; }

        /// <summary>
        /// 缓动函数
        /// </summary>
        public Easing? Easing { get; set; }

        /// <summary>
        /// 获取最大时间
        /// </summary>
        /// <returns>最大时间</returns>
        public TimeSpan GetMaxTime()
        {
            if (this.KeyFrames.Count == 0)
                return TimeSpan.Zero;

            return this.KeyFrames.Max(p => p.Value.Time);
        }
    }
}
