using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 领域构建器 -- 循环
    /// </summary>
    public class DanceDomainBuilder_Loop : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "循环";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            IDanceLoopManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceLoopManager>();
            manager.Start();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            IDanceLoopManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceLoopManager>();
            manager.Dispose();
        }
    }
}
