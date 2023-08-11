using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 视图模型
    /// </summary>
    public class DanceViewModel : DanceModel
    {
        #region View -- 视图

        private WeakReference? view;

        /// <summary>
        /// 视图
        /// </summary>
        public object? View
        {
            get
            {
                return view?.Target;
            }
            set
            {
                view = new WeakReference(value);
                this.OnPropertyChanged();
            }
        }

        #endregion
    }
}
