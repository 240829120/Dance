using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 变换分组
    /// </summary>
    public class DanceTransformGroupElement3D : GroupElement3D
    {
        #region InnerBounds -- 内置范围

        /// <summary>
        /// 内置范围
        /// </summary>
        public SharpDX.BoundingBox? InnerBounds
        {
            get { return (SharpDX.BoundingBox)GetValue(InnerBoundsProperty); }
            set { SetValue(InnerBoundsProperty, value); }
        }

        /// <summary>
        /// 内置范围
        /// </summary>
        public static readonly DependencyProperty InnerBoundsProperty =
            DependencyProperty.Register("InnerBounds", typeof(SharpDX.BoundingBox), typeof(DanceTransformGroupElement3D), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 获取变换范围
        /// </summary>
        /// <returns></returns>
        public SharpDX.BoundingBox GetTransformBounds()
        {
            return this.InnerBounds ?? new SharpDX.BoundingBox();
        }
    }
}
