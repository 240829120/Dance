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

        public DanceTreeViewItem()
        {
            this.Loaded += DanceTreeViewItem_Loaded;
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

        /// <summary>
        /// 加载
        /// </summary>
        private void DanceTreeViewItem_Loaded(object sender, RoutedEventArgs e)
        {
            DanceTreeViewItem? parent = DanceXamlExpansion.GetVisualTreeParent<DanceTreeViewItem>(this);
            if (parent == null)
                return;

            this.Level = parent.Level + 1;
        }
    }
}
