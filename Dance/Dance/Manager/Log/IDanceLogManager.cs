using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public interface IDanceLogManager
    {
        /// <summary>
        /// 记录日志事件
        /// </summary>
        event EventHandler<DanceLogEventArgs>? Logging;
    }
}
