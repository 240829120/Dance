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
    public interface IDanceAnimationBuilder
    {
        /// <summary>
        /// 元素
        /// </summary>
        VisualElement Element { get; }

        /// <summary>
        /// 项集合
        /// </summary>
        Dictionary<BindableProperty, IDanceAnimationBuilderProperty> PropertyDic { get; }

        /// <summary>
        /// 持续时间
        /// </summary>
        TimeSpan? Duration { get; }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="name">动画名称</param>
        /// <returns>是否被取消</returns>
        Task<bool> Commit(string name);
    }
}
