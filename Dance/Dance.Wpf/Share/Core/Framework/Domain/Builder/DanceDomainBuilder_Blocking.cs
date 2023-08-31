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
    /// 领域构建器 -- 阻塞
    /// </summary>
    public class DanceDomainBuilder_Blocking : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "阻塞";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {

        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            IDanceBlockingManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceBlockingManager>();
            manager.Dispose();
        }
    }
}
