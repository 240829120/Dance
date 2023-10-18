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
    public class DanceDragEventArgs : EventArgs
    {
        /// <summary>
        /// 拖拽结束事件参数
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="data">数据</param>
        public DanceDragEventArgs(FrameworkElement element, IDataObject data)
        {
            this.Element = element;
            this.Data = data;
        }

        /// <summary>
        /// 元素
        /// </summary>
        public FrameworkElement Element { get; private set; }

        /// <summary>
        /// 拖拽事件参数
        /// </summary>
        public IDataObject Data { get; private set; }

        /// <summary>
        /// 效果
        /// </summary>
        public DragDropEffects Effects { get; set; } = DragDropEffects.None;
    }
}
