using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 单例
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class DanceSingletonAttribute : Attribute
    {
        /// <summary>
        /// 单例
        /// </summary>
        public DanceSingletonAttribute()
        {
            this.Key = DanceIocLifeScope.DEFAULT_KEY;
        }

        /// <summary>
        /// 单例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        public DanceSingletonAttribute(Type serviceType)
        {
            this.Key = DanceIocLifeScope.DEFAULT_KEY;
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// 单例
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="serviceType">服务类型</param>
        public DanceSingletonAttribute(object key, Type serviceType)
        {
            this.Key = key;
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// 键
        /// </summary>
        public object Key { get; private set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public Type? ServiceType { get; private set; }
    }
}
