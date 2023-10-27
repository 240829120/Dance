using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Dance.Wpf
{
    /// <summary>
    /// 路径动画
    /// </summary>
    public class DancePathAnimation : Shape
    {
        static DancePathAnimation()
        {
            StretchProperty.AddOwner(typeof(DancePathAnimation), new FrameworkPropertyMetadata(Stretch.Uniform, (s, e) =>
            {
                if (s is not DancePathAnimation animation)
                    return;

                animation.UpdateStoryboard();
            }));

            StrokeThicknessProperty.AddOwner(typeof(DancePathAnimation), new FrameworkPropertyMetadata(1d, (s, e) =>
            {
                if (s is not DancePathAnimation animation)
                    return;

                animation.UpdateStoryboard();
            }));
        }

        public DancePathAnimation()
        {
            this.IsHitTestVisible = false;
            this.Fill = null;
            this.Loaded -= DancePathAnimation_Loaded;
            this.Loaded += DancePathAnimation_Loaded;
        }

        // ======================================================================================
        // Field

        /// <summary>
        /// 定义路径
        /// </summary>
        protected override Geometry DefiningGeometry => Data ?? Geometry.Empty;

        /// <summary>
        /// 动画
        /// </summary>
        private Storyboard? Storyboard;

        // ======================================================================================
        // Property

        #region Data -- 数据

        /// <summary>
        /// 数据
        /// </summary>
        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Geometry), typeof(DancePathAnimation), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePathAnimation animation)
                    return;

                animation.UpdateStoryboard();
            })));

        #endregion

        #region PathLength -- 路径长度

        /// <summary>
        /// 路径长度
        /// </summary>
        public double PathLength
        {
            get { return (double)GetValue(PathLengthProperty); }
            set { SetValue(PathLengthProperty, value); }
        }

        /// <summary>
        /// 路径长度
        /// </summary>
        public static readonly DependencyProperty PathLengthProperty =
            DependencyProperty.Register("PathLength", typeof(double), typeof(DancePathAnimation), new PropertyMetadata(10d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePathAnimation animation)
                    return;

                animation.UpdateStoryboard();
            })));

        #endregion

        #region Duration -- 持续时间

        /// <summary>
        /// 持续时间
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(DancePathAnimation), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(2)), new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePathAnimation animation)
                    return;

                animation.UpdateStoryboard();

            })));

        #endregion

        #region IsPlaying -- 是否启用

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public static readonly DependencyProperty IsPlayingProperty =
            DependencyProperty.Register("IsPlaying", typeof(bool), typeof(DancePathAnimation), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DancePathAnimation animation)
                    return;

                animation.UpdateStoryboard();

            })));

        #endregion

        #region RepeatBehavior -- 重复行为

        /// <summary>
        /// 重复行为
        /// </summary>
        public RepeatBehavior RepeatBehavior
        {
            get { return (RepeatBehavior)GetValue(RepeatBehaviorProperty); }
            set { SetValue(RepeatBehaviorProperty, value); }
        }

        /// <summary>
        /// 重复行为
        /// </summary>
        public static readonly DependencyProperty RepeatBehaviorProperty =
            DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(DancePathAnimation), new PropertyMetadata(RepeatBehavior.Forever));

        #endregion

        #region FillBehavior -- 完成行为

        /// <summary>
        /// 完成行为
        /// </summary>
        public FillBehavior FillBehavior
        {
            get { return (FillBehavior)GetValue(FillBehaviorProperty); }
            set { SetValue(FillBehaviorProperty, value); }
        }

        /// <summary>
        /// 完成行为
        /// </summary>
        public static readonly DependencyProperty FillBehaviorProperty =
            DependencyProperty.Register("FillBehavior", typeof(FillBehavior), typeof(DancePathAnimation), new PropertyMetadata(FillBehavior.Stop));

        #endregion

        // ======================================================================================
        // Private Function

        /// <summary>
        /// 元素加载
        /// </summary>
        private void DancePathAnimation_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateStoryboard();
        }

        /// <summary>
        /// 更新动画
        /// </summary>
        private void UpdateStoryboard()
        {
            this.Storyboard?.Stop();
            this.Storyboard = null;

            if (!this.IsPlaying || this.Data == null || this.Data.IsEmpty())
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }

            this.Visibility = Visibility.Visible;
            double totalLength = GetPathTotalLength(this.Data);
            this.StrokeDashArray = new DoubleCollection(new double[] { this.PathLength, totalLength - this.PathLength });

            this.Storyboard = new()
            {
                Duration = this.Duration,
                RepeatBehavior = this.RepeatBehavior,
                FillBehavior = this.FillBehavior
            };

            DoubleAnimationUsingKeyFrames frames = new();
            frames.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero),
                Value = 0
            });
            frames.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(this.Duration.TimeSpan),
                Value = -totalLength
            });

            Storyboard.SetTarget(frames, this);
            Storyboard.SetTargetProperty(frames, new PropertyPath(StrokeDashOffsetProperty));
            this.Storyboard.Children.Add(frames);

            this.Storyboard.Begin();
        }

        /// <summary>
        /// 获取路径总长度
        /// </summary>
        /// <param name="geometry">路径</param>
        /// <returns>路径总长度</returns>
        public static double GetPathTotalLength(Geometry geometry)
        {
            if (geometry == null)
                return 0;

            PathGeometry pg = PathGeometry.CreateFromGeometry(geometry);
            pg.GetPointAtFractionLength(0.0001, out Point point, out _);
            return (pg.Figures[0].StartPoint - point).Length * 10000;
        }
    }
}