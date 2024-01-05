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
    /// 列表移除步骤
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="index">索引</param>
    /// <param name="items">集合</param>
    /// <param name="detail">描述</param>
    public class DanceCollectionRemoveHistoryStep(IList target, int index, IList items, string detail = "集合移除") : DanceObject, IDanceHistoryStep
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
        /// 索引
        /// </summary>
        public int Index { get; } = index;

        /// <summary>
        /// 集合
        /// </summary>
        public IList Items { get; } = items;

        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Redo(IDanceHistoryManager manager)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.Target.RemoveAt(this.Index);
            }
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Undo(IDanceHistoryManager manager)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.Target.Insert(this.Index + i, this.Items[i]);
            }
        }
    }
}