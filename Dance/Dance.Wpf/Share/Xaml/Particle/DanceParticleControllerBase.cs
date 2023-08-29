using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子构建器基类
    /// </summary>
    public abstract class DanceParticleControllerBase : DependencyObject, IDanceParticleController
    {
        /// <summary>
        /// 粒子集合
        /// </summary>
        protected List<IDanceParticle> Particles = new(100);

        #region Duration -- 持续时间

        /// <summary>
        /// 持续时间
        /// </summary>
        public DanceRangeTimeSpan Duration
        {
            get { return (DanceRangeTimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(DanceRangeTimeSpan), typeof(DanceParticleControllerBase), new PropertyMetadata(new DanceRangeTimeSpan(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20))));

        #endregion

        #region TranslateSpeedX -- X轴平移速度

        /// <summary>
        /// X轴平移速度
        /// </summary>
        public DanceRangeFloat TranslateSpeedX
        {
            get { return (DanceRangeFloat)GetValue(TranslateSpeedXProperty); }
            set { SetValue(TranslateSpeedXProperty, value); }
        }

        /// <summary>
        /// X轴平移速度
        /// </summary>
        public static readonly DependencyProperty TranslateSpeedXProperty =
            DependencyProperty.Register("TranslateSpeedX", typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new PropertyMetadata(DanceRangeFloat.Zero));

        #endregion

        #region TranslateSpeedY -- Y轴平移速度

        /// <summary>
        /// Y轴平移速度
        /// </summary>
        public DanceRangeFloat TranslateSpeedY
        {
            get { return (DanceRangeFloat)GetValue(TranslateSpeedYProperty); }
            set { SetValue(TranslateSpeedYProperty, value); }
        }

        /// <summary>
        /// Y轴平移速度
        /// </summary>
        public static readonly DependencyProperty TranslateSpeedYProperty =
            DependencyProperty.Register("TranslateSpeedY", typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new PropertyMetadata(DanceRangeFloat.Zero));

        #endregion

        #region RotationSpeedX -- X轴旋转速度

        /// <summary>
        /// X轴旋转速度
        /// </summary>
        public DanceRangeFloat RotationSpeedX
        {
            get { return (DanceRangeFloat)GetValue(RotationSpeedXProperty); }
            set { SetValue(RotationSpeedXProperty, value); }
        }

        /// <summary>
        /// X轴旋转速度
        /// </summary>
        public static readonly DependencyProperty RotationSpeedXProperty =
            DependencyProperty.Register("RotationSpeedX", typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new PropertyMetadata(DanceRangeFloat.Zero));

        #endregion

        #region RotationSpeedY -- Y轴旋转速度

        /// <summary>
        /// Y轴旋转速度
        /// </summary>
        public DanceRangeFloat RotationSpeedY
        {
            get { return (DanceRangeFloat)GetValue(RotationSpeedYProperty); }
            set { SetValue(RotationSpeedYProperty, value); }
        }

        /// <summary>
        /// Y轴旋转速度
        /// </summary>
        public static readonly DependencyProperty RotationSpeedYProperty =
            DependencyProperty.Register("RotationSpeedY", typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new PropertyMetadata(DanceRangeFloat.Zero));

        #endregion

        #region RotationSpeedZ -- Z轴旋转速度

        /// <summary>
        /// Z轴旋转速度
        /// </summary>
        public DanceRangeFloat RotationSpeedZ
        {
            get { return (DanceRangeFloat)GetValue(RotationSpeedZProperty); }
            set { SetValue(RotationSpeedZProperty, value); }
        }

        /// <summary>
        /// Z轴旋转速度
        /// </summary>
        public static readonly DependencyProperty RotationSpeedZProperty =
            DependencyProperty.Register("RotationSpeedZ", typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new PropertyMetadata(DanceRangeFloat.Zero));

        #endregion

        #region OpacityChangeTime -- 不透明度改变时间点

        /// <summary>
        /// 不透明度改变时间点
        /// </summary>
        public TimeSpan OpacityChangeTime
        {
            get { return (TimeSpan)GetValue(OpacityChangeTimeProperty); }
            set { SetValue(OpacityChangeTimeProperty, value); }
        }

        /// <summary>
        /// 不透明度改变时间点
        /// </summary>
        public static readonly DependencyProperty OpacityChangeTimeProperty =
            DependencyProperty.Register("OpacityChangeTime", typeof(TimeSpan), typeof(DanceParticleControllerBase), new PropertyMetadata(TimeSpan.FromSeconds(1)));

        #endregion

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="dt">渲染时间</param>
        public abstract void Generat(TimeSpan dt);

        /// <summary>
        /// 步骤
        /// </summary>
        /// <param name="dt">渲染时间</param>
        public void Step(TimeSpan dt)
        {

        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="dt">渲染时间</param>
        public void Destory(TimeSpan dt)
        {
            List<IDanceParticle> removeList = new();

            DateTime now = DateTime.Now;

            foreach (IDanceParticle particle in this.Particles)
            {
                if (particle.GeneratTime + particle.Duration < now)
                    continue;

                removeList.Add(particle);
            }

            foreach (IDanceParticle particle in removeList)
            {
                this.Particles.Remove(particle);
            }
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="size">绘制区域</param>
        /// <param name="canvas">画布</param>
        public void Draw(SKSize size, SKCanvas canvas)
        {
            foreach (IDanceParticle particle in this.Particles)
            {
                SKMatrix44 matrix44 = SKMatrix44.CreateIdentity();
                matrix44.PostConcat(SKMatrix44.CreateRotationDegrees(1, 0, 0, particle.RotateX));
                matrix44.PostConcat(SKMatrix44.CreateRotationDegrees(0, 1, 0, particle.RotateY));
                matrix44.PostConcat(SKMatrix44.CreateRotationDegrees(0, 0, 1, particle.RotateZ));

                canvas.SetMatrix(matrix44.Matrix);
                particle.Draw(size, canvas);
                canvas.ResetMatrix();
            }
        }
    }
}
