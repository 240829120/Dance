using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 分页
    /// </summary>
    public class DancePagination : Control
    {
        static DancePagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DancePagination), new FrameworkPropertyMetadata(typeof(DancePagination)));
        }

        #region PageNum -- 页数

        /// <summary>
        /// 页数
        /// </summary>
        public int PageNum
        {
            get { return (int)GetValue(PageNumProperty); }
            set { SetValue(PageNumProperty, value); }
        }

        /// <summary>
        /// 页数
        /// </summary>
        public static readonly DependencyProperty PageNumProperty =
            DependencyProperty.Register("PageNum", typeof(int), typeof(DancePagination), new PropertyMetadata(1, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePagination pagination)
                    return;

                pagination.ResetItemsSource();
            })));

        #endregion

        #region PageTotal -- 总页数

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal
        {
            get { return (int)GetValue(PageTotalProperty); }
            set { SetValue(PageTotalProperty, value); }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public static readonly DependencyProperty PageTotalProperty =
            DependencyProperty.Register("PageTotal", typeof(int), typeof(DancePagination), new PropertyMetadata(0, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePagination pagination)
                    return;

                pagination.ResetItemsSource();
            })));

        #endregion

        #region PageMoreNum -- 更多页数

        /// <summary>
        /// 更多页数
        /// </summary>
        public int PageMoreNum
        {
            get { return (int)GetValue(PageMoreNumProperty); }
            set { SetValue(PageMoreNumProperty, value); }
        }

        /// <summary>
        /// 更多页数
        /// </summary>
        public static readonly DependencyProperty PageMoreNumProperty =
            DependencyProperty.Register("PageMoreNum", typeof(int), typeof(DancePagination), new PropertyMetadata(5, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePagination pagination)
                    return;

                pagination.ResetItemsSource();
            })));

        #endregion

        #region ItemsSource -- 数据源

        /// <summary>
        /// 数据源
        /// </summary>
        public List<DancePaginationInfo> ItemsSource
        {
            get { return (List<DancePaginationInfo>)GetValue(ItemsSourcePropertyKey.DependencyProperty); }
            set { SetValue(ItemsSourcePropertyKey, value); }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly DependencyPropertyKey ItemsSourcePropertyKey =
            DependencyProperty.RegisterReadOnly("ItemsSource", typeof(List<DancePaginationInfo>), typeof(DancePagination), new PropertyMetadata(null));

        #endregion

        #region GotoPageNum -- 跳转页码

        /// <summary>
        /// 跳转页码
        /// </summary>
        public string GotoPageNum
        {
            get { return (string)GetValue(GotoPageNumProperty); }
            set { SetValue(GotoPageNumProperty, value); }
        }

        /// <summary>
        /// 跳转页码
        /// </summary>
        public static readonly DependencyProperty GotoPageNumProperty =
            DependencyProperty.Register("GotoPageNum", typeof(string), typeof(DancePagination), new PropertyMetadata(null));

        #endregion

        #region AllowGoto -- 允许跳转

        /// <summary>
        /// 允许跳转
        /// </summary>
        public bool AllowGoto
        {
            get { return (bool)GetValue(AllowGotoProperty); }
            set { SetValue(AllowGotoProperty, value); }
        }

        /// <summary>
        /// 允许跳转
        /// </summary>
        public static readonly DependencyProperty AllowGotoProperty =
            DependencyProperty.Register("AllowGoto", typeof(bool), typeof(DancePagination), new PropertyMetadata(false));

        #endregion

        /// <summary>
        /// 切换页
        /// </summary>
        /// <param name="info">分页信息</param>
        internal void ChangePage(DancePaginationInfo info)
        {
            switch (info.Type)
            {
                case DancePaginationInfoType.Page:
                    this.PageNum = info.PageNum ?? 1;
                    break;
                case DancePaginationInfoType.PreviousPage:
                    this.PageNum = this.PageNum - 1 <= 1 ? 1 : this.PageNum - 1;
                    break;
                case DancePaginationInfoType.PreviousMorePage:
                    this.PageNum = this.PageNum - this.PageMoreNum <= 1 ? 1 : this.PageNum - this.PageMoreNum;
                    break;
                case DancePaginationInfoType.NextPage:
                    this.PageNum = this.PageNum + 1 >= this.PageTotal ? this.PageTotal : this.PageNum + 1;
                    break;
                case DancePaginationInfoType.NextMorePage:
                    this.PageNum = this.PageNum + this.PageMoreNum >= this.PageTotal ? this.PageTotal : this.PageNum + this.PageMoreNum;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 跳转
        /// </summary>
        public void Goto()
        {
            _ = int.TryParse(this.GotoPageNum, out int num);
            this.PageNum = Math.Clamp(num, 1, this.PageTotal);
        }

        /// <summary>
        /// 重置数据源
        /// </summary>
        private void ResetItemsSource()
        {
            int start = this.PageNum;
            int end = this.PageNum;

            while (end - start + 1 < this.PageMoreNum)
            {
                if (start > 1)
                    --start;

                if (end < this.PageTotal)
                    ++end;

                if (start == 1 && end == PageTotal)
                    break;
            }

            List<DancePaginationInfo> items =
            [
                // 上一页
                new() { Type = DancePaginationInfoType.PreviousPage, IsEnabled = this.PageNum > 1 }
            ];

            // 第一页
            if (start >= this.PageMoreNum / 2)
            {
                items.Add(new DancePaginationInfo { Type = DancePaginationInfoType.Page, PageNum = 1 });
            }

            // 上更多页
            if (start >= 3)
            {
                items.Add(new() { Type = DancePaginationInfoType.PreviousMorePage });
            }

            // 页选项
            for (int i = start; i <= end; i++)
            {
                items.Add(new() { Type = DancePaginationInfoType.Page, PageNum = i, IsSelected = i == this.PageNum });
            }

            // 下更多页
            if (end <= this.PageTotal - 2)
            {
                items.Add(new() { Type = DancePaginationInfoType.NextMorePage });
            }

            // 最后一页
            if (end <= this.PageTotal - this.PageMoreNum / 2 + 1)
            {
                items.Add(new DancePaginationInfo { Type = DancePaginationInfoType.Page, PageNum = this.PageTotal });
            }

            // 下一页
            items.Add(new() { Type = DancePaginationInfoType.NextPage, IsEnabled = this.PageNum < this.PageTotal });

            this.ItemsSource = items;
        }
    }
}
