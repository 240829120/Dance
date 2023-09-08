using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace Dance.Wpf
{
    /// <summary>
    /// 领域构建器 -- 操作日志
    /// </summary>
    public class DanceDomainBuilder_Record : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        public string Name { get; } = "操作日志";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            IDanceLoopManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceLoopManager>();
            manager.Register("IDanceRecordManager.Flush", 30, () =>
            {
                IDanceRecordManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceRecordManager>();
                manager?.Flush();
            });
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            IDanceRecordManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceRecordManager>();
            manager?.Flush();
            manager?.Dispose();
        }
    }
}
