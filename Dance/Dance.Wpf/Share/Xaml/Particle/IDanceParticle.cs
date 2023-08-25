using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子
    /// </summary>
    public interface IDanceParticle : IDisposable
    {
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="size">可绘制区域</param>
        /// <param name="drawingContext">绘制上下文</param>
        void Draw(Size size, DrawingContext drawingContext);
    }
}
