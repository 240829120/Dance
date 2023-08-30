using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Dance.Wpf
{
    /// <summary>
    /// 粒子控制器基类
    /// </summary>
    [ContentProperty(nameof(Generator))]
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
            DependencyProperty.Register("Duration", typeof(DanceRangeTimeSpan), typeof(DanceParticleControllerBase), new PropertyMetadata(new DanceRangeTimeSpan(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10))));

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

        #region TranslateSpeedZ -- Z轴平移速度

        /// <summary>
        /// Z轴平移速度
        /// </summary>
        public DanceRangeFloat TranslateSpeedZ
        {
            get { return (DanceRangeFloat)GetValue(TranslateSpeedZProperty); }
            set { SetValue(TranslateSpeedZProperty, value); }
        }

        /// <summary>
        /// Z轴平移速度
        /// </summary>
        public static readonly DependencyProperty TranslateSpeedZProperty =
            DependencyProperty.Register("TranslateSpeedZ", typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new PropertyMetadata(new DanceRangeFloat(10, 20)));

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

        #region ExcitationTranslateSpeedX -- X轴平移扰动

        /// <summary>
        /// X轴平移扰动
        /// </summary>
        public float ExcitationTranslateSpeedX
        {
            get { return (float)GetValue(ExcitationTranslateSpeedXProperty); }
            set { SetValue(ExcitationTranslateSpeedXProperty, value); }
        }

        /// <summary>
        /// X轴平移扰动
        /// </summary>
        public static readonly DependencyProperty ExcitationTranslateSpeedXProperty =
            DependencyProperty.Register("ExcitationTranslateSpeedX", typeof(float), typeof(DanceParticleControllerBase), new PropertyMetadata(0.05f));

        #endregion

        #region ExcitationTranslateSpeedY -- Y轴平移扰动

        /// <summary>
        /// Y轴平移扰动
        /// </summary>
        public float ExcitationTranslateSpeedY
        {
            get { return (float)GetValue(ExcitationTranslateSpeedYProperty); }
            set { SetValue(ExcitationTranslateSpeedYProperty, value); }
        }

        /// <summary>
        /// Y轴平移扰动
        /// </summary>
        public static readonly DependencyProperty ExcitationTranslateSpeedYProperty =
            DependencyProperty.Register("ExcitationTranslateSpeedY", typeof(float), typeof(DanceParticleControllerBase), new PropertyMetadata(0.05f));

        #endregion

        #region ExcitationTranslateSpeedZ -- Z轴平移扰动

        /// <summary>
        /// Z轴平移扰动
        /// </summary>
        public float ExcitationTranslateSpeedZ
        {
            get { return (float)GetValue(ExcitationTranslateSpeedZProperty); }
            set { SetValue(ExcitationTranslateSpeedZProperty, value); }
        }

        /// <summary>
        /// Z轴平移扰动
        /// </summary>
        public static readonly DependencyProperty ExcitationTranslateSpeedZProperty =
            DependencyProperty.Register("ExcitationTranslateSpeedZ", typeof(float), typeof(DanceParticleControllerBase), new PropertyMetadata(0.05f));

        #endregion

        #region ExcitationRotationSpeedX -- X轴旋转扰动

        /// <summary>
        /// X轴旋转扰动
        /// </summary>
        public float ExcitationRotationSpeedX
        {
            get { return (float)GetValue(ExcitationRotationSpeedXProperty); }
            set { SetValue(ExcitationRotationSpeedXProperty, value); }
        }

        /// <summary>
        /// X轴旋转扰动
        /// </summary>
        public static readonly DependencyProperty ExcitationRotationSpeedXProperty =
            DependencyProperty.Register("ExcitationRotationSpeedX", typeof(float), typeof(DanceParticleControllerBase), new PropertyMetadata(0.05f));

        #endregion

        #region ExcitationRotationSpeedY -- Y轴旋转扰动

        /// <summary>
        /// Y轴旋转扰动
        /// </summary>
        public float ExcitationRotationSpeedY
        {
            get { return (float)GetValue(ExcitationRotationSpeedYProperty); }
            set { SetValue(ExcitationRotationSpeedYProperty, value); }
        }

        /// <summary>
        /// Y轴旋转扰动
        /// </summary>
        public static readonly DependencyProperty ExcitationRotationSpeedYProperty =
            DependencyProperty.Register("ExcitationRotationSpeedY", typeof(float), typeof(DanceParticleControllerBase), new PropertyMetadata(0.05f));

        #endregion

        #region ExcitationRotationSpeedZ -- Z轴旋转扰动

        /// <summary>
        /// Z轴旋转扰动
        /// </summary>
        public float ExcitationRotationSpeedZ
        {
            get { return (float)GetValue(ExcitationRotationSpeedZProperty); }
            set { SetValue(ExcitationRotationSpeedZProperty, value); }
        }

        /// <summary>
        /// Z轴旋转扰动
        /// </summary>
        public static readonly DependencyProperty ExcitationRotationSpeedZProperty =
            DependencyProperty.Register("ExcitationRotationSpeedZ", typeof(float), typeof(DanceParticleControllerBase), new PropertyMetadata(0.05f));

        #endregion

        #region ShowTimePoint -- 显示时间点

        /// <summary>
        /// 显示时间点
        /// </summary>

        public TimeSpan ShowTimePoint
        {
            get { return (TimeSpan)GetValue(ShowTimePointProperty); }
            set { SetValue(ShowTimePointProperty, value); }
        }

        /// <summary>
        /// 显示时间点
        /// </summary>
        public static readonly DependencyProperty ShowTimePointProperty =
            DependencyProperty.Register("ShowTimePoint", typeof(TimeSpan), typeof(DanceParticleControllerBase), new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

        #endregion

        #region HideTimePoint -- 隐藏时间点

        /// <summary>
        /// 隐藏时间点
        /// </summary>
        public TimeSpan HideTimePoint
        {
            get { return (TimeSpan)GetValue(HideTimePointProperty); }
            set { SetValue(HideTimePointProperty, value); }
        }

        /// <summary>
        /// 隐藏时间点
        /// </summary>
        public static readonly DependencyProperty HideTimePointProperty =
            DependencyProperty.Register("HideTimePoint", typeof(TimeSpan), typeof(DanceParticleControllerBase), new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

        #endregion

        #region Perspective -- 透视变换值

        /// <summary>
        /// 透视变换值
        /// </summary>
        public float Perspective
        {
            get { return (float)GetValue(PerspectiveProperty); }
            set { SetValue(PerspectiveProperty, value); }
        }

        /// <summary>
        /// 透视变换值
        /// </summary>
        public static readonly DependencyProperty PerspectiveProperty =
            DependencyProperty.Register("Perspective", typeof(float), typeof(DanceParticleControllerBase), new PropertyMetadata(-0.001f));

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
                particle.X += seconds * particle.TranslateSpeedX * this.Random.NextFloat(1 - this.ExcitationTranslateSpeedX, 1 + this.ExcitationTranslateSpeedX);
                particle.Y += seconds * particle.TranslateSpeedY * this.Random.NextFloat(1 - this.ExcitationTranslateSpeedY, 1 + this.ExcitationTranslateSpeedY);
                particle.Z += seconds * particle.TranslateSpeedZ * this.Random.NextFloat(1 - this.ExcitationTranslateSpeedZ, 1 + this.ExcitationTranslateSpeedZ);
                particle.RotateX += seconds * particle.RotateSpeedX * this.Random.NextFloat(1 - this.ExcitationRotationSpeedX, 1 + this.ExcitationRotationSpeedX);
                particle.RotateY += seconds * particle.RotateSpeedY * this.Random.NextFloat(1 - this.ExcitationRotationSpeedY, 1 + this.ExcitationRotationSpeedY);
                particle.RotateZ += seconds * particle.RotateSpeedZ * this.Random.NextFloat(1 - this.ExcitationRotationSpeedZ, 1 + this.ExcitationRotationSpeedZ);

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
                SKMatrix44 matrix44 = SKMatrix44.CreateIdentity();
                matrix44.PostConcat(SKMatrix44.CreateRotationDegrees(1, 0, 0, particle.RotateX));
                matrix44.PostConcat(SKMatrix44.CreateRotationDegrees(0, 1, 0, particle.RotateY));
                matrix44.PostConcat(SKMatrix44.CreateRotationDegrees(0, 0, 1, particle.RotateZ));
                matrix44.PostConcat(SKMatrix44.CreateTranslation(particle.X, particle.Y, particle.Z));

                SKMatrix44 perspectiveMatrix = SKMatrix44.CreateIdentity();
                perspectiveMatrix[3, 2] = this.Perspective;
                matrix44.PostConcat(perspectiveMatrix);

                canvas.SetMatrix(matrix44.Matrix);
                particle.UpdatePaintAlpha(this.ShowTimePoint, this.HideTimePoint);
                particle.Draw(size, canvas);
            }
        }

        /// <summary>
        /// 获取粒子数量
        /// </summary>
        /// <returns>粒子数量</returns>
        public int GetParticleCount()
        {
            return this.Particles.Count;
        }
    }
}
