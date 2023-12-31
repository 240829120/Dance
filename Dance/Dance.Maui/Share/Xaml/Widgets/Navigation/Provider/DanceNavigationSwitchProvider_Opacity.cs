﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 导航切换 -- 透明度
    /// </summary>
    public class DanceNavigationSwitchProvider_Opacity : DanceNavigationSwitchProviderBase
    {
        /// <summary>
        /// 切换模式
        /// </summary>
        public override DanceNavigationSwitchMode SwitchMode => DanceNavigationSwitchMode.Opacity;

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
            view.ClearKeyFrameAnimation();
            view.CreateKeyFrameAnimation()
                .Double(DanceNavigationItem.OpacityProperty, easing, new DanceAnimationKeyFrame<double>(0, 0), new DanceAnimationKeyFrame<double>(1, duration))
                .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(true, duration))
                .Commit("IN");
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
            view.ClearKeyFrameAnimation();
            view.CreateKeyFrameAnimation()
                .Double(DanceNavigationItem.OpacityProperty, easing, new DanceAnimationKeyFrame<double>(1, 0), new DanceAnimationKeyFrame<double>(0, duration))
                .Bool(DanceNavigationItem.IsVisibleProperty, easing, new DanceAnimationKeyFrame<bool>(true, 0), new DanceAnimationKeyFrame<bool>(false, duration))
                .Commit("OUT");
        }
    }
}
