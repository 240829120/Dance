using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 导航切换模式
    /// </summary>
    public enum DanceNavigationSwitchMode
    {
        /// <summary>
        /// 是否可见
        /// </summary>
        IsVisible,

        /// <summary>
        /// 透明度
        /// </summary>
        Opacity,

        /// <summary>
        /// X平移
        /// </summary>
        TranslationX,

        /// <summary>
        /// X平移和透明度
        /// </summary>
        TranslationX_Opacity,

        /// <summary>
        /// Y平移
        /// </summary>
        TranslationY,

        /// <summary>
        /// Y平移和透明度
        /// </summary>
        TranslationY_Opacity,

        /// <summary>
        /// 左上角旋转 
        /// </summary>
        RotationLeftTop,

        /// <summary>
        /// 左上角旋转和透明度
        /// </summary>
        RotationLeftTop_Opacity,

        /// <summary>
        /// 中心旋转和透明度
        /// </summary>
        RotationCenter_Opacity
    }
}
