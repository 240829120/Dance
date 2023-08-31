using nkast.Aether.Physics2D.Dynamics;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Dance.Maui
{
    /// <summary>
    /// 粒子控制器基类
    /// </summary>
    [ContentProperty(nameof(Generator))]
    public abstract class DanceParticleControllerBase : BindableObject, IDanceParticleController
    {
        /// <summary>
        /// 总更新时间
        /// </summary>
        private TimeSpan TotalUpdateTime;

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
        public static readonly BindableProperty GeneratorProperty =
            BindableProperty.Create(nameof(Generator), typeof(IDanceParticleGenerator), typeof(DanceParticleControllerBase), null, propertyChanged: (s, o, n) =>
            {
                if (s is not DanceParticleControllerBase controller)
                    return;

                controller.TotalUpdateTime = TimeSpan.Zero;
            });

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
        public static readonly BindableProperty GenerateSpeedProperty =
            BindableProperty.Create(nameof(GenerateSpeed), typeof(float), typeof(DanceParticleControllerBase), 0.05f);

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
        public static readonly BindableProperty DurationProperty =
            BindableProperty.Create(nameof(Duration), typeof(DanceRangeTimeSpan), typeof(DanceParticleControllerBase), new DanceRangeTimeSpan(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)));

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
        public static readonly BindableProperty TranslateSpeedXProperty =
            BindableProperty.Create(nameof(TranslateSpeedX), typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new DanceRangeFloat(10, 20));

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
        public static readonly BindableProperty TranslateSpeedYProperty =
            BindableProperty.Create(nameof(TranslateSpeedY), typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new DanceRangeFloat(10, 20));

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
        public static readonly BindableProperty TranslateSpeedZProperty =
            BindableProperty.Create(nameof(TranslateSpeedZ), typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), new DanceRangeFloat(10, 20));

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
        public static readonly BindableProperty RotationSpeedXProperty =
            BindableProperty.Create(nameof(RotationSpeedX), typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), DanceRangeFloat.Zero);

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
        public static readonly BindableProperty RotationSpeedYProperty =
            BindableProperty.Create(nameof(RotationSpeedY), typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), DanceRangeFloat.Zero);

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
        public static readonly BindableProperty RotationSpeedZProperty =
            BindableProperty.Create(nameof(RotationSpeedZ), typeof(DanceRangeFloat), typeof(DanceParticleControllerBase), DanceRangeFloat.Zero);

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
        public static readonly BindableProperty ExcitationTranslateSpeedXProperty =
            BindableProperty.Create(nameof(ExcitationTranslateSpeedX), typeof(float), typeof(DanceParticleControllerBase), 0.05f);

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
        public static readonly BindableProperty ExcitationTranslateSpeedYProperty =
            BindableProperty.Create(nameof(ExcitationTranslateSpeedY), typeof(float), typeof(DanceParticleControllerBase), 0.05f);

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
        public static readonly BindableProperty ExcitationTranslateSpeedZProperty =
            BindableProperty.Create(nameof(ExcitationTranslateSpeedZ), typeof(float), typeof(DanceParticleControllerBase), 0.05f);

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
        public static readonly BindableProperty ExcitationRotationSpeedXProperty =
            BindableProperty.Create(nameof(ExcitationRotationSpeedX), typeof(float), typeof(DanceParticleControllerBase), 0.05f);

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
        public static readonly BindableProperty ExcitationRotationSpeedYProperty =
            BindableProperty.Create(nameof(ExcitationRotationSpeedY), typeof(float), typeof(DanceParticleControllerBase), 0.05f);

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
        public static readonly BindableProperty ExcitationRotationSpeedZProperty =
            BindableProperty.Create(nameof(ExcitationRotationSpeedZ), typeof(float), typeof(DanceParticleControllerBase), 0.05f);

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
        public static readonly BindableProperty ShowTimePointProperty =
            BindableProperty.Create(nameof(ShowTimePoint), typeof(TimeSpan), typeof(DanceParticleControllerBase), TimeSpan.FromSeconds(0.3));

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
        public static readonly BindableProperty HideTimePointProperty =
            BindableProperty.Create(nameof(HideTimePoint), typeof(TimeSpan), typeof(DanceParticleControllerBase), TimeSpan.FromSeconds(0.3));

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
        public static readonly BindableProperty PerspectiveProperty =
            BindableProperty.Create(nameof(Perspective), typeof(float), typeof(DanceParticleControllerBase), -0.001f);

        #endregion

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="dt">渲染时间</param>
        /// <returns>新生成的粒子</returns>
        public virtual IList<IDanceParticle> Generate(TimeSpan dt)
        {
            List<IDanceParticle> list = new();

            TimeSpan one = TimeSpan.FromSeconds(1) / this.GenerateSpeed;
            this.TotalUpdateTime += dt;

            int count = (int)((double)this.TotalUpdateTime.Ticks / one.Ticks);
            if (count > 0)
            {
                DateTime now = DateTime.Now;
                for (int i = 0; i < count; i++)
                {
                    IDanceParticle particle = this.Generator.Generate();
                    particle.GeneratTime = now;
                    particle.Duration = this.Random.NextTimeSpan(this.Duration.MinValue, this.Duration.MaxValue);

                    list.Add(particle);
                    this.Particles.Add(particle);
                }

                this.TotalUpdateTime = TimeSpan.Zero;
            }

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
