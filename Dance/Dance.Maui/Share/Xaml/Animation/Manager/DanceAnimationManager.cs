using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Dance.Maui
{
    /// <summary>
    /// 动画管理器
    /// </summary>
    public class DanceAnimationManager : BindableObject
    {
        #region AnimationInfoDicProperty -- 动画信息字典

        /// <summary>
        /// 动画信息字典
        /// </summary>
        /// <param name="view">元素</param>
        /// <returns>值</returns>
        public static ConcurrentDictionary<string, DanceAnimationInfo> GetAnimationInfoDic(BindableObject view)
        {
            return (ConcurrentDictionary<string, DanceAnimationInfo>)view.GetValue(AnimationInfoDicProperty);
        }

        /// <summary>
        /// 设置动画信息字典
        /// </summary>
        /// <param name="view">元素</param>
        /// <param name="value">值</param>
        public static void SetAnimationInfoDic(BindableObject view, double value)
        {
            view.SetValue(AnimationInfoDicProperty, value);
        }

        /// <summary>
        /// 动画信息字典
        /// </summary>
        public static readonly BindableProperty AnimationInfoDicProperty =
            BindableProperty.CreateAttached("AnimationInfoDic", typeof(ConcurrentDictionary<string, DanceAnimationInfo>), typeof(DanceAnimationManager), null, defaultValueCreator: b => new ConcurrentDictionary<string, DanceAnimationInfo>());

        #endregion

        /// <summary>
        /// 添加动画
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="name">名称</param>
        /// <param name="animation">动画</param>
        public static void AddAnimation(VisualElement element, string name, Animation animation)
        {
            ConcurrentDictionary<string, DanceAnimationInfo> dic = GetAnimationInfoDic(element);

            dic[name] = new DanceAnimationInfo(name, animation);
        }

        /// <summary>
        /// 移除动画
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="names">名称</param>
        public static void RemoveAnimation(VisualElement element, params string[] names)
        {
            ConcurrentDictionary<string, DanceAnimationInfo> dic = GetAnimationInfoDic(element);

            foreach (string name in names)
            {
                dic.TryRemove(name, out DanceAnimationInfo? info);
                if (info == null)
                    continue;

                info.Animation.Dispose();
            }
        }

        /// <summary>
        /// 清理动画
        /// </summary>
        /// <param name="element">元素</param>
        public static void ClearAnimation(VisualElement element)
        {
            ConcurrentDictionary<string, DanceAnimationInfo> dic = GetAnimationInfoDic(element);

            while (!dic.IsEmpty)
            {
                DanceAnimationInfo info = dic.FirstOrDefault().Value;
                info.Animation.Dispose();

                dic.TryRemove(info.Name, out _);
            }
        }
    }
}
