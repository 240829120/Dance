using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// Color动画处理器
    /// </summary>
    public class DanceColorAnimationProvider : DanceAnimationProviderBase<Color>
    {
        /// <summary>
        /// 转化
        /// </summary>
        /// <param name="builder">动画构建器</param>
        /// <param name="t">值所占百分比</param>
        /// <param name="from">开始值</param>
        /// <param name="to">结束值</param>
        /// <returns>转化后的值</returns>
        protected override Color Transform(IDanceAnimationBuilder builder, double t, Color from, Color to)
        {
            double r = (1 - t) * from.Red + t * to.Red;
            double g = (1 - t) * from.Green + t * to.Green;
            double b = (1 - t) * from.Blue + t * to.Blue;
            double a = (1 - t) * from.Alpha + t * to.Alpha;

            return Color.FromRgba(r, g, b, a);
        }
    }
}
