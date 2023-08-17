using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Media.Animation;

namespace Dance.Wpf
{
    /// <summary>
    /// 动画管理器
    /// </summary>
    public class DanceAnimationManager : DependencyObject
    {
        #region AnimationInfoDicProperty -- 动画信息字典

        /// <summary>
        /// 获取动画信息字典
        /// </summary>
        public static ConcurrentDictionary<string, DanceAnimationInfo> GetAnimationInfoDic(DependencyObject obj)
        {
            return (ConcurrentDictionary<string, DanceAnimationInfo>)obj.GetValue(AnimationInfoDicProperty);
        }

        /// <summary>
        /// 设置动画信息字典
        /// </summary>
        public static void SetAnimationInfoDic(DependencyObject obj, ConcurrentDictionary<string, DanceAnimationInfo> value)
        {
            obj.SetValue(AnimationInfoDicProperty, value);
        }

        /// <summary>
        /// 动画信息字典
        /// </summary>
        public static readonly DependencyProperty AnimationInfoDicProperty =
            DependencyProperty.RegisterAttached("AnimationInfoDic", typeof(ConcurrentDictionary<string, DanceAnimationInfo>), typeof(DanceAnimationManager), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 添加动画
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="name">名称</param>
        /// <param name="animation">动画</param>
        public static void AddAnimation(FrameworkElement element, string name, Storyboard animation)
        {
            ConcurrentDictionary<string, DanceAnimationInfo> dic = GetAnimationInfoDic(element);
            if (dic == null)
            {
                dic = new();
                SetAnimationInfoDic(element, dic);
            }

            dic[name] = new DanceAnimationInfo(name, animation);
        }

        /// <summary>
        /// 移除动画
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="names">名称</param>
        public static void RemoveAnimation(FrameworkElement element, params string[] names)
        {
            ConcurrentDictionary<string, DanceAnimationInfo> dic = GetAnimationInfoDic(element);
            if (dic == null)
                return;

            foreach (string name in names)
            {
                dic.TryRemove(name, out DanceAnimationInfo? info);
                if (info == null)
                    continue;

                info.Animation.Stop();
            }
        }

        /// <summary>
        /// 清理动画
        /// </summary>
        /// <param name="element">元素</param>
        public static void ClearAnimation(FrameworkElement element)
        {
            ConcurrentDictionary<string, DanceAnimationInfo> dic = GetAnimationInfoDic(element);
            if (dic == null)
                return;

            while (!dic.IsEmpty)
            {
                DanceAnimationInfo info = dic.FirstOrDefault().Value;
                info.Animation.Stop();

                dic.TryRemove(info.Name, out _);
            }
        }
    }
}
