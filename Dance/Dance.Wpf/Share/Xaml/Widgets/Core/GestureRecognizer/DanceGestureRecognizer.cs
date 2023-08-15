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
    /// 手势识别控件
    /// </summary>
    public class DanceGestureRecognizer : ContentControl
    {
        #region GestureRecognizers -- 手势识别器集合

        /// <summary>
        /// 手势识别器集合
        /// </summary>
        public IList<IDanceGestureRecognizer> GestureRecognizers
        {
            get { return (IList<IDanceGestureRecognizer>)GetValue(GestureRecognizersProperty); }
            set { SetValue(GestureRecognizersProperty, value); }
        }

        /// <summary>
        /// 手势识别器集合
        /// </summary>
        public static readonly DependencyProperty GestureRecognizersProperty =
            DependencyProperty.Register("GestureRecognizers", typeof(IList<IDanceGestureRecognizer>), typeof(DanceGestureRecognizer), new PropertyMetadata(null));

        #endregion
    }
}
