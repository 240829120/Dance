using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Dance.Manager;
using log4net;

namespace Dance.Wpf
{
    /// <summary>
    /// 领域构建器 -- 释放
    /// </summary>
    public class DanceDomainBuilder_Dispose : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "释放";

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
            IDanceDisposeManager disposeManager = DanceDomain.Current.LifeScope.Resolve<IDanceDisposeManager>();
            if (disposeManager == null)
                return;

            disposeManager.Dispose();
        }
    }
}
