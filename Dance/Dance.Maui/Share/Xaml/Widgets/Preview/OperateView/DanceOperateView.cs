using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 操作视图
    /// </summary>
    public class DanceOperateView : ContentView
    {
        public DanceOperateView()
        {
            this.ShowCommand = new RelayCommand(this.Show);
        }

        // ==========================================================================================================
        // Property

        #region IsShow -- 当前是否显示

        /// <summary>
        /// 当前是否显示
        /// </summary>
        public bool IsShow
        {
            get => (bool)GetValue(IsShowProperty);
            set => SetValue(IsShowProperty, value);
        }

        /// <summary>
        /// 当前是否显示
        /// </summary>
        public static readonly BindableProperty IsShowProperty =
            BindableProperty.Create(nameof(IsShow), typeof(bool), typeof(DanceOperateView), false);

        #endregion

        #region IsKeepShow -- 是否保持显示

        /// <summary>
        /// 是否保持显示
        /// </summary>
        public bool IsKeepShow
        {
            get => (bool)GetValue(IsKeepShowProperty);
            set => SetValue(IsKeepShowProperty, value);
        }

        /// <summary>
        /// 是否保持显示
        /// </summary>
        public static readonly BindableProperty IsKeepShowProperty =
            BindableProperty.Create(nameof(IsKeepShow), typeof(bool), typeof(DanceOperateView), false, BindingMode.OneWay, propertyChanged: IsKeepShowPropertyChanged);

        /// <summary>
        /// 是否保持显示属性改变后出发
        /// </summary>
        /// <param name="obj">绑定对象</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        private static void IsKeepShowPropertyChanged(BindableObject obj, object oldValue, object newValue)
        {
            if (obj is not DanceOperateView view || newValue is not bool value)
                return;

            if (value)
            {
                view.Show();
            }
            else
            {
                IDanceDelayManager? delayManager = DanceDomain.Current.LifeScope.Resolve<IDanceDelayManager>();
                delayManager?.Wait($"OperateView_Show_{view.GetHashCode()}", view.ShowDuration, () =>
                {
                    view.Dispatcher.Dispatch(view.Hide);
                });
            }
        }

        #endregion

        #region ShowDuration -- 显示持续时间

        /// <summary>
        /// 显示持续时间
        /// </summary>
        public TimeSpan ShowDuration
        {
            get => (TimeSpan)GetValue(ShowDurationProperty);
            set => SetValue(ShowDurationProperty, value);
        }

        /// <summary>
        /// 显示持续时间
        /// </summary>
        public static readonly BindableProperty ShowDurationProperty =
            BindableProperty.Create(nameof(ShowDuration), typeof(TimeSpan), typeof(DanceOperateView), TimeSpan.FromSeconds(5));

        #endregion

        #region ShowCommand -- 显示命令

        /// <summary>
        /// 显示命令
        /// </summary>
        public RelayCommand? ShowCommand
        {
            get => (RelayCommand?)GetValue(ShowCommandProperty);
            set => SetValue(ShowCommandProperty, value);
        }

        /// <summary>
        /// 显示命令
        /// </summary>
        public static readonly BindableProperty ShowCommandProperty =
            BindableProperty.Create(nameof(ShowCommand), typeof(RelayCommand), typeof(DanceOperateView), null);

        #endregion

        // ==========================================================================================================
        // Public Function

        /// <summary>
        /// 显示
        /// </summary>
        public void Show()
        {
            if (this.IsShow)
                return;

            this.CreateKeyFrameAnimation().Double(DanceOperateView.OpacityProperty, Easing.Linear, new DanceAnimationKeyFrame<double>(this.Opacity, 0), new DanceAnimationKeyFrame<double>(1, 0.7))
                                          .Bool(DanceOperateView.IsShowProperty, Easing.Linear, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(true, 0.7))
                                          .Commit("SHOW");

            IDanceDelayManager? delayManager = DanceDomain.Current.LifeScope.Resolve<IDanceDelayManager>();
            delayManager?.Wait($"OperateView_Show_{this.GetHashCode()}", this.ShowDuration, () =>
            {
                this.Dispatcher.Dispatch(this.Hide);
            });
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hide()
        {
            if (!this.IsShow || this.IsKeepShow)
                return;

            this.CreateKeyFrameAnimation().Double(DanceOperateView.OpacityProperty, Easing.Linear, new DanceAnimationKeyFrame<double>(this.Opacity, 0), new DanceAnimationKeyFrame<double>(0.00001, 0.7))
                                          .Bool(DanceOperateView.IsShowProperty, Easing.Linear, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(false, 0.7))
                                          .Commit("HIDE");
        }
    }
}
