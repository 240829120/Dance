using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Ioc构建器扩展 -- 单例
    /// </summary>
    public static class DanceIocBuilder_Singleton
    {
        /// <summary>
        /// 添加单例
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementationType">实现类型</typeparam>
        /// <param name="builder">Ioc构建器</param>
        /// <returns>实例</returns>
        public static DanceIocBuilder AddSingleton<TService, TImplementationType>(this DanceIocBuilder builder) where TImplementationType : class, TService, new()
        {
            return builder.AddSingleton<TService, TImplementationType>(DanceIocLifeScope.DEFAULT_KEY);
        }

        /// <summary>
        /// 添加单例
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementationType">实现类型</typeparam>
        /// <param name="builder">Ioc构建器</param>
        /// <param name="key">键</param>
        /// <returns>实例</returns>
        public static DanceIocBuilder AddSingleton<TService, TImplementationType>(this DanceIocBuilder builder, object key) where TImplementationType : class, TService, new()
        {
            return builder.AddSingleton(key, typeof(TService), typeof(TImplementationType));
        }

        /// <summary>
        /// 添加单例
        /// </summary>
        /// <param name="builder">Ioc构建器</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实现类型</param>
        /// <returns>实例</returns>
        public static DanceIocBuilder AddSingleton(this DanceIocBuilder builder, Type serviceType, Type implementationType)
        {
            return builder.AddSingleton(DanceIocLifeScope.DEFAULT_KEY, serviceType, implementationType);
        }

        /// <summary>
        /// 添加单例
        /// </summary>
        /// <param name="builder">Ioc构建器</param>
        /// <param name="key">键</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实现类型</param>
        /// <returns>实例</returns>
        public static DanceIocBuilder AddSingleton(this DanceIocBuilder builder, object key, Type serviceType, Type implementationType)
        {
            DanceIocInfo info = new(key, serviceType, implementationType)
            {
                IsSingleton = true
            };

            builder.AddInfo(info);

            return builder;
        }
    }
}
