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
    /// 领域构建器 -- 日志
    /// </summary>
    public class DanceDomainBuilder_Log : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "日志";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "log.config");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {

        }
    }
}
