using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 动画扩展
    /// </summary>
    public static class DanceAnimationExpansion
    {
        /// <summary>
        /// 创建关键帧动画
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>动画构建器</returns>
        public static IDanceAnimationBuilder CreateKeyFrameAnimation(this FrameworkElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return new DanceAnimationKeyFrameBuilder(element);
        }
    }
}
