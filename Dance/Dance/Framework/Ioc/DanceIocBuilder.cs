using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Ioc构建器
    /// </summary>
    public class DanceIocBuilder
    {
        /// <summary>
        /// 信息池
        /// </summary>
        public ConcurrentDictionary<Type, ConcurrentDictionary<object, DanceIocInfo>> Pool { get; } = new();

        /// <summary>
        /// 创建生命周期
        /// </summary>
        /// <returns>生命周期</returns>
        public DanceIocLifeScope CreateLifeScope()
        {
            return new(this);
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="info">IOC信息</param>
        internal void AddInfo(DanceIocInfo info)
        {
            lock (this.Pool)
            {
                if (!this.Pool.TryGetValue(info.ServiceType, out var dic))
                {
                    dic = new();

                    if (!this.Pool.TryAdd(info.ServiceType, dic))
                        throw new Exception("Update DanceIocBuilder.Pool error.");
                }

                if (!dic.TryAdd(info.Key, info))
                    throw new Exception("Update DanceIocBuilder.Pool Item error.");
            }
        }
    }
}
