using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 动画构建器
    /// </summary>
    /// <param name="element">元素</param>
    public class DanceAnimationKeyFrameBuilder(FrameworkElement element) : IDanceAnimationBuilder
    {
        /// <summary>
        /// 元素
        /// </summary>
        public FrameworkElement Element { get; } = element;

        /// <summary>
        /// 属性构建器集合
        /// </summary>
        public Dictionary<string, IDanceAnimationBuilderProperty> PropertyDic { get; } = [];

        /// <summary>
        /// 动画
        /// </summary>
        public Storyboard Storyboard { get; } = new();

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>是否被取消</returns>
        public void Commit(string name)
        {
            foreach (var item in this.PropertyDic)
            {
                Timeline timeline = item.Value.Build();

                this.Storyboard.Children.Add(timeline);
            }

            this.Storyboard.Name = name;

            DanceAnimationManager.AddAnimation(this.Element, name, this.Storyboard);
            this.Storyboard.Completed += (s, e) =>
            {
                DanceAnimationManager.RemoveAnimation(this.Element, name);
            };

            this.Storyboard.Begin(this.Element);
        }
    }
}
