using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 图片
    /// </summary>
    public class DanceImage : BindableObject
    {
        #region Uri -- 地址

        /// <summary>
        /// 地址
        /// </summary>
        public Uri? Uri
        {
            get { return (Uri?)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public static readonly BindableProperty UriProperty =
            BindableProperty.Create(nameof(Uri), typeof(Uri), typeof(DanceImage), null);

        #endregion

        #region Tag -- 标志

        /// <summary>
        /// 标志
        /// </summary>
        public object? Tag
        {
            get { return (object?)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        /// <summary>
        /// 标志
        /// </summary>
        public static readonly BindableProperty TagProperty =
            BindableProperty.Create(nameof(Tag), typeof(object), typeof(DanceImage), null);

        #endregion
    }
}
