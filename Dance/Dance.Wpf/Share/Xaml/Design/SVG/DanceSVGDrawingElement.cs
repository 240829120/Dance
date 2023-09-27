using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// SVG可渲染元素
    /// </summary>
    public abstract class DanceSVGDrawingElement : DanceSVGElement
    {
        /// <summary>
        /// 元素
        /// </summary>
        /// <param name="svg">根元素</param>
        /// <param name="parent">父级元素</param>
        /// <param name="element">当前元素</param>
        public DanceSVGDrawingElement(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected abstract Geometry? GetBaseGeometry();

        /// <summary>
        /// 获取几何图形
        /// </summary>
        /// <returns>几何图形</returns>
        public virtual Geometry? GetGeometry()
        {
            DanceSVGDisplay? display = this.GetAttributeValue<DanceSVGDisplay>("display");
            if (display == DanceSVGDisplay.None)
            {
                return null;
            }

            if (this.GetBaseGeometry() is not Geometry geometry)
                return null;

            DanceSVGTransform? transform = this.GetAttributeValue<DanceSVGTransform>("transform");

            if (transform != null && transform.Value != null && transform.Value.Count > 0)
            {
                TransformGroup group = GetTransformGroup(geometry);

                foreach (Transform item in transform.Value)
                {
                    group.Children.Add(item);
                }

                geometry.Transform = group;
            }

            DanceSVGClipPath? clip_path = this.GetAttributeValue<DanceSVGClipPath>("clip-path");

            if (clip_path != null && clip_path.Value != null)
            {
                geometry = Geometry.Combine(geometry, clip_path.Value, GeometryCombineMode.Intersect, null);
            }

            return geometry;
        }

        /// <summary>
        /// 获取基础渲染
        /// </summary>
        /// <returns>基础渲染</returns>
        protected virtual Drawing? GetBaseDrawing()
        {
            if (this.GetGeometry() is not Geometry geometry || geometry.IsEmpty())
                return null;

            Brush? fill_brush = this.GetFillBrush();
            Pen? pen = this.GetPen();

            if (fill_brush == null && pen == null)
                return null;

            if (fill_brush != null && geometry.GetArea() > 0)
            {
                DanceSVGFillRule? fill_rule = this.GetAttributeValue<DanceSVGFillRule>("fill-rule", true, DanceSVGFillRule.Nonzero);

                PathGeometry path_geometry = Geometry.Combine(geometry, Geometry.Empty, GeometryCombineMode.Exclude, null);
                path_geometry.FillRule = fill_rule?.Value ?? FillRule.EvenOdd;
                geometry = path_geometry;
            }

            GeometryDrawing result = new(fill_brush, pen, geometry);

            return result;
        }

        /// <summary>
        /// 获取渲染
        /// </summary>
        /// <returns>渲染</returns>
        public virtual Drawing? GetDrawing()
        {
            if (this.GetBaseDrawing() is not Drawing drawing)
                return null;

            DanceSVGMask? mask = this.GetAttributeValue<DanceSVGMask>("mask");

            if (mask != null && mask.Value != null)
            {
                DanceSVGTransform? transform = this.GetAttributeValue<DanceSVGTransform>("transform");

                DrawingGroup group = new()
                {
                    OpacityMask = mask.Value
                };

                if (transform != null && transform.Value != null && transform.Value.Count > 0)
                {
                    TransformGroup transform_group = GetTransformGroup(group.OpacityMask);

                    foreach (Transform item in transform.Value)
                    {
                        transform_group.Children.Add(item);
                    }
                }

                group.Children.Add(drawing);
                drawing = group;
            }

            return drawing;
        }

        /// <summary>
        /// 获取填充画刷
        /// </summary>
        /// <returns></returns>
        protected Brush? GetFillBrush()
        {
            Brush? fill_brush;

            if (this is DanceText)
            {
                fill_brush = this.GetAttributeValue<DanceSVGBrush>("fill", true, DanceSVGBrush.Black)?.GetValue();
            }
            else
            {
                fill_brush = this.GetAttributeValue<DanceSVGBrush>("fill", false, null)?.GetValue();
            }

            if (fill_brush == null)
                return null;

            double fill_opacity = this.GetAttributeValue<DanceSVGDouble>("fill-opacity", false, DanceSVGDouble.One)?.GetValue(1) ?? 1;
            double opacity = this.GetAttributeValue<DanceSVGDouble>("opacity", false, DanceSVGDouble.One)?.GetValue(1) ?? 1;

            fill_brush.Opacity = fill_opacity * opacity;

            return fill_brush;
        }

        /// <summary>
        /// 获取边框线
        /// </summary>
        /// <returns>边框线</returns>
        protected Pen? GetPen()
        {
            Brush? stroke_brush = null;
            double stroke_width = 0d;

            if (this is DanceText)
            {
                stroke_brush = this.GetAttributeValue<DanceSVGBrush>("stroke", true, DanceSVGBrush.Black)?.GetValue();
                stroke_width = this.GetAttributeValue<DanceSVGDouble>("stroke-width", true, new DanceSVGDouble(0.1))?.GetValue() ?? 0.1;
            }
            else
            {
                stroke_brush = this.GetAttributeValue<DanceSVGBrush>("stroke", false, null)?.GetValue();
                stroke_width = this.GetAttributeValue<DanceSVGDouble>("stroke-width", false, DanceSVGDouble.One)?.GetValue() ?? 1;
            }

            if (stroke_brush == null || stroke_width == 0d)
                return null;

            double stroke_opacity = this.GetAttributeValue<DanceSVGDouble>("stroke-opacity", true, DanceSVGDouble.One)?.GetValue(1) ?? 1;
            double opacity = this.GetAttributeValue<DanceSVGDouble>("opacity", false, DanceSVGDouble.One)?.GetValue(1) ?? 1;

            double stroke_miterlimit = this.GetAttributeValue<DanceSVGDouble>("stroke-miterlimit", true, new DanceSVGDouble(4))?.GetValue() ?? 4;
            DanceSVGDashArray? stroke_dasharray = this.GetAttributeValue<DanceSVGDashArray>("stroke-dasharray", true, null);
            DanceSVGLineCap? stroke_linecap = this.GetAttributeValue<DanceSVGLineCap>("stroke-linecap", true, null);
            DanceSVGLineJoin? stroke_linejoin = this.GetAttributeValue<DanceSVGLineJoin>("stroke-linejoin", true, null);

            stroke_brush.Opacity = stroke_opacity * opacity;

            Pen pen = new(stroke_brush, stroke_width)
            {
                MiterLimit = stroke_miterlimit
            };

            if (stroke_dasharray != null && stroke_dasharray.Value != null && stroke_dasharray.Value.Count > 0)
            {
                double scale = 1d / stroke_width;
                DashStyle dash_style = new(stroke_dasharray.Value.Select(p => p * scale), 0);
                pen.DashStyle = dash_style;
            }
            if (stroke_linecap != null)
            {
                pen.StartLineCap = stroke_linecap.Value;
                pen.EndLineCap = stroke_linecap.Value;
            }
            if (stroke_linejoin != null)
            {
                pen.LineJoin = stroke_linejoin.Value;
            }

            return pen;
        }

        /// <summary>
        /// 获取变换组
        /// </summary>
        /// <param name="geometry">路径组</param>
        /// <returns>变换组</returns>
        protected static TransformGroup GetTransformGroup(Geometry geometry)
        {
            TransformGroup result;

            if (geometry.Transform is TransformGroup transformGroup)
            {
                result = transformGroup;
            }
            else
            {
                result = new TransformGroup();
                geometry.Transform = result;
            }

            return result;
        }

        /// <summary>
        /// 获取变换组
        /// </summary>
        /// <param name="group">绘制组</param>
        /// <returns>变换组</returns>
        protected static TransformGroup GetTransformGroup(DrawingGroup group)
        {
            TransformGroup result;

            if (group.Transform is TransformGroup transformGroup)
            {
                result = transformGroup;
            }
            else
            {
                result = new TransformGroup();
                group.Transform = result;
            }

            return result;
        }

        /// <summary>
        /// 获取变换组
        /// </summary>
        /// <param name="brush">画刷</param>
        /// <returns>变换组</returns>
        protected static TransformGroup GetTransformGroup(Brush brush)
        {
            TransformGroup result;

            if (brush.Transform is TransformGroup transformGroup)
            {
                result = transformGroup;
            }
            else
            {
                result = new TransformGroup();
                brush.Transform = result;
            }

            return result;
        }
    }
}
