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
    /// 虚线基类
    /// </summary>
    public abstract class DanceDashedLineBase : Control
    {
        /// <summary>
        /// 画笔
        /// </summary>
        protected Pen StrokePen = new(Brushes.Black, 1d);

        #region Stroke -- 画刷

        /// <summary>
        /// 画刷
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// 画刷
        /// </summary>
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(DanceDashedLineBase), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceDashedLineBase line)
                    return;

                if (e.NewValue is not Brush brush)
                    return;

                line.StrokePen.Brush = brush;
            })));

        #endregion

        #region StrokeThickness -- 宽度

        /// <summary>
        /// 宽度
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(DanceDashedLineBase), new PropertyMetadata(1d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceDashedLineBase line)
                    return;

                if (e.NewValue is not double value)
                    return;

                line.StrokePen.Thickness = value;

            })));

        #endregion

        #region StrokeDashArray -- 虚线配置

        /// <summary>
        /// 虚线配置
        /// </summary>
        public DanceFloatCollection StrokeDashArray
        {
            get { return (DanceFloatCollection)GetValue(StrokeDashArrayProperty); }
            set { SetValue(StrokeDashArrayProperty, value); }
        }

        /// <summary>
        /// 虚线配置
        /// </summary>
        public static readonly DependencyProperty StrokeDashArrayProperty =
            DependencyProperty.Register("StrokeDashArray", typeof(DanceFloatCollection), typeof(DanceDashedLineBase), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceDashedLineBase line)
                    return;

                if (e.NewValue is not DanceFloatCollection collection)
                    return;

                line.StrokePen.DashStyle = new DashStyle(collection.Select(p => (double)p), 0);
            })));

        #endregion
    }
}
