using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 资源键
    /// </summary>
    public static class DanceResourceKey
    {
        // -----------------------------------------------------------------------------------------------------
        // 字体
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 字体 -- 大小
        /// </summary>
        public const string FONT_SIZE = "FONT_SIZE";

        /// <summary>
        /// 字体 -- 头部大小
        /// </summary>
        public const string FONT_SIZE_HEADER = "FONT_SIZE_HEADER";

        /// <summary>
        /// 字体
        /// </summary>
        public const string FONT_FAMILY = "FONT_FAMILY";

        // -----------------------------------------------------------------------------------------------------
        // 动画
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 动画 -- 持续时间
        /// </summary>
        public const string ANIMATION_DURATION = "ANIMATION_DURATION";

        /// <summary>
        /// 动画 -- 持续时间 -- TimeSpan
        /// </summary>
        public const string ANIMATION_DURATION_TIMESPAN = "ANIMATION_DURATION_TIMESPAN";

        // -----------------------------------------------------------------------------------------------------
        // 控件
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 控件禁用透明度
        /// </summary>
        public const string CONTROL_OPACITY_DISABLED = "CONTROL_DISABLED_OPACITY";

        // -----------------------------------------------------------------------------------------------------
        // 前景
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 前景
        /// </summary>
        public const string FOREGROUND_BRUSH = "FOREGROUND_BRUSH";

        /// <summary>
        /// 前景 -- 选中
        /// </summary>
        public const string FOREGROUND_BRUSH_SELECTED = "FOREGROUND_BRUSH_SELECTED";

        /// <summary>
        /// 前景 -- 鼠标滑过
        /// </summary>
        public const string FOREGROUND_BRUSH_MOUSE_OVER = "FOREGROUND_BRUSH_MOUSE_OVER";

        /// <summary>
        /// 前景 -- 按下
        /// </summary>
        public const string FOREGROUND_BRUSH_PRESSED = "FOREGROUND_BRUSH_PRESSED";

        // -----------------------------------------------------------------------------------------------------
        // 背景
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 背景
        /// </summary>
        public const string BACKGROUND_BRUSH = "BACKGROUND_BRUSH";

        /// <summary>
        /// 背景 -- 鼠标滑过
        /// </summary>
        public const string BACKGROUND_BRUSH_MOUSE_OVER = "BACKGROUND_BRUSH_MOUSE_OVER";

        /// <summary>
        /// 背景 -- 选中
        /// </summary>
        public const string BACKGROUND_BRUSH_SELECTED = "BACKGROUND_BRUSH_SELECTED";

        /// <summary>
        /// 背景 -- 按下
        /// </summary>
        public const string BACKGROUND_BRUSH_PRESSED = "BACKGROUND_BRUSH_PRESSED";

        // -----------------------------------------------------------------------------------------------------
        // 边框
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 边框
        /// </summary>
        public const string BORDER_BRUSH = "BORDER_BRUSH";

        /// <summary>
        /// 边框 -- 鼠标滑过
        /// </summary>
        public const string BORDER_BRUSH_MOUSE_OVER = "BORDER_BRUSH_MOUSE_OVER";

        /// <summary>
        /// 边框 -- 选中
        /// </summary>
        public const string BORDER_BRUSH_SELECTED = "BORDER_BRUSH_SELECTED";

        /// <summary>
        /// 边框 -- 按下
        /// </summary>
        public const string BORDER_BRUSH_PRESSED = "BORDER_BRUSH_PRESSED";

        /// <summary>
        /// 边框 -- 宽度
        /// </summary>
        public const string BORDER_THICKNESS = "BORDER_THICKNESS";

        /// <summary>
        /// 边框 -- 圆角
        /// </summary>
        public const string BORDER_CORNERRADIUS = "BORDER_CORNERRADIUS";

        // -----------------------------------------------------------------------------------------------------
        // 编辑
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 编辑 -- 宽度
        /// </summary>
        public const string EDIT_WIDTH = "EDIT_WIDTH";

        /// <summary>
        /// 编辑 -- 高度
        /// </summary>
        public const string EDIT_HEIGHT = "EDIT_HEIGHT";

        /// <summary>
        /// 编辑 -- 高度 -- 中号
        /// </summary>
        public const string EDIT_HEIGHT_MIDDLE = "EDIT_HEIGHT_MIDDLE";

        /// <summary>
        /// 编辑 -- 高度 -- 大号
        /// </summary>
        public const string EDIT_HEIGHT_LARGE = "EDIT_HEIGHT_LARGE";

        /// <summary>
        /// 编辑 -- 高度负数
        /// </summary>
        public const string EDIT_HEIGHT_NEGATIVE = "EDIT_HEIGHT_NEGATIVE";

        /// <summary>
        /// 编辑 -- 图标 -- 大小
        /// </summary>
        public const string EDIT_ICON_SIZE = "EDIT_ICON_SIZE";

        /// <summary>
        /// 编辑 -- 部件 -- 宽度
        /// </summary>
        public const string EDIT_PART_WIDTH = "EDIT_PART_WIDTH";

        /// <summary>
        /// 编辑 -- 部件 -- 宽度 -- 中号
        /// </summary>
        public const string EDIT_PART_WIDTH_MIDDLE = "EDIT_PART_WIDTH_MIDDLE";

        /// <summary>
        /// 编辑 -- 部件 -- 宽度 -- 大号
        /// </summary>
        public const string EDIT_PART_WIDTH_LARGE = "EDIT_PART_WIDTH_LARGE";

        /// <summary>
        /// 编辑 -- 部件 -- 高度
        /// </summary>
        public const string EDIT_PART_HEIGHT = "EDIT_PART_HEIGHT";

        /// <summary>
        /// 编辑 -- 部件 -- 高度 -- 中号
        /// </summary>
        public const string EDIT_PART_HEIGHT_MIDDLE = "EDIT_PART_HEIGHT_MIDDLE";

        /// <summary>
        /// 编辑 -- 部件 -- 内容外边距
        /// </summary>
        public const string EDIT_CONTENT_MARGIN = "EDIT_CONTENT_MARGIN";

        /// <summary>
        /// 编辑 -- 部件 -- 内容外边距 -- 大号
        /// </summary>
        public const string EDIT_CONTENT_MARGIN_LARGE = "EDIT_PART_CONTENT_MARGIN_LARGE";

        // -----------------------------------------------------------------------------------------------------
        // 滚动
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 滚动 -- 滑轮 -- 大小
        /// </summary>
        public const string SCROLL_BAR_SIZE = "SCROLL_BAR_SIZE";

        /// <summary>
        /// 滚动 -- 背景
        /// </summary>
        public const string SCROLL_BAR_BACKGROUND = "SCROLL_BAR_BACKGROUND";

        /// <summary>
        /// 滚动 -- 前景
        /// </summary>
        public const string SCROLL_BAR_FOREGROUND = "SCROLL_BAR_FOREGROUND";

        // -----------------------------------------------------------------------------------------------------
        // 弹出框
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 弹出框 -- 最大高度
        /// </summary>
        public const string POPUP_MAX_HEIGHT = "POPUP_MAX_HEIGHT";

        /// <summary>
        /// 弹出框 -- 外边距
        /// </summary>
        public const string POPUP_MARGIN = "POPUP_MARGIN";

        // -----------------------------------------------------------------------------------------------------
        // 路径
        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 路径 -- 正确标记
        /// </summary>
        public const string PATH_RIGHT_MARK = "PATH_RIGHT_MARK";

        /// <summary>
        /// 路径 -- 空标记
        /// </summary>
        public const string PATH_NONE_MARK = "PATH_NONE_MARK";

        /// <summary>
        /// 路径 -- 箭头标记
        /// </summary>
        public const string PATH_ARROW_MARK = "PATH_ARROW_MARK";
    }
}
