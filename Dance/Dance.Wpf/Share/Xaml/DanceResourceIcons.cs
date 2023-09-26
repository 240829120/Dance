using System;
using System.Collections.Generic;
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
        public static ImageSource Failure { get; } = new BitmapImage(new Uri("/Dance.Wpf;component/Themes/Resources/Icon/failure.png", UriKind.RelativeOrAbsolute));

        /// <summary>
        /// 成功
        /// </summary>
        public static ImageSource Success { get; } = new BitmapImage(new Uri("/Dance.Wpf;component/Themes/Resources/Icon/success.png", UriKind.RelativeOrAbsolute));

        /// <summary>
        /// 警告
        /// </summary>
        public static ImageSource Warning { get; } = new BitmapImage(new Uri("/Dance.Wpf;component/Themes/Resources/Icon/warning.png", UriKind.RelativeOrAbsolute));
    }
}
