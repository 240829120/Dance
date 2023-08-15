using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 导航切换 -- 旋转左上角
    /// </summary>
    public class DanceNavigationSwitchProvider_RotationLeftTop : DanceNavigationSwitchProviderBase
    {
        /// <summary>
        /// 切换模式
        /// </summary>
        public override DanceNavigationSwitchMode SwitchMode => DanceNavigationSwitchMode.RotationLeftTop;

        /// <summary>
        /// 执行进入动画
        /// </summary>
        /// <param name="inIndex">进入索引</param>
        /// <param name="outIndex">退出索引</param>
        /// <param name="navigation">导航</param>
        /// <param name="view">进入导航视图项</param>
        /// <param name="easing">过渡函数</param>
        /// <param name="duration">持续时间</param>
        protected override void ExecuteIn(int inIndex, int? outIndex, DanceNavigationView navigation, DanceNavigationItem view, IEasingFunction easing, TimeSpan duration)
        {
            view.AnchorX = 0;
            view.AnchorY = 0;
            double angle = DanceNavigationSwitchRotationOption.GetAngle(navigation);

            if (outIndex == null || inIndex > outIndex)
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(true, duration))
                    .Double(DanceNavigationItem.RotationProperty, easing, new DanceAnimationKeyFrame<double>(-angle, 0), new DanceAnimationKeyFrame<double>(0, duration))
                    .Commit("IN");
            }
            else
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(true, duration))
                    .Double(DanceNavigationItem.RotationProperty, easing, new DanceAnimationKeyFrame<double>(angle, 0), new DanceAnimationKeyFrame<double>(0, duration))
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
        protected override void ExecuteOut(int? inIndex, int outIndex, DanceNavigationView navigation, DanceNavigationItem view, IEasingFunction easing, TimeSpan duration)
        {
            view.AnchorX = 0;
            view.AnchorY = 0;
            double angle = DanceNavigationSwitchRotationOption.GetAngle(navigation);

            if (inIndex == null || inIndex > outIndex)
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(false, duration))
                    .Double(DanceNavigationItem.RotationProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(angle, duration))
                    .Commit("OUT");
            }
            else
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(false, duration))
                    .Double(DanceNavigationItem.RotationProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(-angle, duration))
                    .Commit("OUT");
            }
        }

        /// <summary>
        /// 左扫
        /// </summary>
        /// <param name="navigation">导航</param>
        public override void LeftSwiped(DanceNavigationView navigation)
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
        /// 右扫
        /// </summary>
        /// <param name="navigation">导航</param>
        public override void RightSwiped(DanceNavigationView navigation)
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
