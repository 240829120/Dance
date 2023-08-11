using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 领域构建器
    /// </summary>
    public interface IDanceDomainBuilder : IDisposable
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 构建
        /// </summary>
        void Build();
    }
}
