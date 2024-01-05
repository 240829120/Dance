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
    /// 列表添加步骤
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="oldIndex">原始索引</param>
    /// <param name="newIndex">新索引</param>
    /// <param name="detail">描述</param>
    public class DanceCollectionMoveHistoryStep(IList target, int oldIndex, int newIndex, string detail = "集合移动") : DanceObject, IDanceHistoryStep
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
        public int OldIndex { get; } = oldIndex;

        /// <summary>
        /// 集合
        /// </summary>
        public int NewIndex { get; } = newIndex;

        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Redo(IDanceHistoryManager manager)
        {
            (this.Target[this.OldIndex], this.Target[this.NewIndex]) = (this.Target[this.NewIndex], this.Target[this.OldIndex]);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Undo(IDanceHistoryManager manager)
        {
            (this.Target[this.NewIndex], this.Target[this.OldIndex]) = (this.Target[this.OldIndex], this.Target[this.NewIndex]);
        }
    }
}