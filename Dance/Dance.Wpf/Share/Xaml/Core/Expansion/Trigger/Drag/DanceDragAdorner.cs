using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 拖拽装饰器
    /// </summary>
    public partial class DanceDragAdorner : Adorner
    {
        /// <summary>
        /// 拖拽装饰器
        /// </summary>
        /// <param name="adornedElement">元素</param>
        /// <param name="background">背景</param>
        public DanceDragAdorner(UIElement adornedElement, Brush? background) : base(adornedElement)
        {
            this.IsHitTestVisible = false;
            this.VisualBrush = new VisualBrush(adornedElement);
            this.VisualPen = new Pen(Brushes.Transparent, 0);
            this.Background = background;
        }

        /// <summary>
        /// 虚拟画刷
        /// </summary>
        private readonly VisualBrush VisualBrush;

        /// <summary>
        /// 背景
        /// </summary>
        private readonly Brush? Background;

        /// <summary>
        /// 虚拟画笔
        /// </summary>
        private readonly Pen VisualPen;

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.AdornedElement == null || this.AdornedElement is not FrameworkElement element)
                return;

            POINT screenPos = new();
            if (!GetCursorPos(ref screenPos))
                return;

            Point pos = PointFromScreen(new Point(screenPos.X, screenPos.Y));
            Rect rect = new(pos.X, pos.Y, element.ActualWidth, element.ActualHeight);
            drawingContext.PushOpacity(1);
            if (this.Background != null)
            {
                drawingContext.DrawRectangle(this.Background, this.VisualPen, rect);
            }
            drawingContext.DrawRectangle(this.VisualBrush, this.VisualPen, rect);
            drawingContext.Pop();
        }

        public struct POINT { public Int32 X; public Int32 Y; }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetCursorPos(ref POINT point);
    }
}
