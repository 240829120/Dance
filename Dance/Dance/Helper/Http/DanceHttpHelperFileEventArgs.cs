using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Http辅助类 文件事件参数
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="length">总长度</param>
    /// <param name="localPath">本地路径</param>
    /// <param name="url">请求地址</param>
    public class DanceHttpHelperFileEventArgs(long position, long length, string localPath, string url) : EventArgs
    {
        /// <summary>
        /// 位置
        /// </summary>
        public long Position { get; private set; } = position;

        /// <summary>
        /// 总长度
        /// </summary>
        public long Length { get; private set; } = length;

        /// <summary>
        /// 本地路径
        /// </summary>
        public string LocalPath { get; private set; } = localPath;

        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; private set; } = url;

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
