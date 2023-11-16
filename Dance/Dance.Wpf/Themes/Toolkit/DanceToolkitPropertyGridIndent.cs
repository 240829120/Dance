using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Dance.Wpf
{
    /// <summary>
    /// 属性列表缩进
    /// </summary>
    public class DanceToolkitPropertyGridIndent : Border
    {
        public DanceToolkitPropertyGridIndent()
        {
            this.Loaded += DanceToolkitPropertyGridIndent_Loaded;
        }

        #region UnitWidth -- 单位宽度

        /// <summary>
        /// 单位宽度
        /// </summary>
        public double UnitWidth
        {
            get { return (double)GetValue(UnitWidthProperty); }
            set { SetValue(UnitWidthProperty, value); }
        }

        /// <summary>
        /// 单位宽度
        /// </summary>
        public static readonly DependencyProperty UnitWidthProperty =
            DependencyProperty.Register("UnitWidth", typeof(double), typeof(DanceToolkitPropertyGridIndent), new PropertyMetadata(15d));

        #endregion

        private void DanceToolkitPropertyGridIndent_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PropertyItemBase? parent = DanceXamlExpansion.GetVisualTreeParent<PropertyItemBase>(this);
            if (parent == null)
                return;

            this.Width = (parent.Level + 1) * this.UnitWidth;
        }
    }
}
