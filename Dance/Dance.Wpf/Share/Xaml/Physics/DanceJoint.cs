using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Dynamics.Joints;

namespace Dance.Wpf
{
    /// <summary>
    /// 关节元素
    /// </summary>
    public abstract class DanceJoint : DancePhysics
    {
        public DanceJoint()
        {
            this.StrokeDashPattern = new DanceFloatCollection();
        }

        /// <summary>
        /// 锁对象
        /// </summary>
        protected readonly object lock_object = new();

        #region StrokeColor -- 颜色

        /// <summary>
        /// 颜色
        /// </summary>
        public Color StrokeColor
        {
            get { return (Color)GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.Register("StrokeColor", typeof(Color), typeof(DanceJoint), new PropertyMetadata(Colors.Red));

        #endregion

        #region StrokeSize -- 大小

        /// <summary>
        /// 大小
        /// </summary>
        public float StrokeSize
        {
            get { return (float)GetValue(StrokeSizeProperty); }
            set { SetValue(StrokeSizeProperty, value); }
        }

        /// <summary>
        /// 大小
        /// </summary>
        public static readonly DependencyProperty StrokeSizeProperty =
            DependencyProperty.Register("StrokeSize", typeof(float), typeof(DanceJoint), new PropertyMetadata(1f));

        #endregion

        #region StrokeDashPattern -- 虚线配置

        /// <summary>
        /// 虚线配置
        /// </summary>
        public DanceFloatCollection StrokeDashPattern
        {
            get { return (DanceFloatCollection)GetValue(StrokeDashPatternProperty); }
            set { SetValue(StrokeDashPatternProperty, value); }
        }

        /// <summary>
        /// 虚线配置
        /// </summary>
        public static readonly DependencyProperty StrokeDashPatternProperty =
            DependencyProperty.Register("StrokeDashPattern", typeof(DanceFloatCollection), typeof(DanceJoint), new PropertyMetadata(null));

        #endregion

        #region Breakpoint -- 断点

        /// <summary>
        /// 断点
        /// </summary>
        public float Breakpoint
        {
            get { return (float)GetValue(BreakpointProperty); }
            set { SetValue(BreakpointProperty, value); }
        }

        /// <summary>
        /// 断点
        /// </summary>
        public static readonly DependencyProperty BreakpointProperty =
            DependencyProperty.Register("Breakpoint", typeof(float), typeof(DanceJoint), new PropertyMetadata(float.MaxValue));

        #endregion

        /// <summary>
        /// 获取或创建关节
        /// </summary>
        /// <param name="world">物理世界</param>
        /// <returns>关节</returns>
        public abstract Joint? GetOrCreateJoint(World world);

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="world">物理世界</param>
        /// <param name="joint">关节</param>
        /// <param name="canvas">画布</param>
        /// <param name="dirtyRect">绘制区域</param>
        public abstract void Draw(World world, Joint joint, DrawingContext canvas, Rect dirtyRect);
    }
}
