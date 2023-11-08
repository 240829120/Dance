using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 包装模型
    /// </summary>
    public class DanceWrapperModel : DanceModel
    {
        /// <summary>
        /// 触发包装模型属性改变之前事件
        /// </summary>
        /// <param name="propertyName">属性名</param>
        protected void OnWrapperPropertyChanging([CallerMemberName] string? propertyName = null)
        {
            if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null)
            {
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
                return;
            }

            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
            });
        }

        /// <summary>
        /// 触发包装模型属性改变之后事件
        /// </summary>
        /// <param name="propertyName">属性名</param>
        protected void OnWrapperPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
                return;
            }

            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            });
        }
    }
}
