using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 图片粒子定义
    /// </summary>
    public class DanceParticleImageDefine : DependencyObject
    {
        #region Uri -- 地址

        /// <summary>
        /// 地址
        /// </summary>
        [TypeConverter(typeof(UriTypeConverter))]
        public Uri Uri
        {
            get { return (Uri)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register("Uri", typeof(Uri), typeof(DanceParticleImageDefine), new PropertyMetadata(null));

        #endregion

        #region SKImage -- 图片

        /// <summary>
        /// 图片
        /// </summary>
        public SKImage? SKImage
        {
            get { return (SKImage)GetValue(SKImageProperty); }
            set { SetValue(SKImageProperty, value); }
        }

        /// <summary>
        /// 图片
        /// </summary>
        public static readonly DependencyProperty SKImageProperty =
            DependencyProperty.Register("SKImage", typeof(SKImage), typeof(DanceParticleImageDefine), new PropertyMetadata(null));

        #endregion
    }
}
