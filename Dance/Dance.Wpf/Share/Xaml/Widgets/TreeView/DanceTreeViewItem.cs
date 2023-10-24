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
    /// 树节点
    /// </summary>
    public class DanceTreeViewItem : TreeViewItem
    {
        static DanceTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceTreeViewItem), new FrameworkPropertyMetadata(typeof(DanceTreeViewItem)));
        }

        #region Level -- 等级

        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register("Level", typeof(int), typeof(DanceTreeViewItem), new PropertyMetadata(0));

        #endregion

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is DanceTreeViewItem node)
            {
                node.Level = this.Level + 1;
                return true;
            }

            return false;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceTreeViewItem() { Level = this.Level + 1 };
        }
    }
}
