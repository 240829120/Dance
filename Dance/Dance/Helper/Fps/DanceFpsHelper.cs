using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// FPS辅助类
    /// </summary>
    public class DanceFpsHelper : DanceObject
    {
        /// <summary>
        /// FPS辅助类
        /// </summary>
        /// <param name="restrictFps">约束FPS</param>
        /// <param name="length">统计长度</param>
        public DanceFpsHelper(int restrictFps, int length)
        {
            this.RestrictFps = restrictFps;
            this.OneFrameTicks = TimeSpan.FromSeconds(1d / restrictFps).Ticks;
            this.Length = length;
            this.Datas = new long[length];
            this.Stopwatch.Start();
        }

        /// <summary>
        /// 统计长度
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// FPS
        /// </summary>
        public int FPS { get; private set; }

        /// <summary>
        /// 约束FPS
        /// </summary>
        public int RestrictFps { get; private set; }

        /// <summary>
        /// 一帧对应Ticks
        /// </summary>
        public long OneFrameTicks { get; private set; }

        // -----------------------------------------------------------------

        /// <summary>
        /// 索引
        /// </summary>
        private int Index;

        /// <summary>
        /// 统计数据
        /// </summary>
        private readonly long[] Datas;

        /// <summary>
        /// 总数
        /// </summary>
        private long Sum;

        /// <summary>
        /// 渲染目标时间
        /// </summary>
        private long RenderTargetTime;

        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        private long LastUpdateTime;

        /// <summary>
        /// 计时器
        /// </summary>
        private readonly Stopwatch Stopwatch = new();

        /// <summary>
        /// 计算
        /// </summary>
        /// <returns>是否可以渲染</returns>
        public bool Calculate()
        {
            long time = this.Stopwatch.ElapsedTicks;

            if (time < this.RenderTargetTime)
                return false;

            if (time > this.RenderTargetTime + 2 * this.OneFrameTicks)
                this.RenderTargetTime = time;

            long dt = time - this.LastUpdateTime;

            this.Index %= this.Length;

            this.Sum -= this.Datas[this.Index];
            this.Datas[this.Index++] = dt;
            this.Sum += dt;

            this.FPS = (int)Math.Round(10000000d / (this.Sum / this.Length));

            this.RenderTargetTime += this.OneFrameTicks;
            this.LastUpdateTime = time;

            return true;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Stopwatch?.Stop();
        }
    }
}