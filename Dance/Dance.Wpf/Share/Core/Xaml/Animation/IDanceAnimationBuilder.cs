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
    public interface IDanceAnimationBuilder
    {
        /// <summary>
        /// 元素
        /// </summary>
        FrameworkElement Element { get; }

        /// <summary>
        /// 项集合
        /// </summary>
        Dictionary<string, IDanceAnimationBuilderProperty> PropertyDic { get; }

        /// <summary>
        /// 动画
        /// </summary>
        Storyboard Storyboard { get; }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>是否被取消</returns>
        void Commit(string name);
    }
}
