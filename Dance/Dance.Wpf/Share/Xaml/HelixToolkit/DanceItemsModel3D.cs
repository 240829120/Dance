using HelixToolkit.SharpDX.Core.Model.Scene;
using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 3D子项模型
    /// </summary>
    public class DanceItemsModel3D : CompositeModel3D
    {
        public DanceItemsModel3D()
        {
            this.SceneNode.Attached += SceneNode_Attached;
            this.SceneNode.Detached += SceneNode_Detached;
        }

        // =========================================================================================================
        // Field

        /// <summary>
        /// 缓存字典
        /// </summary>
        private readonly Dictionary<object, Element3D> CacheDic = new();

        // =========================================================================================================
        // Property

        #region ItemTemplate -- 项模板

        /// <summary>
        /// 项模板
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// 项模板
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(DanceItemsModel3D), new PropertyMetadata(null));

        #endregion

        #region ItemTemplateSelector -- 项模板选择器

        /// <summary>
        /// 项模板选择器
        /// </summary>
        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// 项模板选择器
        /// </summary>
        public static readonly DependencyProperty ItemTemplateSelectorProperty =
            DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(DanceItemsModel3D), new PropertyMetadata(null));

        #endregion

        #region ItemsSource -- 源

        /// <summary>
        /// 源
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// 源
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(DanceItemsModel3D), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceItemsModel3D items)
                    return;

                items.ItemsSourceChanged(e.OldValue as IEnumerable, e.NewValue as IEnumerable);
            })));

        #endregion

        #region OctreeManager -- 命中测试管理器

        /// <summary>
        /// 命中测试管理器
        /// </summary>
        public IOctreeManagerWrapper OctreeManager
        {
            get { return (IOctreeManagerWrapper)GetValue(OctreeManagerProperty); }
            set { SetValue(OctreeManagerProperty, value); }
        }

        /// <summary>
        /// 命中测试管理器
        /// </summary>
        public static readonly DependencyProperty OctreeManagerProperty =
            DependencyProperty.Register("OctreeManager", typeof(IOctreeManagerWrapper), typeof(DanceItemsModel3D), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceItemsModel3D items)
                    return;

                if (e.OldValue != null)
                {
                    items.RemoveLogicalChild(e.OldValue);
                }

                if (e.NewValue != null)
                {
                    items.AddLogicalChild(e.NewValue);
                }

                if (items.SceneNode is GroupNode node && e.NewValue is IOctreeManagerWrapper wrapper)
                {
                    node.OctreeManager = wrapper.Manager;
                }

            })));

        #endregion

        // =========================================================================================================
        // Private Function

        /// <summary>
        /// 场景节点附加
        /// </summary>
        private void SceneNode_Attached(object? sender, EventArgs e)
        {
            this.Collection_CollectionChanged_Add(this.ItemsSource);

            if (this.Children.Count > 0 && this.SceneNode is GroupNode group)
            {
                group.OctreeManager?.RequestRebuild();
            }
        }

        /// <summary>
        /// 场景节点移除
        /// </summary>
        private void SceneNode_Detached(object? sender, EventArgs e)
        {
            this.Collection_CollectionChanged_Clear();
        }

        /// <summary>
        /// 源改变时触发
        /// </summary>
        /// <param name="oldValue">原始值</param>
        /// <param name="newValue">新值</param>
        private void ItemsSourceChanged(IEnumerable? oldValue, IEnumerable? newValue)
        {
            if (oldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= Collection_CollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged -= Collection_CollectionChanged;
                newCollection.CollectionChanged += Collection_CollectionChanged;
            }

            this.Collection_CollectionChanged_Clear();
            this.Collection_CollectionChanged_Add(newValue);

            if (this.Children.Count > 0 && this.SceneNode is GroupNode group)
            {
                group.OctreeManager?.RequestRebuild();
            }
        }

        /// <summary>
        /// 数据源列表改变时触发
        /// </summary>
        private void Collection_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Collection_CollectionChanged_Add(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Collection_CollectionChanged_Remove(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    this.Collection_CollectionChanged_Remove(e.OldItems);
                    this.Collection_CollectionChanged_Add(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Move:
                    this.Children.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.Collection_CollectionChanged_Clear();
                    this.Collection_CollectionChanged_Add(e.NewItems);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 列表改变 -- 清理
        /// </summary>
        private void Collection_CollectionChanged_Clear()
        {
            lock (this.CacheDic)
            {
                this.Children.Clear();
                this.CacheDic.Clear();
            }
        }

        /// <summary>
        /// 列表改变 -- 添加
        /// </summary>
        /// <param name="items">项集合</param>
        private void Collection_CollectionChanged_Add(IEnumerable? items)
        {
            if (items == null)
                return;

            lock (this.CacheDic)
            {
                foreach (object item in items)
                {
                    if (item is not IDanceModel3D model || this.CacheDic.ContainsKey(item))
                        continue;

                    DataTemplate? template = this.ItemTemplateSelector?.SelectTemplate(item, null) ?? this.ItemTemplate;
                    if (template == null)
                        continue;

                    if (template.LoadContent() is not Element3D element)
                        continue;

                    element.DataContext = item;
                    model.Element = element;
                    this.CacheDic.Add(item, element);
                    this.Children.Add(element);
                }
            }
        }

        /// <summary>
        /// 列表改变 -- 移除
        /// </summary>
        /// <param name="items">项集合</param>
        private void Collection_CollectionChanged_Remove(IEnumerable? items)
        {
            if (items == null)
                return;

            lock (this.CacheDic)
            {
                foreach (object item in items)
                {
                    if (!this.CacheDic.TryGetValue(item, out Element3D? element))
                        continue;

                    this.Children.Remove(element);
                    this.CacheDic.Remove(item);
                }
            }
        }
    }
}
