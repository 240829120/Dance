using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Xaml扩展
    /// </summary>
    public static class DanceXamlExpansion
    {
        /// <summary>
        /// 获取祖先元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="element">查找开始元素</param>
        /// <returns>祖先元素</returns>
        public static T? GetVisualElementParent<T>(this Element element) where T : Element
        {
            if (element == null)
                return null;

            Element parent = element.Parent;

            while (parent != null && parent is not T)
            {
                parent = parent.Parent;
            };

            return parent as T;
        }

        /// <summary>
        /// 获取子元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="element">查找开始元素</param>
        /// <returns>子元素集合</returns>
        public static IReadOnlyList<T> GetVisualElementChildren<T>(this Element element) where T : Element
        {
            return element.GetVisualTreeDescendants().Where(p => p is T).Select(p => (T)p).ToList();
        }

        /// <summary>
        /// 获取相对位置
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="relative">相对元素</param>
        /// <returns>相对位置</returns>
        public static Point? GetVisualPosition(this VisualElement element, VisualElement relative)
        {
            if (element.Parent is not VisualElement parent)
                return null;

            Point point = new(element.X, element.Y);

            while (parent != null && parent != relative)
            {
                if (parent.Parent is not VisualElement p)
                    return null;

                parent = p;
                point.X += p.X;
                point.Y += p.Y;
            }

            return point;
        }
    }
}
