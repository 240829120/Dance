using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 导航切换处理器基类
    /// </summary>
    public abstract class DanceNavigationSwitchProviderBase
    {
        /// <summary>
        /// 切换模式
        /// </summary>
        public abstract DanceNavigationSwitchMode SwitchMode { get; }

        /// <summary>
        /// 切换
        /// </summary>
        /// <param name="navigation">导航</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        public void Switch(DanceNavigationView navigation, object? oldValue, object? newValue)
        {
            if (navigation.ItemsSource == null)
                return;

            int? outIndex = oldValue == null ? null : navigation.ItemsSource.IndexOf(oldValue);
            int? inIndex = newValue == null ? null : navigation.ItemsSource.IndexOf(newValue);

            IReadOnlyList<DanceNavigationItem> items = navigation.GetVisualElementChildren<DanceNavigationItem>();

            DanceNavigationItem? oldView = oldValue == null ? null : items.FirstOrDefault(p => p.BindingContext == oldValue);
            DanceNavigationItem? newView = newValue == null ? null : items.FirstOrDefault(p => p.BindingContext == newValue);


            if (oldView != null)
            {
                this.ExecuteOut(inIndex, outIndex ?? 0, navigation, oldView, navigation.SwitchEasing, navigation.SwitchDuration);
            }

            if (newView != null)
            {
                this.ExecuteIn(inIndex ?? 0, outIndex, navigation, newView, navigation.SwitchEasing, navigation.SwitchDuration);
            }
        }

        /// <summary>
        /// 执行进入动画
        /// </summary>
        /// <param name="inIndex">进入索引</param>
        /// <param name="outIndex">退出索引</param>
        /// <param name="navigation">导航</param>
        /// <param name="view">进入导航视图项</param>
        /// <param name="easing">过渡函数</param>
        /// <param name="duration">持续时间</param>
        protected abstract void ExecuteIn(int inIndex, int? outIndex, DanceNavigationView navigation, DanceNavigationItem view, IEasingFunction easing, TimeSpan duration);

        /// <summary>
        /// 执行退出动画
        /// </summary>
        /// <param name="inIndex">进入索引</param>
        /// <param name="outIndex">退出索引</param>
        /// <param name="navigation">导航</param>
        /// <param name="view">退出导航视图项</param>
        /// <param name="easing">过渡函数</param>
        /// <param name="duration">持续时间</param>
        protected abstract void ExecuteOut(int? inIndex, int outIndex, DanceNavigationView navigation, DanceNavigationItem view, IEasingFunction easing, TimeSpan duration);

        /// <summary>
        /// 左扫
        /// </summary>
        /// <param name="navigation">导航</param>
        public virtual void LeftSwiped(DanceNavigationView navigation) { }

        /// <summary>
        /// 右扫
        /// </summary>
        /// <param name="navigation">导航</param>
        public virtual void RightSwiped(DanceNavigationView navigation) { }

        /// <summary>
        /// 上扫
        /// </summary>
        /// <param name="navigation">导航</param>
        public virtual void UpSwiped(DanceNavigationView navigation) { }

        /// <summary>
        /// 下扫
        /// </summary>
        /// <param name="navigation">导航</param>
        public virtual void DownSwiped(DanceNavigationView navigation) { }
    }
}
