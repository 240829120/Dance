using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 绘制
    /// </summary>
    public interface IDanceDrawing
    {
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="graphics">绘制上下文</param>
        void OnDrawing(Graphics graphics);
    }
}
