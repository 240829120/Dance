using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 鼠标滑过
    /// </summary>
    public class DanceMouseOver : ContentControl
    {
        static DanceMouseOver()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceMouseOver), new FrameworkPropertyMetadata(typeof(DanceMouseOver)));
        }

        #region MouseOverBackground -- 鼠标滑过背景

        /// <summary>
        /// 鼠标滑过背景
        /// </summary>
        [TypeConverter(typeof(BrushConverter))]
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        /// <summary>
        /// 鼠标滑过背景
        /// </summary>
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(DanceMouseOver), new PropertyMetadata(null));

        #endregion
    }
}
