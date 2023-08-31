using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 图片粒子定义
    /// </summary>
    public class DanceParticleImageDefine : BindableObject
    {
        #region AssemblyName -- 程序集名称

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName
        {
            get { return (string)GetValue(AssemblyNameProperty); }
            set { SetValue(AssemblyNameProperty, value); }
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public static readonly BindableProperty AssemblyNameProperty =
            BindableProperty.Create(nameof(AssemblyName), typeof(string), typeof(DanceParticleImageDefine), null);

        #endregion

        #region Uri -- 地址

        /// <summary>
        /// 地址
        /// </summary>
        [TypeConverter(typeof(Microsoft.Maui.Controls.UriTypeConverter))]
        public Uri Uri
        {
            get { return (Uri)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public static readonly BindableProperty UriProperty =
            BindableProperty.Create(nameof(Uri), typeof(Uri), typeof(DanceParticleImageDefine), null);

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
        public static readonly BindableProperty SKImageProperty =
            BindableProperty.Create(nameof(SKImage), typeof(SKImage), typeof(DanceParticleImageDefine), null);

        #endregion
    }
}
