using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 列表替换步骤
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="oldIndex">原始索引</param>
    /// <param name="oldItems">原始集合</param>
    /// <param name="newIndex">新索引</param>
    /// <param name="newItems">新集合</param>
    /// <param name="detail">描述</param>
    public class DanceCollectionReplaceHistoryStep(IList target, int oldIndex, IList oldItems, int newIndex, IList newItems, string detail = "集合替换") : DanceObject, IDanceHistoryStep
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Detail { get; } = detail;

        /// <summary>
        /// 目标
        /// </summary>
        public IList Target { get; } = target;

        /// <summary>
        /// 原始索引
        /// </summary>
        public int OldIndex { get; } = oldIndex;

        /// <summary>
        /// 原始集合
        /// </summary>
        public IList OldItems { get; } = oldItems;

        /// <summary>
        /// 新索引
        /// </summary>
        public int NewIndex { get; } = newIndex;

        /// <summary>
        /// 新集合
        /// </summary>
        public IList NewItems { get; } = newItems;

        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Redo(IDanceHistoryManager manager)
        {
            for (int i = 0; i < this.NewItems.Count; i++)
            {
                this.Target[this.NewIndex + i] = this.NewItems[i];
            }
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Undo(IDanceHistoryManager manager)
        {
            for (int i = 0; i < this.OldItems.Count; i++)
            {
                this.Target[this.OldIndex + i] = this.OldItems[i];
            }
        }
    }
}