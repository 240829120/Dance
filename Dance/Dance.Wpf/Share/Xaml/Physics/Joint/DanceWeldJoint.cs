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
    /// 焊接关节
    /// </summary>
    public class DanceWeldJoint : DanceLineSegmentJointBase
    {
        /// <summary>
        /// 关节
        /// </summary>
        public WeldJoint? Joint { get; private set; }

        #region Frequency -- 弹性系数

        /// <summary>
        /// 弹性系数
        /// </summary>
        public float Frequency
        {
            get { return (float)GetValue(FrequencyProperty); }
            set { SetValue(FrequencyProperty, value); }
        }

        /// <summary>
        /// 弹性系数
        /// </summary>
        public static readonly DependencyProperty FrequencyProperty =
            DependencyProperty.Register("Frequency", typeof(float), typeof(DanceWeldJoint), new PropertyMetadata(0.3f));

        #endregion

        #region DampingRatio -- 阻尼

        /// <summary>
        /// 阻尼
        /// </summary>
        public float DampingRatio
        {
            get { return (float)GetValue(DampingRatioProperty); }
            set { SetValue(DampingRatioProperty, value); }
        }

        /// <summary>
        /// 阻尼
        /// </summary>
        public static readonly DependencyProperty DampingRatioProperty =
            DependencyProperty.Register("DampingRatio", typeof(float), typeof(DanceWeldJoint), new PropertyMetadata(0.3f));

        #endregion

        #region ReferenceAngle -- 角度差

        /// <summary>
        /// 角度差
        /// </summary>
        public float ReferenceAngle
        {
            get { return (float)GetValue(ReferenceAngleProperty); }
            set { SetValue(ReferenceAngleProperty, value); }
        }

        /// <summary>
        /// 角度差
        /// </summary>
        public static readonly DependencyProperty ReferenceAngleProperty =
            DependencyProperty.Register("ReferenceAngle", typeof(float), typeof(DanceWeldJoint), new PropertyMetadata(0f));

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

                this.Joint = JointFactory.CreateWeldJoint(world, bodyA, bodyB, new Vector2(this.AnchorA.X, this.AnchorA.Y), new Vector2(this.AnchorB.X, this.AnchorB.Y));
                this.Joint.FrequencyHz = this.Frequency;
                this.Joint.DampingRatio = this.DampingRatio;
                this.Joint.ReferenceAngle = this.ReferenceAngle;
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
            if (joint is not WeldJoint weldJoint)
                return;

            Pen pen = new(new SolidColorBrush(this.StrokeColor), this.StrokeSize)
            {
                DashStyle = new DashStyle(this.StrokeDashPattern.Cast<double>(), 0)
            };
            Point a = new(weldJoint.WorldAnchorA.X, weldJoint.WorldAnchorA.Y);
            Point b = new(weldJoint.WorldAnchorB.X, weldJoint.WorldAnchorB.Y);

            canvas.DrawLine(pen, a, b);
        }
    }
}
