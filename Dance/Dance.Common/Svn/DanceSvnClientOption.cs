using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Common
{
    /// <summary>
    /// Svn设置
    /// </summary>
    public class DanceSvnClientOption
    {
        /// <summary>
        /// Svn设置
        /// </summary>
        /// <param name="path">路径</param>
        public DanceSvnClientOption(string path)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path);

            this.Path = path;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; private set; }
    }
}
