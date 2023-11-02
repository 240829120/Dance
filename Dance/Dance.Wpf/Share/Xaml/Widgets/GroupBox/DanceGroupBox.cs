using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 分组列表
    /// </summary>
    public class DanceGroupBox : ItemsControl
    {
        static DanceGroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceGroupBox), new FrameworkPropertyMetadata(typeof(DanceGroupBox)));
        }

        #region GroupHeaderTemplate -- 分组头模板

        /// <summary>
        /// 分组头模板
        /// </summary>
        public DataTemplate GroupHeaderTemplate
        {
            get { return (DataTemplate)GetValue(GroupHeaderTemplateProperty); }
            set { SetValue(GroupHeaderTemplateProperty, value); }
        }

        /// <summary>
        /// 分组头模板
        /// </summary>
        public static readonly DependencyProperty GroupHeaderTemplateProperty =
            DependencyProperty.Register("GroupHeaderTemplate", typeof(DataTemplate), typeof(DanceGroupBox), new PropertyMetadata(null));

        #endregion

        #region GroupItemTemplate -- 分组项模板

        /// <summary>
        /// 分组项模板
        /// </summary>
        public DataTemplate GroupItemTemplate
        {
            get { return (DataTemplate)GetValue(GroupItemTemplateProperty); }
            set { SetValue(GroupItemTemplateProperty, value); }
        }

        /// <summary>
        /// 分组项模板
        /// </summary>
        public static readonly DependencyProperty GroupItemTemplateProperty =
            DependencyProperty.Register("GroupItemTemplate", typeof(DataTemplate), typeof(DanceGroupBox), new PropertyMetadata(null));

        #endregion

        #region GroupItemsSourcePath -- 分组项数据源属性

        /// <summary>
        /// 分组项数据源属性
        /// </summary>
        public string GroupItemsSourcePath
        {
            get { return (string)GetValue(GroupItemsSourcePathProperty); }
            set { SetValue(GroupItemsSourcePathProperty, value); }
        }

        /// <summary>
        /// 分组项数据源属性
        /// </summary>
        public static readonly DependencyProperty GroupItemsSourcePathProperty =
            DependencyProperty.Register("GroupItemsSourcePath", typeof(string), typeof(DanceGroupBox), new PropertyMetadata(null));

        #endregion

        #region GroupItemsPanel -- 分组项容器

        /// <summary>
        /// 分组项容器
        /// </summary>
        public ItemsPanelTemplate GroupItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(GroupItemsPanelProperty); }
            set { SetValue(GroupItemsPanelProperty, value); }
        }

        /// <summary>
        /// 分组项容器
        /// </summary>
        public static readonly DependencyProperty GroupItemsPanelProperty =
            DependencyProperty.Register("GroupItemsPanel", typeof(ItemsPanelTemplate), typeof(DanceGroupBox), new PropertyMetadata(null));

        #endregion

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DanceGroup;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DanceGroup();
        }
    }
}
