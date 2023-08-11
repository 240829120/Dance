using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 领域扩展
    /// </summary>
    public static class DanceDomainExpansion
    {
        /// <summary>
        /// 使用Dance框架
        /// </summary>
        /// <param name="builder">Maui构建器</param>
        /// <returns>Maui构建器</returns>
        public static MauiAppBuilder UseDance(this MauiAppBuilder builder)
        {
            DanceDomain.Current = new DanceDomain();

            return builder;
        }
    }
}
