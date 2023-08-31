using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 领域构建器 -- 延时
    /// </summary>
    public class DanceDomainBuilder_Delay : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "延时";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            IDanceDelayManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceDelayManager>();
            manager.Start();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            IDanceDelayManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceDelayManager>();
            manager.Dispose();
        }
    }
}
