using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 阻塞信息
    /// </summary>
    /// <param name="key">键</param>
    public class DanceBlockingInfo(string key)
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; private set; } = key;

        /// <summary>
        /// 是否已经被释放
        /// </summary>
        public bool IsReleased { get; set; }
    }
}
