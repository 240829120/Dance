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
    /// 粒子控制器基类
    /// </summary>
    public abstract class DanceParticleControllerBase : DependencyObject, IDanceParticleController
    {
        /// <summary>
        /// 生成数量
        /// </summary>
        private float GenerateCount;

        /// <summary>
        /// 粒子集合
        /// </summary>
        protected List<IDanceParticle> Particles = new(100);

        /// <summary>
        /// 随机数
        /// </summary>
        protected Random Random = new();

        #region Generator -- 粒子构建器

        /// <summary>
        /// 粒子构建器
        /// </summary>
        public IDanceParticleGenerator Generator
        {
            get { return (IDanceParticleGenerator)GetValue(GeneratorProperty); }
            set { SetValue(GeneratorProperty, value); }
        }

        /// <summary>
        /// 粒子构建器
        /// </summary>
        public static readonly DependencyProperty GeneratorProperty =
            DependencyProperty.Register("Generator", typeof(IDanceParticleGenerator), typeof(DanceParticleControllerBase), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceParticleControllerBase controller)
                    return;

                controller.GenerateCount = 0;

            })));

        #endregion

        #region GenerateSpeed -- 生成速度

        /// <summary>
        /// 生成速度
        /// </summary>
        public float GenerateSpeed
        {
            get { return (float)GetValue(GenerateSpeedProperty); }
            set { SetValue(GenerateSpeedProperty, value); }
        }

        /// <summary>
        /// 生成速度
        /// </summary>
        public static readonly DependencyProperty GenerateSpeedProperty =
            DependencyProperty.Register("GenerateSpeed", typeof(float), typeof(DanceParticleControllerBase), new PropertyMetadata(0.05f));

        #endregion

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
        /// <returns>新生成的粒子</returns>
        public virtual IList<IDanceParticle> Generate(TimeSpan dt)
        {
            List<IDanceParticle> list = new();

            this.GenerateCount += this.GenerateSpeed;
            if (this.GenerateCount < 1f || this.Generator == null)
                return list;

            int count = (int)this.GenerateCount;
            this.GenerateCount -= count;

            for (int i = 0; i < count; i++)
            {
                IDanceParticle particle = this.Generator.Generate();
                particle.GeneratTime = DateTime.Now;
                particle.Duration = this.Random.NextTimeSpan(this.Duration.MinValue, this.Duration.MaxValue);

                list.Add(particle);
            }

            this.Particles.AddRange(list);

            return list;
        }

        /// <summary>
        /// 步骤
        /// </summary>
        /// <param name="dt">渲染时间</param>
        public void Step(TimeSpan dt)
        {
            float seconds = (float)dt.TotalSeconds;

            foreach (IDanceParticle particle in this.Particles)
            {
                particle.X += seconds * particle.TranslateSpeedX;
                particle.Y += seconds * particle.TranslateSpeedY;
                particle.RotateX += seconds * particle.RotateSpeedX;
                particle.RotateY += seconds * particle.RotateSpeedY;
                particle.RotateZ += seconds * particle.RotateSpeedZ;

                particle.RotateX %= 360f;
                particle.RotateY %= 360f;
                particle.RotateZ %= 360f;
            }
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
                if (now < particle.GeneratTime + particle.Duration)
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
                // Translate center to origin
                SKMatrix matrix = SKMatrix.CreateTranslation(particle.X, particle.Y);

                // Use 3D matrix for 3D rotations and perspective
                SKMatrix44 matrix44 = SKMatrix44.CreateIdentity();
                matrix.PostConcat(SKMatrix44.CreateRotationDegrees(1, 0, 0, particle.RotateX).Matrix);
                matrix44.PostConcat(SKMatrix44.CreateRotationDegrees(0, 1, 0, particle.RotateY));
                matrix44.PostConcat(SKMatrix44.CreateRotationDegrees(0, 0, 1, particle.RotateZ));

                SKMatrix44 perspectiveMatrix = SKMatrix44.CreateIdentity();
                perspectiveMatrix[3, 2] = -1 / 250f;
                matrix44.PostConcat(perspectiveMatrix);

                matrix = matrix.PostConcat(matrix44.Matrix);

                // Set the matrix and display the bitmap
                canvas.SetMatrix(matrix);
                particle.Draw(size, canvas);
                canvas.ResetMatrix();
            }
        }
    }
}
