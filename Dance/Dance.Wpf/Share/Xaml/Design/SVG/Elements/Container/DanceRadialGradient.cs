using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// radialGradient 元素
    /// </summary>
    public class DanceRadialGradient : DanceSVGElement, IDanceSVGBrush
    {
        /// <summary>
        /// radialGradient 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceRadialGradient(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceRadialGradient(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取画刷
        /// </summary>
        /// <returns>画刷</returns>
        private Brush GetBrush()
        {
            double cx = this.GetAttributeValue<DanceSVGDouble>("cx", false, new DanceSVGDouble(0.5))?.GetValue(1) ?? 0.5;
            double cy = this.GetAttributeValue<DanceSVGDouble>("cy", false, new DanceSVGDouble(0.5))?.GetValue(1) ?? 0.5;
            double fx = this.GetAttributeValue<DanceSVGDouble>("fx", false, new DanceSVGDouble(0.5))?.GetValue(1) ?? 0.5;
            double fy = this.GetAttributeValue<DanceSVGDouble>("fy", false, new DanceSVGDouble(0.5))?.GetValue(1) ?? 0.5;
            double r = this.GetAttributeValue<DanceSVGDouble>("r", false, new DanceSVGDouble(0.5))?.GetValue(1) ?? 0.5;
            DanceSVGGradientUnits? gradientUnits = this.GetAttributeValue<DanceSVGGradientUnits>("gradientUnits");
            DanceSVGTransform? transform = this.GetAttributeValue<DanceSVGTransform>("gradientTransform");
            DanceSVGSpreadMethod? spreadMethod = this.GetAttributeValue<DanceSVGSpreadMethod>("spreadMethod");

            RadialGradientBrush brush = new()
            {
                Center = new System.Windows.Point(cx, cy),
                RadiusX = r,
                RadiusY = r,
                GradientOrigin = new System.Windows.Point(fx, fy)
            };

            foreach (DanceSVGElement item in this.Children)
            {
                if (item is not DanceStop stop)
                    continue;

                double offset = stop.GetAttributeValue<DanceSVGDouble>("offset")?.GetValue(1) ?? 0;
                double opacity = stop.GetAttributeValue<DanceSVGDouble>("stop-opacity", false, DanceSVGDouble.One)?.GetValue(1) ?? 1;
                DanceSVGColor? color = stop.GetAttributeValue<DanceSVGColor>("stop-color");

                GradientStop gs = new()
                {
                    Offset = offset
                };

                if (color != null)
                {
                    Color c = color.Value;
                    c.A = (byte)(opacity * 255);

                    gs.Color = c;
                }

                brush.GradientStops.Add(gs);
            }

            if (transform != null && transform.Value != null && transform.Value.Count > 0)
            {
                TransformGroup group = new();
                foreach (Transform item in transform.Value)
                {
                    group.Children.Add(item);
                }

                brush.Transform = group;
            }

            if (gradientUnits != null)
            {
                brush.MappingMode = gradientUnits.Value;
            }

            if (spreadMethod != null)
            {
                brush.SpreadMethod = spreadMethod.Value;
            }

            return brush;
        }

        private Brush? _value;

        /// <summary>
        /// 画刷
        /// </summary>
        public Brush? Value
        {
            get
            {
                this._value ??= this.GetBrush();

                return this._value;
            }
        }

    }
}
