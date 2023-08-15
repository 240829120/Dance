using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 列表
    /// </summary>
    public class DanceCollectionView : TemplatedView
    {
        // ================================================================================================
        // Property

        #region SelectedItem -- 当前选中项

        /// <summary>
        /// 当前选中项
        /// </summary>
        public object? SelectedItem
        {
            get => (object?)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// 当前选中项
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(DanceCollectionView), null, BindingMode.TwoWay,
            validateValue: (obj, v) =>
            {
                if (obj is not DanceCollectionView collectionView)
                    return false;

                return v == null || collectionView.ItemsSource != null && collectionView.ItemsSource.Contains(v);
            }, propertyChanged: (obj, o, n) =>
            {
                if (obj is not DanceCollectionView collectionView)
                    return;

                collectionView.OnSelectedItemChagned(o, n);
            });

        #endregion

        #region ItemsSource -- 数据源

        /// <summary>
        /// 数据源
        /// </summary>
        public IList? ItemsSource
        {
            get => (IList?)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(DanceCollectionView), null);

        #endregion

        #region SelectionMode -- 选择模式

        /// <summary>
        /// 选择模式
        /// </summary>
        public SelectionMode SelectionMode
        {
            get => (SelectionMode)GetValue(SelectionModeProperty);
            set => SetValue(SelectionModeProperty, value);
        }

        /// <summary>
        /// 选择模式
        /// </summary>
        public static readonly BindableProperty SelectionModeProperty =
            BindableProperty.Create(nameof(SelectionMode), typeof(SelectionMode), typeof(DanceCollectionView), SelectionMode.Single, BindingMode.OneWay);

        #endregion

        #region ItemTemplate -- 项模板

        /// <summary>
        /// 选择模式
        /// </summary>
        public DataTemplate? ItemTemplate
        {
            get => (DataTemplate?)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// 选择模式
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(DanceCollectionView), null, BindingMode.OneWay);

        #endregion

        // ================================================================================================
        // Protected Function

        /// <summary>
        /// 当选中项发生改变时触发
        /// </summary>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected virtual void OnSelectedItemChagned(object oldValue, object newValue)
        {
            IReadOnlyList<DanceCollectionItem> items = this.GetVisualTreeDescendants<DanceCollectionItem>();
            foreach (DanceCollectionItem item in items)
            {
                item.IsSelected = item.BindingContext == newValue;
            }
        }
    }
}
