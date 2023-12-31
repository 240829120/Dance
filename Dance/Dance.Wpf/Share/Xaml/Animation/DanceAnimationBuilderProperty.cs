﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 动画构建器项
    /// </summary>
    /// <param name="propertyPath">关联属性</param>
    public abstract class DanceAnimationBuilderProperty<T>(string propertyPath) : IDanceAnimationBuilderProperty
    {
        /// <summary>
        /// 绑定属性
        /// </summary>
        public string PropertyPath { get; set; } = propertyPath;

        /// <summary>
        /// 关键帧
        /// </summary>
        public SortedList<TimeSpan, DanceAnimationKeyFrame<T>> KeyFrames { get; } = [];

        /// <summary>
        /// 缓动函数
        /// </summary>
        public IEasingFunction? Easing { get; set; }

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns>建时间线</returns>
        public abstract Timeline Build();
    }
}
