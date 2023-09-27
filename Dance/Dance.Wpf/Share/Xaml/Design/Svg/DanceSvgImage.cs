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
    /// Svg图片
    /// </summary>
    public class DanceSvgImage : Control
    {
        static DanceSvgImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceSvgImage), new FrameworkPropertyMetadata(typeof(DanceSvgImage)));
        }

        #region Uri -- 源

        /// <summary>
        /// 源
        /// </summary>
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// 源
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(DanceSvgImage), new PropertyMetadata(null));

        #endregion
    }
}
