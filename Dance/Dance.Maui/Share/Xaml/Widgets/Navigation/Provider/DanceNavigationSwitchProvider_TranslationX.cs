﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 导航切换 -- 水平移动
    /// </summary>
    public class DanceNavigationSwitchProvider_TranslationX : DanceNavigationSwitchProviderBase
    {
        /// <summary>
        /// 切换模式
        /// </summary>
        public override DanceNavigationSwitchMode SwitchMode => DanceNavigationSwitchMode.TranslationX;

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
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(true, duration))
                    .Double(DanceNavigationItem.TranslationXProperty, easing, new DanceAnimationKeyFrame<double>(navigation.Width, 0), new DanceAnimationKeyFrame<double>(0, duration))
                    .Commit("IN");
            }
            else
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(true, duration))
                    .Double(DanceNavigationItem.TranslationXProperty, easing, new DanceAnimationKeyFrame<double>(-navigation.Width, 0), new DanceAnimationKeyFrame<double>(0, duration))
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
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(false, duration))
                    .Double(DanceNavigationItem.TranslationXProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(-navigation.Width, duration))
                    .Commit("OUT");
            }
            else
            {
                view.ClearKeyFrameAnimation();
                view.CreateKeyFrameAnimation()
                    .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(false, duration))
                    .Double(DanceNavigationItem.TranslationXProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(navigation.Width, duration))
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
