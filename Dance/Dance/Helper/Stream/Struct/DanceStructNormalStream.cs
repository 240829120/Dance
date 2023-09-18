using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 数据结构标准化流
    /// </summary>
    /// <typeparam name="T">数据结构类型</typeparam>
    public class DanceStructNormalStream<T> : DanceFixedNormalStream where T : struct
    {
        /// <summary>
        /// 固定长度标准化流
        /// </summary>
        /// <param name="header">帧头</param>
        /// <param name="cacheLength">缓存大小</param>
        public DanceStructNormalStream(byte[]? header = null, int cacheLength = 1024) : base(0, header, cacheLength)
        {
            this.FixedLength = Marshal.SizeOf<T>();
            this.Helper = new DanceStructNormalStreamHelper(typeof(T));
        }

        /// <summary>
        /// 数据结构标准化流辅助类
        /// </summary>
        protected readonly DanceStructNormalStreamHelper Helper;

        /// <summary>
        /// 读取数据结构
        /// </summary>
        /// <param name="isSwap">是否交换字节顺序</param>
        /// <returns>数据结构</returns>
        public T? ReadStruct(bool isSwap)
        {
            byte[]? buffer = base.Read();
            if (buffer == null)
                return null;

            return this.Helper.ConvertToStruct<T>(buffer, isSwap);
        }

        /// <summary>
        /// 写入数据结构
        /// </summary>
        /// <param name="obj">数据结构</param>
        /// <param name="isSwap">是否交换字节顺序</param>
        public void WriteStruct(T obj, bool isSwap)
        {
            base.Write(this.Helper.ConvertToByte(obj, isSwap));
        }
    }
}
