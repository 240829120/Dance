using CommunityToolkit.Mvvm.ComponentModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 模型
    /// </summary>
    public class DanceModel : ObservableObject, IDisposable
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected readonly static ILog log = LogManager.GetLogger(typeof(DanceModel));

        /// <summary>
        /// 是否已经释放
        /// </summary>
        private bool IsDisposed = false;

        /// <summary>
        /// 析构
        /// </summary>
        ~DanceModel()
        {
            this.Destroy(false);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.Destroy(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing">是否执行销毁</param>
        protected void Destroy(bool disposing)
        {
            if (this.IsDisposed)
                return;

            if (disposing)
            {
                try
                {
                    this.Destroy();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }

            this.IsDisposed = true;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected virtual void Destroy()
        {

        }
    }
}