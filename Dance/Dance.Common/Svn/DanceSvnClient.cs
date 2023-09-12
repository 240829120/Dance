using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpSvn;

namespace Dance.Common
{
    /// <summary>
    /// SVN客户端
    /// </summary>
    public class DanceSvnClient : DanceObject, IDisposable
    {
        /// <summary>
        /// SVN客户端
        /// </summary>
        /// <param name="option">设置</param>
        public DanceSvnClient(DanceSvnClientOption option)
        {
            this.Option = option;

            this.SvnClient = new();
            this.SvnClient.Authentication.UserNamePasswordHandlers += Authentication_UserNamePasswordHandlers;

            DanceLogWritter writter = new(Encoding.UTF8, m => this.Logging?.Invoke(this, m));
            SvnClientReporter reporter = new(this.SvnClient, writter);
        }

        // =======================================================================================================
        // Field

        // =======================================================================================================
        // Event

        /// <summary>
        /// 日志
        /// </summary>
        public event EventHandler<string>? Logging;

        // =======================================================================================================
        // Property

        /// <summary>
        /// SVN客户端
        /// </summary>
        public SvnClient? SvnClient { get; private set; }

        /// <summary>
        /// 设置
        /// </summary>
        public DanceSvnClientOption Option { get; private set; }

        // =======================================================================================================
        // Public Function

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否操作成功</returns>
        public bool Update(string path)
        {
            return this.SvnClient?.Update(path) ?? false;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否操作成功</returns>
        public bool Add(string path)
        {
            return this.SvnClient?.Add(path) ?? false;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否操作成功</returns>
        public bool Commit(string path)
        {
            return this.SvnClient?.Commit(path) ?? false;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="path"></param>
        public void GetStatus(string path)
        {
            this.SvnClient?.GetStatus(path, out var list);
        }

        // =======================================================================================================
        // Protected Function

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.SvnClient?.Dispose();
            this.SvnClient = null;
        }

        // =======================================================================================================
        // Private Function

        /// <summary>
        /// 验证用户名密码
        /// </summary>
        private void Authentication_UserNamePasswordHandlers(object? sender, SharpSvn.Security.SvnUserNamePasswordEventArgs e)
        {
            e.UserName = this.Option.UserName;
            e.Password = this.Option.Password;
        }
    }
}
