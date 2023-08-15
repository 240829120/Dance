using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 手势识别
    /// </summary>
    public interface IDanceGestureRecognizer : IDisposable
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="element">元素</param>
        void Register(FrameworkElement element);

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="element">元素</param>
        void UnRegister(FrameworkElement element);
    }
}