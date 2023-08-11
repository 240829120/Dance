using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 资源预览
    /// </summary>
    public class DanceResourcePreview : DancePreviewBase
    {
        #region PreviewType -- 预览类型

        /// <summary>
        /// 预览类型
        /// </summary>
        public DanceResourcePreviewType PreviewType
        {
            get => (DanceResourcePreviewType)GetValue(PreviewTypeProperty);
            set => SetValue(PreviewTypeProperty, value);
        }

        /// <summary>
        /// 预览类型
        /// </summary>
        public static readonly BindableProperty PreviewTypeProperty =
            BindableProperty.Create(nameof(PreviewType), typeof(DanceResourcePreviewType), typeof(DanceResourcePreview), DanceResourcePreviewType.Image);

        #endregion
    }
}
