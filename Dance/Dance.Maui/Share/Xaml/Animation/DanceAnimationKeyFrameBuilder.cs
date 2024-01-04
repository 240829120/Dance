using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 动画构建器
    /// </summary>
    /// <param name="element">元素</param>
    public class DanceAnimationKeyFrameBuilder(VisualElement element) : IDanceAnimationBuilder
    {
        /// <summary>
        /// 元素
        /// </summary>
        public VisualElement Element { get; } = element;

        /// <summary>
        /// 属性构建器集合
        /// </summary>
        public Dictionary<BindableProperty, IDanceAnimationBuilderProperty> PropertyDic { get; } = [];

        /// <summary>
        /// 动画
        /// </summary>
        public Animation? Animation { get; private set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan? Duration { get; private set; }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="name">动画名称</param>
        /// <returns>是否被取消</returns>
        public Task<bool> Commit(string name)
        {
            Animation animation = [];
            TimeSpan max = TimeSpan.Zero;

            foreach (var item in this.PropertyDic)
            {
                if (item.Value.Transform == null)
                    continue;

                Animation part = new(t => this.Element.SetValue(item.Value.BindableProperty, item.Value.Transform(t)), 0, 1, item.Value.Easing, item.Value.Finished);

                animation.Add(0, 1, part);

                TimeSpan temp = item.Value.GetMaxTime();
                if (max < temp)
                {
                    max = temp;
                }
            }

            this.Animation = animation;
            this.Duration = max;

            if (this.Animation == null || this.Duration == null || this.Duration.Value == TimeSpan.Zero)
                return Task.FromResult(true);

            DanceAnimationManager.AddAnimation(this.Element, name, this.Animation);

            TaskCompletionSource<bool> task = new();
            this.Animation.Commit(this.Element, name, 16, (uint)this.Duration.Value.TotalMilliseconds, null, (v, c) =>
            {
                task.SetResult(c);
                DanceAnimationManager.RemoveAnimation(this.Element, name);
            });

            return task.Task;
        }
    }
}
