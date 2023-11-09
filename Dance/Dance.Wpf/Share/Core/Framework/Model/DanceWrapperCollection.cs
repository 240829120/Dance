using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 包装列表
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class DanceWrapperCollection<T> : ObservableCollection<T>
    {
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null)
            {
                base.OnCollectionChanged(e);
                return;
            }

            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                base.OnCollectionChanged(e);
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                base.OnCollectionChanged(e);
            });
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null)
            {
                base.OnPropertyChanged(e);
                return;
            }

            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                base.OnPropertyChanged(e);
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                base.OnPropertyChanged(e);
            });
        }
    }
}
