﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// Double动画处理器
    /// </summary>
    public class DanceDoubleAnimationProvider : DanceAnimationProviderBase<double>
    {
        /// <summary>
        /// 转化
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="t">值所占百分比</param>
        /// <param name="from">开始值</param>
        /// <param name="to">结束值</param>
        /// <returns>转化后的值</returns>
        protected override double Transform(IDanceAnimationBuilder builder, double t, double from, double to)
        {
            return from + (to - from) * t;
        }
    }
}
