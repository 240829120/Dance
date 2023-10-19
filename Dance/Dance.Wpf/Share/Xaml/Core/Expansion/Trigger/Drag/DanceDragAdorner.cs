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
        public DanceDragAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.IsHitTestVisible = false;
            this.VisualBrush = new VisualBrush(adornedElement);
            this.VisualPen = new Pen(Brushes.Transparent, 0);
        }

        /// <summary>
        /// 虚拟画刷
        /// </summary>
        private readonly VisualBrush VisualBrush;

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
            Rect rect = new(pos.X / DanceXamlExpansion.DpiScale.DpiScaleX, pos.Y / DanceXamlExpansion.DpiScale.DpiScaleY, element.ActualWidth, element.ActualHeight);
            drawingContext.PushOpacity(1);
            drawingContext.DrawRectangle(this.VisualBrush, this.VisualPen, rect);
            drawingContext.Pop();
        }

        public struct POINT { public Int32 X; public Int32 Y; }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetCursorPos(ref POINT point);
    }
}
