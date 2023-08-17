using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 轻扫事件参数
    /// </summary>
    public class DanceSwipeGestureRecognizerEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// 轻扫事件参数
        /// </summary>
        /// <param name="direction">方向</param>
        public DanceSwipeGestureRecognizerEventArgs(DanceSwipeGestureRecognizerDirection direction)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// 方向
        /// </summary>
        public DanceSwipeGestureRecognizerDirection Direction { get; private set; }
    }
}
