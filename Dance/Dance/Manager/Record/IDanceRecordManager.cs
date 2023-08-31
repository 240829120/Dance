using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 操作记录管理器
    /// </summary>
    public interface IDanceRecordManager
    {
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="content">内容</param>
        void Log(string content);

        /// <summary>
        /// 将操作日志输出到文件
        /// </summary>
        void Flush();
    }
}
