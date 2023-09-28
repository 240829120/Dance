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
    /// 绘制上下文扩展
    /// </summary>
    public static class DanceDrawingContextExpansion
    {
        /// <summary>
        /// 绘制清晰线条
        /// </summary>
        /// <param name="dc">绘制上下文</param>
        /// <param name="pen">画笔</param>
        /// <param name="lineThickness">线条宽度</param>
        /// <param name="points">点</param>
        public static void DrawSnappedLinesBetweenPoints(this DrawingContext dc, Pen pen, double lineThickness, params Point[] points)
        {
            var guidelineSet = new GuidelineSet();
            foreach (var point in points)
            {
                guidelineSet.GuidelinesX.Add(point.X);
                guidelineSet.GuidelinesY.Add(point.Y);
            }
            var half = lineThickness / 2;
            points = points.Select(p => new Point(p.X + half, p.Y + half)).ToArray();
            dc.PushGuidelineSet(guidelineSet);
            for (var i = 0; i < points.Length - 1; i += 2)
            {
                dc.DrawLine(pen, points[i], points[i + 1]);
            }
            dc.Pop();
        }
    }
}
