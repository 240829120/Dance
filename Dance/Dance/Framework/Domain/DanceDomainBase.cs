using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 领域基类
    /// </summary>
    public abstract class DanceDomainBase<TDomain> : DanceObject where TDomain : DanceDomainBase<TDomain>
    {
        public DanceDomainBase()
        {
            this.LifeScope = this.IocBuilder.CreateLifeScope();
        }

        /// <summary>
        /// 当前领域
        /// </summary>
        [NotNull]
        public static TDomain? Current { get; set; }

        /// <summary>
        /// Ioc构建器
        /// </summary>
        public DanceIocBuilder IocBuilder { get; } = new();

        /// <summary>
        /// 生命周期
        /// </summary>
        public DanceIocLifeScope LifeScope { get; private set; }

        /// <summary>
        /// 构建器集合
        /// </summary>
        public List<IDanceDomainBuilder> Builders { get; } = new();

        /// <summary>
        /// 消息
        /// </summary>
        public IMessenger Messenger { get; } = new WeakReferenceMessenger();

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
#if DEBUG
            Stopwatch stopwatch = new();

            Debug.WriteLine("========================================================================================");

            foreach (IDanceDomainBuilder builder in this.Builders)
            {
                stopwatch.Restart();
                builder.Build();

                Debug.WriteLine($"领域构建 || {builder.Name,-30} -- {stopwatch.ElapsedMilliseconds}ms");
            }

            stopwatch.Stop();
            Debug.WriteLine("========================================================================================");
#else
            foreach (IDanceDomainBuilder builder in this.Builders)
            {
                builder.Build();
            }
#endif
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
#if DEBUG
            Stopwatch stopwatch = new();

            Debug.WriteLine("========================================================================================");

            foreach (IDanceDomainBuilder builder in this.Builders)
            {
                stopwatch.Restart();
                builder.Dispose();

                Debug.WriteLine($"领域销毁 || {builder.Name,-30} -- {stopwatch.ElapsedMilliseconds}ms");
            }

            stopwatch.Stop();
            Debug.WriteLine("========================================================================================");
#else
            foreach (IDanceDomainBuilder builder in this.Builders)
            {
                builder.Dispose();
            }
#endif
        }
    }
}