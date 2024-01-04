using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 操作日志信息
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="time">时间</param>
    /// <param name="content">内容</param>
    public class DanceRecordInfo(long id, DateTime time, string content)
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long ID { get; private set; } = id;

        /// <summary>
        /// 时间点
        /// </summary>
        public DateTime Time { get; private set; } = time;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; } = content;
    }
}
