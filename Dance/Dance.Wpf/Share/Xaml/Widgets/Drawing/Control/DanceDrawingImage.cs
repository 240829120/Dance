using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace Dance.Wpf
{
    /// <summary>
    /// 绘制图片
    /// </summary>
    public class DanceDrawingImage : System.Windows.Controls.Image, IDanceDrawing
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
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)this.Source));
            encoder.Save(ms);

            Bitmap bmp = new(ms);
            ms.Close();

            graphics.DrawImage(bmp, new RectangleF((float)point.X, (float)point.Y, (float)this.ActualWidth, (float)this.ActualHeight));
        }
    }
}
