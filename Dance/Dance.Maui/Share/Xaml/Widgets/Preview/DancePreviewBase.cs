using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 预览基类
    /// </summary>
    public abstract class DancePreviewBase : TemplatedView, IDancePreview
    {
        #region Source -- 源

        /// <summary>
        /// 源
        /// </summary>
        public string? Source
        {
            get => (string?)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// 源
        /// </summary>
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(DancePreviewBase), null);

        #endregion

        #region BackgroundSource -- 背景源

        /// <summary>
        /// 背景源
        /// </summary>
        public string? BackgroundSource
        {
            get => (string?)GetValue(BackgroundSourceProperty);
            set => SetValue(BackgroundSourceProperty, value);
        }

        /// <summary>
        /// 背景源
        /// </summary>
        public static readonly BindableProperty BackgroundSourceProperty =
            BindableProperty.Create(nameof(BackgroundSource), typeof(string), typeof(DancePreviewBase), null);

        #endregion

        #region Aspect -- 填充方式

        /// <summary>
        /// 填充方式
        /// </summary>
        public Aspect Aspect
        {
            get => (Aspect)GetValue(AspectProperty);
            set => SetValue(AspectProperty, value);
        }

        /// <summary>
        /// 填充方式
        /// </summary>
        public static readonly BindableProperty AspectProperty =
            BindableProperty.Create(nameof(Aspect), typeof(Aspect), typeof(DancePreviewBase), Aspect.AspectFill);

        #endregion
    }
}
