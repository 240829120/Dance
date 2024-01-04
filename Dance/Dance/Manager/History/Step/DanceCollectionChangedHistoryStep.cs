using CommunityToolkit.Mvvm.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 列表改变步骤
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="args">行为参数</param>
    /// <param name="detail">描述</param>
    public class DanceCollectionChangedHistoryStep(IList target, NotifyCollectionChangedEventArgs args, string detail) : DanceObject, IDanceHistoryStep
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Detail { get; } = detail;

        /// <summary>
        /// 参数
        /// </summary>
        public NotifyCollectionChangedEventArgs Args { get; } = args;

        /// <summary>
        /// 目标
        /// </summary>
        public IList Target { get; } = target;

        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Redo(IDanceHistoryManager manager)
        {

        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Undo(IDanceHistoryManager manager)
        {

        }
    }
}
