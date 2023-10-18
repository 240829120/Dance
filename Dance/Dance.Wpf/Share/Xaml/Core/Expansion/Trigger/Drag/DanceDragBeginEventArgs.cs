using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 拖拽开始事件参数
    /// </summary>
    public class DanceDragBeginEventArgs : EventArgs
    {
        /// <summary>
        /// 拖拽开始事件参数
        /// </summary>
        /// <param name="element">元素</param>
        public DanceDragBeginEventArgs(FrameworkElement element)
        {
            this.Element = element;
        }

        /// <summary>
        /// 元素
        /// </summary>
        public FrameworkElement Element { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// 效果
        /// </summary>
        public DragDropEffects Effects { get; set; } = DragDropEffects.Copy;

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
