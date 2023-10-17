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
    /// 领域构建器 -- UI
    /// </summary>
    public class DanceDomainBuilder_UI : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "UI";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            DanceMessageExpansion.Initialize();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {

        }
    }
}
