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
    /// 二维码控件
    /// </summary>
    public class DanceQRCoder : Image
    {
        static DanceQRCoder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceQRCoder), new FrameworkPropertyMetadata(typeof(DanceQRCoder)));
        }

        #region Code -- 二维码

        /// <summary>
        /// 二维码
        /// </summary>
        public string? Code
        {
            get { return (string?)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        /// <summary>
        /// 二维码
        /// </summary>
        public static readonly DependencyProperty CodeProperty =
            DependencyProperty.Register("Code", typeof(string), typeof(DanceQRCoder), new PropertyMetadata(null));

        #endregion
    }
}
