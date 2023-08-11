using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 离散动画处理器
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class DanceAnimationDiscreteProvider<T> : DanceAnimationProviderBase<T>
    {
        /// <summary>
        /// 转化
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="t">值所占百分比</param>
        /// <param name="from">开始值</param>
        /// <param name="to">结束值</param>
        /// <returns>转化后的值</returns>
        protected override T Transform(IDanceAnimationBuilder builder, double t, T from, T to)
        {
            return t < 1 ? from : to;
        }
    }
}
