using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dance.Wpf
{
    /// <summary>
    /// 图标资源
    /// </summary>
    public static class DanceResourceIcons
    {
        /// <summary>
        /// 失败
        /// </summary>
        [NotNull]
        public static ImageSource? Failure { get; } = DanceXamlExpansion.GetSvgImageSource(new Uri("pack://application:,,,/Dance.Wpf;component/Themes/Resources/Icon/failure.svg", UriKind.RelativeOrAbsolute));

        /// <summary>
        /// 成功
        /// </summary>
        [NotNull]
        public static ImageSource? Success { get; } = DanceXamlExpansion.GetSvgImageSource(new Uri("pack://application:,,,/Dance.Wpf;component/Themes/Resources/Icon/success.svg", UriKind.RelativeOrAbsolute));

        /// <summary>
        /// 警告
        /// </summary>
        [NotNull]
        public static ImageSource? Warning { get; } = DanceXamlExpansion.GetSvgImageSource(new Uri("pack://application:,,,/Dance.Wpf;component/Themes/Resources/Icon/warning.svg", UriKind.RelativeOrAbsolute));

        /// <summary>
        /// 信息
        /// </summary>
        [NotNull]
        public static ImageSource? Info { get; } = DanceXamlExpansion.GetSvgImageSource(new Uri("pack://application:,,,/Dance.Wpf;component/Themes/Resources/Icon/info.svg", UriKind.RelativeOrAbsolute));
    }
}
