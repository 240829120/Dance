using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 绘制控件
    /// </summary>
    public class DanceDrawing : ContentControl, IDanceDrawing
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly static ILog log = LogManager.GetLogger(typeof(DanceDrawing));

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="graphics">绘制上下文</param>
        public void OnDrawing(Graphics graphics)
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DanceXamlExpansion.TraversalVisualTree(this, e =>
            {
                if (e == this || e is not IDanceDrawing drawing)
                    return;

                drawing.OnDrawing(graphics);
            });
        }

        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="format">图片格式</param>
        public Task CaptureScreenAsync(string path, System.Drawing.Imaging.ImageFormat format)
        {
            return Task.Run(() =>
            {
                bool hock = false;

                this.Dispatcher.BeginInvoke(() =>
                {
                    try
                    {
                        Bitmap bmp = new((int)this.ActualWidth, (int)this.ActualHeight);
                        Graphics g = Graphics.FromImage(bmp);
                        this.OnDrawing(g);

                        bmp.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                    finally
                    {
                        hock = true;
                    }

                }, System.Windows.Threading.DispatcherPriority.SystemIdle);

                while (!hock)
                {
                    Task.Delay(100).Wait();
                }
            });
        }
    }
}
