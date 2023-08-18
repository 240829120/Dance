using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D.Dynamics.Joints;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Common;
using System.Windows.Media;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 鼠标关节
    /// </summary>
    public class DanceMouseJoint : DanceJoint
    {
        /// <summary>
        /// 关节
        /// </summary>
        public FixedMouseJoint? Joint { get; private set; }

        #region Body -- 体元素

        /// <summary>
        /// 体元素
        /// </summary>
        public DanceBody Body
        {
            get { return (DanceBody)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        /// <summary>
        /// 体元素
        /// </summary>
        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register("Body", typeof(DanceBody), typeof(DanceMouseJoint), new PropertyMetadata(null));

        #endregion

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
            DependencyProperty.Register("Frequency", typeof(float), typeof(DanceMouseJoint), new PropertyMetadata(0.3f));

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
            DependencyProperty.Register("DampingRatio", typeof(float), typeof(DanceMouseJoint), new PropertyMetadata(0.3f));

        #endregion

        #region WorldAnchor -- 锚定点

        /// <summary>
        /// 锚定点
        /// </summary>
        public DancePoint WorldAnchor
        {
            get { return (DancePoint)GetValue(WorldAnchorProperty); }
            set { SetValue(WorldAnchorProperty, value); }
        }

        /// <summary>
        /// 锚定点
        /// </summary>
        public static readonly DependencyProperty WorldAnchorProperty =
            DependencyProperty.Register("WorldAnchor", typeof(DancePoint), typeof(DanceMouseJoint), new PropertyMetadata(DancePoint.Zero));

        #endregion

        #region MaxForce -- 最大力

        /// <summary>
        /// 最大力
        /// </summary>
        public float MaxForce
        {
            get { return (float)GetValue(MaxForceProperty); }
            set { SetValue(MaxForceProperty, value); }
        }

        /// <summary>
        /// 最大力
        /// </summary>
        public static readonly DependencyProperty MaxForceProperty =
            DependencyProperty.Register("MaxForce", typeof(float), typeof(DanceMouseJoint), new PropertyMetadata(100000f));

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

            if (this.Body == null || this.Body.GetOrCreateBody() is not Body body)
                return null;

            lock (this.lock_object)
            {
                if (this.Joint != null)
                    return this.Joint;

                this.Joint = JointFactory.CreateFixedMouseJoint(world, body, new Vector2(this.WorldAnchor.X, this.WorldAnchor.Y));
                this.Joint.Frequency = this.Frequency;
                this.Joint.DampingRatio = this.DampingRatio;
                this.Joint.Breakpoint = this.Breakpoint;
                this.Joint.MaxForce = this.MaxForce;

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
            if (joint is not FixedMouseJoint fixedMouseJoint)
                return;

            Pen pen = new(new SolidColorBrush(this.StrokeColor), this.StrokeSize)
            {
                DashStyle = new DashStyle(this.StrokeDashPattern.Cast<double>(), 0)
            };
            Point a = new(fixedMouseJoint.WorldAnchorA.X, fixedMouseJoint.WorldAnchorA.Y);
            Point b = new(fixedMouseJoint.WorldAnchorB.X, fixedMouseJoint.WorldAnchorB.Y);

            canvas.DrawLine(pen, a, b);
        }
    }
}
