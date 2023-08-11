using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// Double动画处理器
    /// </summary>
    public class DanceThicknessAnimationProvider : DanceAnimationProviderBase<Thickness>
    {
        /// <summary>
        /// 转化
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="t">值所占百分比</param>
        /// <param name="from">开始值</param>
        /// <param name="to">结束值</param>
        /// <returns>转化后的值</returns>
        protected override Thickness Transform(IDanceAnimationBuilder builder, double t, Thickness from, Thickness to)
        {
            double left = from.Left + (to.Left - from.Left) * t;
            double right = from.Right + (to.Right - from.Right) * t;
            double top = from.Top + (to.Top - from.Top) * t;
            double bottom = from.Bottom + (to.Bottom - from.Bottom) * t;

            return new Thickness(left, top, right, bottom);
        }
    }
}
