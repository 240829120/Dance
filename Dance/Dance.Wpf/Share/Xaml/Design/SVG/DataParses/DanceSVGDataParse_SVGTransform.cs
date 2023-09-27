using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// SVGDashArray
    /// </summary>
    public class DanceSVGDataParse_SVGTransform : DanceSVGDataParse<DanceSVGTransform>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(DanceSVGAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.Value))
                return true;

            DanceSVGTransform result = new();

            string[] components = attribute.Value.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < components.Length; ++i)
            {
                components[i] = components[i].Trim();
            }

            foreach (string item in components)
            {
                if (item.StartsWith("translate"))
                {
                    Excute_translate(result, item);
                }
                else if (item.StartsWith("scale"))
                {
                    Excute_scale(result, item);
                }
                else if (item.StartsWith("rotate"))
                {
                    Excute_rotate(result, item);
                }
                else if (item.StartsWith("skewX"))
                {
                    Excute_skewX(result, item);
                }
                else if (item.StartsWith("skewY"))
                {
                    Excute_skewY(result, item);
                }
                else if (item.StartsWith("matrix"))
                {
                    Excute_matrix(result, item);
                }
            }

            result.Value.Reverse();

            attribute.Data = result;

            return true;
        }

        /// <summary>
        /// 执行translate变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private static void Excute_translate(DanceSVGTransform transform, string item)
        {
            item = item.Replace("translate", "").Trim();
            item = item[1..];
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double x = 0;
            double y = 0;

            if (components.Length == 1)
            {
                x = double.Parse(components[0].Trim());
            }
            else if (components.Length == 2)
            {
                x = double.Parse(components[0].Trim());
                y = double.Parse(components[1].Trim());
            }

            TranslateTransform t = new()
            {
                X = x,
                Y = y
            };

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行scale变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private static void Excute_scale(DanceSVGTransform transform, string item)
        {
            item = item.Replace("scale", "").Trim();
            item = item[1..];
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double x;
            double y;

            if (components.Length == 1)
            {
                x = double.Parse(components[0].Trim());
                y = x;
            }
            else
            {
                x = double.Parse(components[0].Trim());
                y = double.Parse(components[1].Trim());
            }

            ScaleTransform t = new()
            {
                CenterX = 0.5d,
                CenterY = 0.5d,
                ScaleX = x,
                ScaleY = y
            };

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行rotate变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private static void Excute_rotate(DanceSVGTransform transform, string item)
        {
            item = item.Replace("rotate", "").Trim();
            item = item[1..];
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double centerX = 0.5d;
            double centerY = 0.5d;
            double angle = 0d;

            if (components.Length == 1)
            {
                angle = double.Parse(components[0].Trim());
            }
            else if (components.Length == 3)
            {
                angle = double.Parse(components[0].Trim());
                centerX = double.Parse(components[1].Trim());
                centerY = double.Parse(components[2].Trim());
            }

            RotateTransform t = new()
            {
                CenterX = centerX,
                CenterY = centerY,
                Angle = angle
            };

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行skewX变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private static void Excute_skewX(DanceSVGTransform transform, string item)
        {
            item = item.Replace("skewX", "").Trim();
            item = item[1..];
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double angle = 0d;

            if (components.Length == 1)
            {
                angle = double.Parse(components[0].Trim());
            }

            SkewTransform t = new()
            {
                AngleX = angle
            };

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行skewY变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private static void Excute_skewY(DanceSVGTransform transform, string item)
        {
            item = item.Replace("skewY", "").Trim();
            item = item[1..];
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double angle = 0d;

            if (components.Length == 1)
            {
                angle = double.Parse(components[0].Trim());
            }

            SkewTransform t = new()
            {
                AngleY = angle
            };

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行matrix变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private static void Excute_matrix(DanceSVGTransform transform, string item)
        {
            item = item.Replace("matrix", "").Trim();
            item = item[1..];
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double m11 = 0d;
            double m12 = 0d;
            double m21 = 0d;
            double m22 = 0d;
            double offsetX = 0d;
            double offsetY = 0d;

            if (components.Length == 6)
            {
                m11 = double.Parse(components[0].Trim());
                m12 = double.Parse(components[1].Trim());
                m21 = double.Parse(components[2].Trim());
                m22 = double.Parse(components[3].Trim());
                offsetX = double.Parse(components[4].Trim());
                offsetY = double.Parse(components[5].Trim());
            }

            MatrixTransform t = new(m11, m12, m21, m22, offsetX, offsetY);

            transform.Value.Add(t);
        }
    }
}
