using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nkast.Aether.Physics2D.Dynamics.Joints;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Common;
using System.Windows.Media;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 绳关节
    /// </summary>
    public class DanceRopeJoint : DanceLineSegmentJointBase
    {
        /// <summary>
        /// 关节
        /// </summary>
        public RopeJoint? Joint { get; private set; }

        #region MaxLength -- 最大长度

        /// <summary>
        /// 最大长度
        /// </summary>
        public float MaxLength
        {
            get { return (float)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        /// <summary>
        /// 最大长度
        /// </summary>
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(float), typeof(DanceRopeJoint), new PropertyMetadata(100f));

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

                this.Joint = JointFactory.CreateRopeJoint(world, bodyA, bodyB, new Vector2(this.AnchorA.X, this.AnchorA.Y), new Vector2(this.AnchorB.X, this.AnchorB.Y));
                this.Joint.MaxLength = this.MaxLength;
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
        public override void Draw(World world, Joint joint, DrawingContext canvas, Rect dirtyRect)
        {
            if (joint is not RopeJoint ropeJoint)
                return;

            Pen pen = new(new SolidColorBrush(this.StrokeColor), this.StrokeSize)
            {
                DashStyle = new DashStyle(this.StrokeDashPattern.Cast<double>(), 0)
            };
            Point a = new(ropeJoint.WorldAnchorA.X, ropeJoint.WorldAnchorA.Y);
            Point b = new(ropeJoint.WorldAnchorB.X, ropeJoint.WorldAnchorB.Y);

            canvas.DrawLine(pen, a, b);
        }
    }
}
