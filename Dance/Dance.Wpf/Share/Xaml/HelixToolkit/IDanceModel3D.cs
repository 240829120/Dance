using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 3D模型
    /// </summary>
    public interface IDanceModel3D : INotifyPropertyChanging, INotifyPropertyChanged, IDanceJsonObject, IDisposable
    {
        /// <summary>
        /// 数据模板
        /// </summary>
        DataTemplate DataTemplate { get; }
    }
}
