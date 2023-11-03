using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 列表扩展
    /// </summary>
    public static class DanceIListExpansion
    {
        /// <summary>
        /// 自排序
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="compare">比较方法</param>
        public static void SortSelf<T>(this IList<T> list, Func<T, T, int> compare)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (compare(list[j], list[j + 1]) == 1)
                    {
                        (list[j + 1], list[j]) = (list[j], list[j + 1]);
                    }
                }
            }
        }
    }
}
