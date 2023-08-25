using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Dynamics.Joints;

namespace Dance.Maui
{
    /// <summary>
    /// 距离关节
    /// </summary>
    public class DanceDistanceJoint : DanceLineSegmentJointBase
    {
        /// <summary>
        /// 关节
        /// </summary>
        public DistanceJoint? Joint { get; private set; }

        #region Frequency -- 弹性系数

        /// <summary>
        /// 弹性系数
        /// </summary>
        public float Frequency
        {
            get => (float)GetValue(FrequencyProperty);
            set => SetValue(FrequencyProperty, value);
        }

        /// <summary>
        /// 弹性系数
        /// </summary>
        public static readonly BindableProperty FrequencyProperty =
            BindableProperty.Create(nameof(Frequency), typeof(float), typeof(DanceDistanceJoint), 0.3f);

        #endregion

        #region DampingRatio -- 阻尼

        /// <summary>
        /// 阻尼
        /// </summary>
        public float DampingRatio
        {
            get => (float)GetValue(DampingRatioProperty);
            set => SetValue(DampingRatioProperty, value);
        }

        /// <summary>
        /// 阻尼
        /// </summary>
        public static readonly BindableProperty DampingRatioProperty =
            BindableProperty.Create(nameof(DampingRatio), typeof(float), typeof(DanceDistanceJoint), 0.3f);

        #endregion

        /// <summary>
        /// 获取或创建关节
        /// </summary>
        /// <param name="world">物理世界</param>
        /// <returns>关节</returns>
        public override Joint? GetOrCreateJoint(World world)
        {
            if (this.Joint != null)
                return this.Joint;

            if (this.BodyA == null || this.BodyA.GetOrCreateBody() is not Body bodyA)
                return null;

            if (this.BodyB == null || this.BodyB.GetOrCreateBody() is not Body bodyB)
                return null;

            if (this.BodyA == this.BodyB)
                return null;

            lock (this.lock_object)
            {
                if (this.Joint != null)
                    return this.Joint;

                this.Joint = JointFactory.CreateDistanceJoint(world, bodyA, bodyB, new Vector2(this.AnchorA.X, this.AnchorA.Y), new Vector2(this.AnchorB.X, this.AnchorB.Y));
                this.Joint.Frequency = this.Frequency;
                this.Joint.DampingRatio = this.DampingRatio;
                this.Joint.Breakpoint = this.Breakpoint;

                return this.Joint;
            }
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="world">物理世界</param>
        /// <param name="joint">关节</param>
        /// <param name="canvas">画布</param>
        /// <param name="dirtyRect">绘制区域</param>
        public override void Draw(World world, Joint joint, ICanvas canvas, RectF dirtyRect)
        {
            if (joint is not DistanceJoint distanceJoint)
                return;

            canvas.StrokeColor = this.StrokeColor;
            canvas.StrokeSize = this.StrokeSize;
            canvas.StrokeDashPattern = this.StrokeDashPattern.ToArray();
            canvas.DrawLine(distanceJoint.WorldAnchorA.X, distanceJoint.WorldAnchorA.Y, distanceJoint.WorldAnchorB.X, distanceJoint.WorldAnchorB.Y);
        }
    }
}
