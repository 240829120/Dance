using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 导航切换 -- 垂直移动和透明度
    /// </summary>
    public class DanceNavigationSwitchProvider_TranslationY_Opacity : DanceNavigationSwitchProviderBase
    {
        /// <summary>
        /// 切换模式
        /// </summary>
        public override DanceNavigationSwitchMode SwitchMode => DanceNavigationSwitchMode.TranslationY_Opacity;

        /// <summary>
        /// 执行进入动画
        /// </summary>
        /// <param name="inIndex">进入索引</param>
        /// <param name="outIndex">退出索引</param>
        /// <param name="navigation">导航</param>
        /// <param name="view">进入导航视图项</param>
        /// <param name="easing">过渡函数</param>
        /// <param name="duration">持续时间</param>
        protected override void ExecuteIn(int inIndex, int? outIndex, DanceNavigationView navigation, DanceNavigationItem view, Easing easing, TimeSpan duration)
        {
            if (outIndex == null || inIndex > outIndex)
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Double(DanceNavigationItem.OpacityProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(1, duration))
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(true, duration))
                    .Double(DanceNavigationItem.TranslationYProperty, easing, new DanceAnimationKeyFrame<double>(navigation.Height, 0), new DanceAnimationKeyFrame<double>(0, duration))
                    .Commit("IN");
            }
            else
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Double(DanceNavigationItem.OpacityProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(1, duration))
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(true, duration))
                    .Double(DanceNavigationItem.TranslationYProperty, easing, new DanceAnimationKeyFrame<double>(-navigation.Height, 0), new DanceAnimationKeyFrame<double>(0, duration))
                    .Commit("IN");
            }
        }

        /// <summary>
        /// 执行退出动画
        /// </summary>
        /// <param name="inIndex">进入索引</param>
        /// <param name="outIndex">退出索引</param>
        /// <param name="navigation">导航</param>
        /// <param name="view">退出导航视图项</param>
        /// <param name="easing">过渡函数</param>
        /// <param name="duration">持续时间</param>
        protected override void ExecuteOut(int? inIndex, int outIndex, DanceNavigationView navigation, DanceNavigationItem view, Easing easing, TimeSpan duration)
        {
            if (inIndex == null || inIndex > outIndex)
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Double(DanceNavigationItem.OpacityProperty, easing, new DanceAnimationKeyFrame<double>(1, 0), new DanceAnimationKeyFrame<double>(0, duration))
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(false, duration))
                    .Double(DanceNavigationItem.TranslationYProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(-navigation.Height, duration))
                    .Commit("OUT");
            }
            else
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Double(DanceNavigationItem.OpacityProperty, easing, new DanceAnimationKeyFrame<double>(1, 0), new DanceAnimationKeyFrame<double>(0, duration))
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(false, duration))
                    .Double(DanceNavigationItem.TranslationYProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(navigation.Height, duration))
                    .Commit("OUT");
            }
        }

        /// <summary>
        /// 上扫
        /// </summary>
        /// <param name="navigation">导航</param>
        public override void UpSwiped(DanceNavigationView navigation)
        {
            if (navigation.SelectedItem == null || navigation.ItemsSource == null || navigation.ItemsSource.Count <= 1)
                return;

            int oldIndex = navigation.ItemsSource.IndexOf(navigation.SelectedItem);
            int newIndex = oldIndex + 1;
            newIndex = Math.Min(newIndex, navigation.ItemsSource.Count - 1);
            if (newIndex == oldIndex)
                return;

            navigation.SelectedItem = navigation.ItemsSource[newIndex];
        }

        /// <summary>
        /// 下扫
        /// </summary>
        /// <param name="navigation">导航</param>
        public override void DownSwiped(DanceNavigationView navigation)
        {
            if (navigation.SelectedItem == null || navigation.ItemsSource == null || navigation.ItemsSource.Count <= 1)
                return;

            int oldIndex = navigation.ItemsSource.IndexOf(navigation.SelectedItem);
            int newIndex = oldIndex - 1;
            newIndex = Math.Max(newIndex, 0);
            if (newIndex == oldIndex)
                return;

            navigation.SelectedItem = navigation.ItemsSource[newIndex];
        }
    }
}
