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
    /// 缩进
    /// </summary>
    public class DanceIndent : Control
    {
        public DanceIndent()
        {
            this.Loaded += DanceIndent_Loaded;
        }

        private void DanceIndent_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = this.Indent * this.UnitWidth;
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
            DependencyProperty.Register("UnitWidth", typeof(double), typeof(DanceIndent), new PropertyMetadata(30d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceIndent indent)
                    return;

                indent.Width = indent.Indent * indent.UnitWidth;

            })));

        #endregion

        #region Indent -- 缩进值

        /// <summary>
        /// 缩进值
        /// </summary>
        public int Indent
        {
            get { return (int)GetValue(IndentProperty); }
            set { SetValue(IndentProperty, value); }
        }

        /// <summary>
        /// 缩进值
        /// </summary>
        public static readonly DependencyProperty IndentProperty =
            DependencyProperty.Register("Indent", typeof(int), typeof(DanceIndent), new PropertyMetadata(0, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceIndent indent)
                    return;

                indent.Width = indent.Indent * indent.UnitWidth;

            })));

        #endregion
    }
}
