using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// IEnumerable 扩展
    /// </summary>
    public static class DanceIEnumerableExpansion
    {
        /// <summary>
        /// 获取对象在集合中的位序
        /// </summary>
        /// <param name="items">集合</param>
        /// <param name="item">对象</param>
        /// <returns>位序</returns>
        public static int IndexOf(this IEnumerable items, object item)
        {
            if (items == null)
                return -1;

            int index = 0;

            foreach (object i in items)
            {
                if (i == item)
                    return index;

                ++index;
            }

            return -1;
        }

        /// <summary>
        /// 集合元素个数
        /// </summary>
        /// <param name="items">集合</param>
        /// <returns>集合元素个数</returns>
        public static int Count(this IEnumerable items)
        {
            if (items == null)
                return 0;

            int index = 0;

            foreach (object i in items)
            {
                ++index;
            }

            return index;
        }

        /// <summary>
        /// 获取元素
        /// </summary>
        /// <param name="items">集合</param>
        /// <param name="index">索引</param>
        /// <returns>元素</returns>
        public static object? GetItemAt(this IEnumerable items, int index)
        {
            if (items == null || index < 0)
                return null;

            int step = 0;

            foreach (object i in items)
            {
                if (step == index)
                    return i;

                ++step;
            }

            return null;
        }

        /// <summary>
        /// 获取最大值或默认值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="items">数据集合</param>
        /// <returns>最大值或默认值</returns>
        public static T? MaxOrDefault<T>(this IEnumerable<T> items)
        {
            if (!items.Any())
                return default;

            return items.Max();
        }

        /// <summary>
        /// 获取最小值或默认值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="items">数据集合</param>
        /// <returns>最小值或默认值</returns>
        public static T? MinOrDefault<T>(this IEnumerable<T> items)
        {
            if (!items.Any())
                return default;

            return items.Min();
        }

        /// <summary>
        /// 转化为 ObservableCollection
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="items">数据集合</param>
        /// <returns>ObservableCollection</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            return new(items);
        }
    }
}