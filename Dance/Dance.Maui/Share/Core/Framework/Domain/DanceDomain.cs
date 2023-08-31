using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 领域
    /// </summary>
    public class DanceDomain : DanceDomainBase<DanceDomain>
    {
        public DanceDomain()
        {
            // IOC构建
            this.IocBuilder.AddAssemblys(Assembly.Load("Dance"), Assembly.Load("Dance.Maui"));

            // 捕获未处理异常
            this.Builders.Add(new DanceDomainBuilder_CatchUnhandledException());
            // 日志
            this.Builders.Add(new DanceDomainBuilder_Log());
            // 操作日志
            this.Builders.Add(new DanceDomainBuilder_Record());
            // 延时
            this.Builders.Add(new DanceDomainBuilder_Delay());
            // 循环
            this.Builders.Add(new DanceDomainBuilder_Loop());
            // 阻塞
            this.Builders.Add(new DanceDomainBuilder_Blocking());
            // 监视
            this.Builders.Add(new DanceDomainBuilder_Monitor());
        }

        /// <summary>
        /// 当前领域
        /// </summary>
        [NotNull]
        public static DanceDomain? Current { get; set; }
    }
}
