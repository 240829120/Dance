using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 绘制数据项
    /// </summary>
    public class DanceDrawingItemsControl : ItemsControl, IDanceDrawing
    {
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="graphics">绘制上下文</param>
        public void OnDrawing(Graphics graphics)
        {
            if (DanceDrawingHelper.GetWorldPoint(this) is not System.Windows.Point point)
                return;

            graphics.FillRectangle(DanceDrawingHelper.GetBrush(this.Background), (float)point.X, (float)point.Y, (float)this.ActualWidth, (float)this.ActualHeight);
        }
    }
}
