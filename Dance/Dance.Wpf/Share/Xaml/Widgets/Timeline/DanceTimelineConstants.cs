using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 时间线常量
    /// </summary>
    public static class DanceTimelineConstants
    {
        /// <summary>
        /// 默认Zoom为1时的1秒宽度
        /// </summary>
        public const double ONE_SECOND_DEFAULT_WIDTH = 1d;

        /// <summary>
        /// 最小缩放值
        /// </summary>
        public const double MIN_ZOOM = 1d;

        /// <summary>
        /// 最大缩放值
        /// </summary>
        public const double MAX_ZOOM = 100d;

        /// <summary>
        /// 刻度最小宽度
        /// </summary>
        public const double MIN_SCALE_WIDTH = 5d;

        /// <summary>
        /// 刻度间隔超过该值则绘制长刻度
        /// </summary>
        public const double MIN_LARGE_SCALE_WIDTH = 60d;

        /// <summary>
        /// 数字最小宽度
        /// </summary>
        public const double MIN_NUMBER_WIDTH = 30d;

        /// <summary>
        /// 元素最小宽度
        /// </summary>
        public static readonly TimeSpan MIN_ELEMENT_WIDTH = TimeSpan.FromSeconds(1);

        /// <summary>
        /// 字体
        /// </summary>
        public const string FONT_FAMILY = "Microsoft Yahei UI";
    }
}
