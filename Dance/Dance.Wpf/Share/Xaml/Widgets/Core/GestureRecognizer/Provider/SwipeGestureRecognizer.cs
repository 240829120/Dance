using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 轻扫
    /// </summary>
    public class SwipeGestureRecognizer : DanceObject, IDanceGestureRecognizer
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="element">元素</param>
        public void Register(FrameworkElement element)
        {

        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="element">元素</param>
        public void UnRegister(FrameworkElement element)
        {

        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {

        }
    }
}
