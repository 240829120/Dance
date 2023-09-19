using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class DancePaginationInfo : DanceModel
    {
        #region PageNum -- 页码

        private int? pageNum;
        /// <summary>
        /// 页码
        /// </summary>
        public int? PageNum
        {
            get { return pageNum; }
            set { pageNum = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Type -- 类型

        private DancePaginationInfoType type;
        /// <summary>
        /// 类型
        /// </summary>
        public DancePaginationInfoType Type
        {
            get { return type; }
            set { type = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsSelected -- 是否选中

        private bool isSelected;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsEnabled -- 是否启用

        private bool isEnabled = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
