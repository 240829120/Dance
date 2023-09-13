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
            this.SvnClient.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;

            this.Writter = new(Encoding.UTF8, m => this.Logging?.Invoke(this, m));
            this.Reporter = new(this.SvnClient, this.Writter);
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
        public SvnClient SvnClient { get; private set; }

        /// <summary>
        /// 日志
        /// </summary>
        public SvnClientReporter Reporter { get; private set; }

        /// <summary>
        /// 日志写入器
        /// </summary>
        public DanceLogWritter Writter { get; private set; }

        /// <summary>
        /// 设置
        /// </summary>
        public DanceSvnClientOption Option { get; private set; }

        // =======================================================================================================
        // Public Function

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns>是否操作成功</returns>
        public bool Update()
        {
            return this.SvnClient.Update(this.Option.Path);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否操作成功</returns>
        public bool Add(string path)
        {
            return this.SvnClient.Add(path);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns>是否操作成功</returns>
        public bool Commit()
        {
            return this.SvnClient.Commit(this.Option.Path);
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        public bool GetStatus(out Collection<SvnStatusEventArgs>? statuses)
        {
            statuses = null;
            return this.SvnClient.GetStatus(this.Option.Path, out statuses);
        }

        /// <summary>
        /// 检出
        /// </summary>
        public bool CheckOut()
        {
            return this.SvnClient.CheckOut(new(this.Option.Url), this.Option.Path);
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        public void FlushLog()
        {
            this.Writter.Flush();
        }

        // =======================================================================================================
        // Protected Function

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.SvnClient?.Dispose();
        }

        // =======================================================================================================
        // Private Function

        /// <summary>
        /// 证书认证
        /// </summary>
        private void Authentication_SslServerTrustHandlers(object? sender, SharpSvn.Security.SvnSslServerTrustEventArgs e)
        {
            e.AcceptedFailures = e.Failures;
            e.Save = true;
        }

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
