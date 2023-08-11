using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 列表项
    /// </summary>
    public class DanceCollectionItem : ContentView
    {
        public DanceCollectionItem()
        {
            TapGestureRecognizer tap = new();
            tap.Tapped += Tap_Tapped;
            this.GestureRecognizers.Add(tap);

            this.Loaded += LogicCollectionItem_Loaded;
        }

        // =======================================================================================
        // Field

        /// <summary>
        /// 所属LogicCollectionView
        /// </summary>
        protected DanceCollectionView? LogicCollectionView { get; set; }

        // =======================================================================================
        // Property

        #region IsSelected -- 是否选中

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(DanceCollectionItem), null, BindingMode.TwoWay);

        #endregion

        // =======================================================================================
        // Override

        // =======================================================================================
        // Private Function

        /// <summary>
        /// 加载完成
        /// </summary>
        private void LogicCollectionItem_Loaded(object? sender, EventArgs e)
        {
            this.LogicCollectionView = this.GetVisualElementParent<DanceCollectionView>();
        }

        /// <summary>
        /// 点击
        /// </summary>
        private void Tap_Tapped(object? sender, TappedEventArgs e)
        {
            if (this.LogicCollectionView == null)
                return;

            switch (this.LogicCollectionView.SelectionMode)
            {
                case SelectionMode.None: break;
                case SelectionMode.Single:
                    if (this.IsSelected)
                        break;
                    IReadOnlyList<DanceCollectionItem> items = this.LogicCollectionView.GetVisualElementChildren<DanceCollectionItem>();
                    foreach (DanceCollectionItem item in items)
                    {
                        item.IsSelected = this == item;
                    }
                    break;
                case SelectionMode.Multiple:
                    this.IsSelected = !this.IsSelected;
                    break;
            }
            this.LogicCollectionView.SelectedItem = this.BindingContext;
        }
    }
}
