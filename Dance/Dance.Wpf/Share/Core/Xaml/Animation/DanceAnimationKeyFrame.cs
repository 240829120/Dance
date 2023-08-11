using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 动画关键帧
    /// </summary>
    public class DanceAnimationKeyFrame<T>
    {
        /// <summary>
        /// 动画关键帧
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="time">时间点</param>
        public DanceAnimationKeyFrame(T value, TimeSpan time)
        {
            this.Value = value;
            this.Time = time;
        }

        /// <summary>
        /// 动画关键帧
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="seconds">时间点</param>
        public DanceAnimationKeyFrame(T value, double seconds)
        {
            this.Value = value;
            this.Time = TimeSpan.FromSeconds(seconds);
        }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 时间点
        /// </summary>
        public TimeSpan Time { get; set; } = TimeSpan.Zero;
    }
}
