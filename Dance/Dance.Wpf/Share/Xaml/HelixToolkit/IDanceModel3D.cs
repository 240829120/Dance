﻿using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

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

        /// <summary>
        /// 变换
        /// </summary>
        DanceTransform3D Transform { get; set; }

        /// <summary>
        /// 所属元素
        /// </summary>
        Element3D? Element { get; set; }

        /// <summary>
        /// 更新变换
        /// </summary>
        void UpdateTransform();
    }
}
