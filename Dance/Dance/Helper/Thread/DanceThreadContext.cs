using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 线程上下文
    /// </summary>
    public class DanceThreadContext
    {
        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception? Exception { get; internal set; }
    }
}
