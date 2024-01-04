using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Dance
{
    /// <summary>
    /// IOC生命周期
    /// </summary>
    public class DanceIocLifeScope
    {
        /// <summary>
        /// 默认键
        /// </summary>
        public readonly static object DEFAULT_KEY = new();

        /// <summary>
        /// Ioc生命周期
        /// </summary>
        /// <param name="builder">Ioc构建器</param>
        internal DanceIocLifeScope(DanceIocBuilder builder)
        {
            this.Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <summary>
        /// 生命周期池
        /// </summary>
        private readonly ConcurrentDictionary<DanceIocInfo, object> LifeScopePool = new();

        /// <summary>
        /// Ioc构建器
        /// </summary>
        public DanceIocBuilder Builder { get; private set; }

        /// <summary>
        /// 确定实现对象
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>实现对象</returns>
        public TService Resolve<TService>() where TService : class
        {
            return this.Resolve<TService>(DEFAULT_KEY);
        }

        /// <summary>
        /// 确定实现对象
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>实现对象</returns>
        public TService Resolve<TService>(object key) where TService : class
        {
            Type type = typeof(TService);
            if (this.Resolve(type, key) is not TService service)
                throw new Exception($"Can not resolve Key: {key} ,  TService: {type.FullName}");

            return service;
        }

        /// <summary>
        /// 确定实现对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>实现对象</returns>
        public object Resolve(Type serviceType)
        {
            return this.Resolve(serviceType, DEFAULT_KEY);
        }

        /// <summary>
        /// 确定实现对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="key">键</param>
        /// <returns>实现对象</returns>
        public object Resolve(Type serviceType, object key)
        {
            ArgumentNullException.ThrowIfNull(serviceType);

            ArgumentNullException.ThrowIfNull(key);

            if (!this.Builder.Pool.TryGetValue(serviceType, out var dic))
                throw new NullReferenceException($"Can not found ServiceType: {serviceType.FullName}");

            if (!dic.TryGetValue(key, out var info))
                throw new NullReferenceException($"Can not found Key: {key}");

            lock (info)
            {
                if (info.IsSingleton)
                {
                    if (info.Instance == null)
                    {
                        if (string.IsNullOrWhiteSpace(info.ImplementationType.FullName))
                            throw new TypeUnloadedException($"Can not found Type: {info.ImplementationType.FullName}");

                        info.Instance = info.ImplementationType.Assembly.CreateInstance(info.ImplementationType.FullName);
                        if (info.Instance == null)
                            throw new NullReferenceException($"Can not create Type: {info.ImplementationType.FullName}");
                    }

                    return info.Instance;
                }
                else
                {
                    if (!this.LifeScopePool.TryGetValue(info, out object? instance))
                    {
                        if (string.IsNullOrWhiteSpace(info.ImplementationType.FullName))
                            throw new TypeUnloadedException($"Can not found Type: {info.ImplementationType.FullName}");

                        instance = info.ImplementationType.Assembly.CreateInstance(info.ImplementationType.FullName);
                        if (instance == null)
                            throw new NullReferenceException($"Can not create Type: {info.ImplementationType.FullName}");

                        if (!this.LifeScopePool.TryAdd(info, instance))
                            throw new NullReferenceException($"Can not add  instance Type: {info.ImplementationType.FullName}");
                    }

                    return instance;
                }
            }
        }
    }
}
