using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// ObservableCollection 类型扩展
    /// </summary>
    public static class DanceObservableCollectionExpansion
    {
        /// <summary>
        /// 添加元素至列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">列表</param>
        /// <param name="items">项集合</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// 循环列表中的项
        /// </summary>
        /// <typeparam name="T">元素</typeparam>
        /// <param name="collection">列表</param>
        /// <param name="action">行为</param>
        public static void ForEach<T>(this ObservableCollection<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }
    }
}
