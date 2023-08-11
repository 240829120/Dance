using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 生命周期
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class DanceLifeScopeAttribute : Attribute
    {
        /// <summary>
        /// 生命周期
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        public DanceLifeScopeAttribute(Type serviceType)
        {
            this.Key = DanceIocLifeScope.DEFAULT_KEY;
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// 生命周期
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="serviceType">服务类型</param>
        public DanceLifeScopeAttribute(object key, Type serviceType)
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
        public Type ServiceType { get; private set; }
    }
}
