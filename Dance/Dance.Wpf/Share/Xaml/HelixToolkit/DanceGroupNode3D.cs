using HelixToolkit.SharpDX.Core;
using HelixToolkit.SharpDX.Core.Model.Scene;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 节点分组
    /// </summary>
    public class DanceGroupNode3D : GroupNode
    {
        /// <summary>
        /// 节点分组
        /// </summary>
        /// <param name="element">元素</param>
        public DanceGroupNode3D(Element3D element)
        {
            this.element = element;
        }

        #region element -- 所属元素

        private readonly Element3D element;
        /// <summary>
        /// 所属元素
        /// </summary>
        public Element3D Element
        {
            get { return element; }
        }

        #endregion

        #region HasBounds -- 是否拥有边界

        private bool hasBounds;
        /// <summary>
        /// 是否拥有边界
        /// </summary>
        public bool HasBounds
        {
            get { return hasBounds; }
        }

        #endregion

        #region Bounds -- 边界

        private BoundingBox bounds;
        /// <summary>
        /// 边界
        /// </summary>
        public override BoundingBox Bounds
        {
            get { return this.bounds; }
        }

        #endregion

        #region HasCenter -- 是否拥有中心

        private bool hasCenter;
        /// <summary>
        /// 是否拥有中心
        /// </summary>
        public bool HasCenter
        {
            get { return hasCenter; }
        }

        #endregion

        #region Center -- 中心

        private Vector3 center;
        /// <summary>
        /// 中心
        /// </summary>
        public Vector3 Center
        {
            get { return center; }
        }

        #endregion

        /// <summary>
        /// 更新边界与中心
        /// </summary>
        public void UpdateBoundsAndCenter()
        {
            this.UpdateAllTransformMatrix();
            this.hasBounds = this.TryGetBound(out this.bounds);
            this.hasCenter = this.TryGetCentroid(out this.center);
        }
    }
}
