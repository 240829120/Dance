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
    /// <param name="direction">方向</param>
    public class DanceSwipeGestureRecognizerEventArgs(DanceSwipeGestureRecognizerDirection direction) : RoutedEventArgs
    {
        /// <summary>
        /// 方向
        /// </summary>
        public DanceSwipeGestureRecognizerDirection Direction { get; private set; } = direction;
    }
}
