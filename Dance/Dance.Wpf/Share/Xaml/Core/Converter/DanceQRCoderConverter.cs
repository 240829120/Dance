using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Runtime.InteropServices;

namespace Dance.Wpf
{
    /// <summary>
    /// 二维码转化器
    /// </summary>
    public class DanceQRCoderConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string code)
                return null;

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20, System.Drawing.Color.Black, System.Drawing.Color.White, true);

            IntPtr hBitmap = qrCodeImage.GetHbitmap();
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hBitmap);

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns>是否删除成功</returns>
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
    }
}

