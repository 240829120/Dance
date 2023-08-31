using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace Dance.Maui
{
    /// <summary>
    /// 领域构建器 -- 监视
    /// </summary>
    public class DanceDomainBuilder_Monitor : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 监视
        /// </summary>
        public string Name { get; } = "监视";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            IDanceMonitorManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceMonitorManager>();
            if (manager == null)
                return;

            // 网络监视
            manager.Executers.Add(new DanceMonitorExecuter_Network());

            manager.Initialize();
            manager.Start();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            IDanceMonitorManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceMonitorManager>();
            manager?.Dispose();
        }
    }
}
