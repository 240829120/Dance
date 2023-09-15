using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 固定长度标准化流
    /// </summary>
    public class DanceFixedNormalStream : DanceNormalStreamBase
    {
        /// <summary>
        /// 固定长度标准化流
        /// </summary>
        /// <param name="fixedLength">数据长度</param>
        /// <param name="header">帧头</param>
        /// <param name="cacheLength">缓存大小</param>
        public DanceFixedNormalStream(int fixedLength, byte[]? header = null, int cacheLength = 1024 * 1024)
        {
            this.FixedLength = fixedLength;
            this.Header = header;
            this.Cache = new byte[cacheLength];
        }

        /// <summary>
        /// 缓存
        /// </summary>
        private readonly byte[] Cache;

        /// <summary>
        /// 开始索引
        /// </summary>
        private int StartIndex;

        /// <summary>
        /// 结束索引
        /// </summary>
        private int EndIndex;

        /// <summary>
        /// 固定长度
        /// </summary>
        public int FixedLength { get; protected set; }

        /// <summary>
        /// 帧头
        /// </summary>
        public byte[]? Header { get; protected set; }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns></returns>
        public override byte[]? Read()
        {
            lock (this.lock_object)
            {
                while (true)
                {
                    if (!this.TryAppendCache())
                        return null;

                    if (!this.TryFixPosition())
                        continue;

                    if (!this.TryAppendCache())
                        return null;

                    byte[] buffer = new byte[this.FixedLength];
                    Buffer.BlockCopy(this.Cache, this.StartIndex, buffer, 0, this.FixedLength);
                    this.StartIndex += this.FixedLength;

                    return buffer;
                }
            }
        }

        /// <summary>
        /// 尝试追加缓存
        /// </summary>
        /// <returns>是否满足数据</returns>
        private bool TryAppendCache()
        {
            while (this.EndIndex - this.StartIndex < this.FixedLength)
            {
                if (this.Queue.Count == 0)
                    return false;

                byte[] buffer = this.Queue.Dequeue();

                if (this.EndIndex + buffer.Length > this.Cache.Length - 1)
                {
                    Buffer.BlockCopy(this.Cache, this.StartIndex, this.Cache, 0, this.EndIndex - this.StartIndex);
                    this.EndIndex -= this.StartIndex;
                    this.StartIndex = 0;
                }

                Buffer.BlockCopy(buffer, 0, this.Cache, this.EndIndex, buffer.Length);
                this.EndIndex += buffer.Length;
            }

            return true;
        }

        /// <summary>
        /// 尝试定位数据
        /// </summary>
        /// <returns>是否成功定位</returns>
        private bool TryFixPosition()
        {
            if (this.Header == null || this.Header.Length == 0)
                return true;

            for (int i = this.StartIndex; i <= this.EndIndex - this.Header.Length; i++)
            {
                this.StartIndex = i;
                bool isSame = true;

                for (int j = 0; j < this.Header.Length; j++)
                {
                    if (this.Cache[i + j] != this.Header[j])
                    {
                        isSame = false;
                        break;
                    }
                }

                if (isSame)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
