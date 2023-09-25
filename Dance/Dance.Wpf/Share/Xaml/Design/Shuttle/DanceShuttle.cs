using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 穿梭
    /// </summary>
    public class DanceShuttle : ContentControl
    {
        static DanceShuttle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceShuttle), new FrameworkPropertyMetadata(typeof(DanceShuttle)));
        }

        #region Orientation -- 方向

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// 方向
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DanceShuttle), new PropertyMetadata(Orientation.Horizontal));

        #endregion

        #region SwitchEasing -- 过渡函数

        /// <summary>
        /// 切换过渡函数
        /// </summary>
        public IEasingFunction SwitchEasing
        {
            get { return (IEasingFunction)GetValue(SwitchEasingProperty); }
            set { SetValue(SwitchEasingProperty, value); }
        }

        /// <summary>
        /// 切换过渡函数
        /// </summary>
        public static readonly DependencyProperty SwitchEasingProperty =
            DependencyProperty.Register("SwitchEasing", typeof(IEasingFunction), typeof(DanceShuttle), new PropertyMetadata(null));

        #endregion

        #region Duration -- 持续时间

        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(DanceShuttle), new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

        #endregion

        #region Value -- 穿梭值

        /// <summary>
        /// 穿梭值
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// 穿梭值
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(DanceShuttle), new PropertyMetadata(0d, new PropertyChangedCallback((s, e) =>
            {

            })));

        #endregion

        #region UnitLength -- 单位长度

        /// <summary>
        /// 单位长度
        /// </summary>
        public double UnitLength
        {
            get { return (double)GetValue(UnitLengthProperty); }
            set { SetValue(UnitLengthProperty, value); }
        }

        /// <summary>
        /// 单位长度
        /// </summary>
        public static readonly DependencyProperty UnitLengthProperty =
            DependencyProperty.Register("UnitLength", typeof(double), typeof(DanceShuttle), new PropertyMetadata(30d, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceShuttle shuttle)
                    return;

                shuttle.CancelKeyFrameAnimation("MOVE");
                shuttle.CreateKeyFrameAnimation().Double(ValueProperty, shuttle.SwitchEasing, new DanceAnimationKeyFrame<double>(shuttle.Value, 0),
                                                                                              new DanceAnimationKeyFrame<double>(shuttle.UnitLength * shuttle.Index, shuttle.Duration))
                       .Commit("MOVE");
            })));

        #endregion

        #region Index -- 索引

        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        /// <summary>
        /// 索引
        /// </summary>
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(DanceShuttle), new PropertyMetadata(-1, new PropertyChangedCallback((s, e) =>
            {
                if (s is not DanceShuttle shuttle)
                    return;

                shuttle.CancelKeyFrameAnimation("MOVE");
                shuttle.CreateKeyFrameAnimation().Double(ValueProperty, shuttle.SwitchEasing, new DanceAnimationKeyFrame<double>(shuttle.Value, 0),
                                                                                              new DanceAnimationKeyFrame<double>(shuttle.UnitLength * shuttle.Index, shuttle.Duration))
                       .Commit("MOVE");
            })));

        #endregion
    }
}