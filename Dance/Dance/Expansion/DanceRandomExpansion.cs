using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 随机数扩展
    /// </summary>
    public static class DanceRandomExpansion
    {
        /// <summary>
        /// 获取随机值
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns>随机值</returns>
        public static float NextFloat(this Random random, float value1, float value2)
        {
            float minValue = MathF.Min(value1, value2);
            float maxValue = MathF.Max(value1, value2);

            double position = random.NextDouble();
            return (float)(minValue + (maxValue - minValue) * position);
        }

        /// <summary>
        /// 获取随机值
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns>随机值</returns>
        public static double NextDouble(this Random random, double value1, double value2)
        {
            double minValue = Math.Min(value1, value2);
            double maxValue = Math.Max(value1, value2);

            double position = random.NextDouble();
            return minValue + (maxValue - minValue) * position;
        }

        /// <summary>
        /// 获取随机值
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns>随机值</returns>
        public static TimeSpan NextTimeSpan(this Random random, TimeSpan value1, TimeSpan value2)
        {
            long minValue = Math.Min(value1.Ticks, value2.Ticks);
            long maxValue = Math.Max(value1.Ticks, value2.Ticks);

            double position = random.NextDouble();
            return TimeSpan.FromTicks((long)(minValue + (maxValue - minValue) * position));
        }
    }
}
