using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Ioc信息
    /// </summary>
    public class DanceIocInfo
    {
        /// <summary>
        /// Ioc信息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="ImplementationType">实现类型</param>
        public DanceIocInfo(object key, Type serviceType, Type ImplementationType)
        {
            this.Key = key;
            this.ServiceType = serviceType;
            this.ImplementationType = ImplementationType;
        }

        /// <summary>
        /// 键
        /// </summary>
        public object Key { get; private set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get; private set; }

        /// <summary>
        /// 实现类型
        /// </summary>
        public Type ImplementationType { get; private set; }

        /// <summary>
        /// 是否是单例
        /// </summary>
        public bool IsSingleton { get; set; }

        /// <summary>
        /// 实例
        /// </summary>
        internal object? Instance { get; set; }
    }
}
