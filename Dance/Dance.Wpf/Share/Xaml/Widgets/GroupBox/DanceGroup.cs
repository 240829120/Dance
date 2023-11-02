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
    /// 分组
    /// </summary>
    public class DanceGroup : ItemsControl
    {
        static DanceGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceGroup), new FrameworkPropertyMetadata(typeof(DanceGroup)));
        }

        #region IsDragOver -- 拖拽经过

        /// <summary>
        /// 拖拽经过
        /// </summary>
        public bool IsDragOver
        {
            get { return (bool)GetValue(IsDragOverProperty); }
            set { SetValue(IsDragOverProperty, value); }
        }

        /// <summary>
        /// 拖拽经过
        /// </summary>
        public static readonly DependencyProperty IsDragOverProperty =
            DependencyProperty.Register("IsDragOver", typeof(bool), typeof(DanceGroup), new PropertyMetadata(false));

        #endregion

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceGroupItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceGroupItem();
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            this.IsDragOver = true;
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);

            this.IsDragOver = false;
        }
    }
}
