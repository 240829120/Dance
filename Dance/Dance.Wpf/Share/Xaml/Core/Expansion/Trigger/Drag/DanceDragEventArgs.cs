using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 拖拽事件参数
    /// </summary>
    /// <param name="element">元素</param>
    /// <param name="e">拖拽事件参数</param>
    public class DanceDragEventArgs(FrameworkElement element, DragEventArgs e) : EventArgs
    {
        /// <summary>
        /// 元素
        /// </summary>
        public FrameworkElement Element { get; private set; } = element;

        /// <summary>
        /// 拖拽事件参数
        /// </summary>
        public DragEventArgs EventArgs { get; private set; } = e;
    }
}
