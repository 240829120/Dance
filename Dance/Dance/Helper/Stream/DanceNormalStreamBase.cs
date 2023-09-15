using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 标准化流基类
    /// </summary>
    public abstract class DanceNormalStreamBase : DanceObject, IDanceNormalStream
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        protected object lock_object = new object();

        /// <summary>
        /// 队列
        /// </summary>
        protected Queue<byte[]> Queue = new();

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>实际读取数据量</returns>
        public abstract byte[]? Read();

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">数据</param>
        public virtual void Write(byte[] buffer)
        {
            lock (lock_object)
            {
                this.Queue.Enqueue(buffer);
            }
        }
    }
}
