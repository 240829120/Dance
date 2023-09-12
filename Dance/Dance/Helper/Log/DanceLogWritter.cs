using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 日志写入器
    /// </summary>
    public class DanceLogWritter : TextWriter
    {
        /// <summary>
        /// 日志写入器
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <param name="writting">写入</param>
        public DanceLogWritter(Encoding encoding, Action<string>? writting)
        {
            this.Encoding = encoding;
            this.Writing = writting;
        }

        /// <summary>
        /// 编码
        /// </summary>
        public override Encoding Encoding { get; }

        /// <summary>
        /// 写入时触发
        /// </summary>
        public readonly Action<string>? Writing;

        /// <summary>
        /// 缓存
        /// </summary>
        private readonly StringBuilder Cache = new();

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(char value)
        {
            this.Cache.Append(value);
        }

        /// <summary>
        /// 输出
        /// </summary>
        public override void Flush()
        {
            string log = this.Cache.ToString();
            this.Cache.Clear();
            this.Writing?.Invoke(log);
        }
    }
}
