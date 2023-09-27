using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Dance.Wpf
{
    /// <summary>
    /// 绘制二维码
    /// </summary>
    public class DanceDrawingQRCoder : DanceQRCoder, IDanceDrawing
    {
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="graphics">绘制上下文</param>
        public void OnDrawing(Graphics graphics)
        {
            if (this.Source == null)
                return;

            if (DanceDrawingHelper.GetWorldPoint(this) is not System.Windows.Point point)
                return;

            using MemoryStream ms = new();
            BmpBitmapEncoder encoder = new();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)this.Source));
            encoder.Save(ms);

            Bitmap bmp = new(ms);
            ms.Close();

            graphics.DrawImage(bmp, new RectangleF((float)point.X, (float)point.Y, (float)this.ActualWidth, (float)this.ActualHeight));
        }
    }
}
