using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 分页信息类型
    /// </summary>
    public enum DancePaginationInfoType
    {
        /// <summary>
        /// 页
        /// </summary>
        Page,

        /// <summary>
        /// 上一页
        /// </summary>
        PreviousPage,

        /// <summary>
        /// 下一页
        /// </summary>
        NextPage,

        /// <summary>
        /// 之前更多页
        /// </summary>
        PreviousMorePage,

        /// <summary>
        /// 之后跟多页
        /// </summary>
        NextMorePage
    }
}
