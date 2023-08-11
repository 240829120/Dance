using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Joints;

namespace Dance.Maui
{
    /// <summary>
    /// 关节元素
    /// </summary>
    public abstract class DanceJoint : DancePhysics
    {
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
            get => (Color)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public static readonly BindableProperty StrokeColorProperty =
            BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(DanceJoint), Colors.Red);

        #endregion

        #region StrokeSize -- 大小

        /// <summary>
        /// 大小
        /// </summary>
        public float StrokeSize
        {
            get => (float)GetValue(StrokeSizeProperty);
            set => SetValue(StrokeSizeProperty, value);
        }

        /// <summary>
        /// 大小
        /// </summary>
        public static readonly BindableProperty StrokeSizeProperty =
            BindableProperty.Create(nameof(StrokeSize), typeof(float), typeof(DanceJoint), 1f);

        #endregion

        #region StrokeDashPattern -- 虚线配置

        /// <summary>
        /// 虚线配置
        /// </summary>
        public DanceFloatCollection StrokeDashPattern
        {
            get => (DanceFloatCollection)GetValue(StrokeDashPatternProperty);
            set => SetValue(StrokeDashPatternProperty, value);
        }

        /// <summary>
        /// 虚线配置
        /// </summary>
        public static readonly BindableProperty StrokeDashPatternProperty =
            BindableProperty.Create(nameof(StrokeDashPattern), typeof(DanceFloatCollection), typeof(DanceJoint), defaultValueCreator: b => new DanceFloatCollection());

        #endregion

        #region Breakpoint -- 断点

        /// <summary>
        /// 断点
        /// </summary>
        public float Breakpoint
        {
            get => (float)GetValue(BreakpointProperty);
            set => SetValue(BreakpointProperty, value);
        }

        /// <summary>
        /// 断点
        /// </summary>
        public static readonly BindableProperty BreakpointProperty =
            BindableProperty.Create(nameof(Breakpoint), typeof(float), typeof(DanceJoint), float.MaxValue);

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
        public abstract void Draw(World world, Joint joint, ICanvas canvas, RectF dirtyRect);
    }
}
