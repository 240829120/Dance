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
    public class DanceAnimationKeyFrameBuilder : IDanceAnimationBuilder
    {
        /// <summary>
        /// 动画关键帧构建器
        /// </summary>
        /// <param name="element">元素</param>
        public DanceAnimationKeyFrameBuilder(FrameworkElement element)
        {
            this.Element = element;
        }

        /// <summary>
        /// 元素
        /// </summary>
        public FrameworkElement Element { get; }

        /// <summary>
        /// 属性构建器集合
        /// </summary>
        public Dictionary<string, IDanceAnimationBuilderProperty> PropertyDic { get; } = new();

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
            this.Storyboard.Begin(this.Element);
        }
    }
}
