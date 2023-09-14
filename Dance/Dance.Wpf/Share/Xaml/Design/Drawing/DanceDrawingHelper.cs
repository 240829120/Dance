using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 绘制辅助类
    /// </summary>
    internal static class DanceDrawingHelper
    {
        /// <summary>
        /// 获取世界坐标
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>世界坐标</returns>
        internal static Point? GetWorldPoint(FrameworkElement element)
        {
            DanceDrawing? root = DanceXamlExpansion.GetVisualTreeParent<DanceDrawing>(element);
            if (root == null)
                return null;

            return element.TransformToAncestor(root).Transform(new Point());
        }

        /// <summary>
        /// 获取字体
        /// </summary>
        /// <returns>字体</returns>
        internal static System.Drawing.Font GetFont(TextBlock textBlock)
        {
            string fontFamily = textBlock.FontFamily.Source;
            float fontSize = (float)textBlock.FontSize;
            System.Drawing.FontStyle fontStyle = System.Drawing.FontStyle.Regular;
            if (textBlock.FontStyle == FontStyles.Italic)
            {
                fontStyle = System.Drawing.FontStyle.Italic;
            }
            if (textBlock.FontWeight == FontWeights.Bold)
            {
                fontStyle |= System.Drawing.FontStyle.Bold;
            }

            return new System.Drawing.Font(fontFamily, fontSize, fontStyle, System.Drawing.GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 获取画刷
        /// </summary>
        /// <param name="brush">画刷</param>
        /// <returns>画刷</returns>
        internal static System.Drawing.Brush GetBrush(Brush brush)
        {
            if (brush is not SolidColorBrush solidColorBrush)
                return System.Drawing.Brushes.Transparent;

            System.Drawing.Color color = System.Drawing.Color.FromArgb(solidColorBrush.Color.A, solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B);

            return new System.Drawing.SolidBrush(color);
        }
    }
}
