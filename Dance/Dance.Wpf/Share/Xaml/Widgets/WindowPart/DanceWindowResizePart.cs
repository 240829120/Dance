using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dance.Wpf
{
    /// <summary>
    /// 窗口拖拽改变模式
    /// </summary>
    public enum DanceWindowResizeMode
    {
        /// <summary>
        /// 左
        /// </summary>
        Left,

        /// <summary>
        /// 右
        /// </summary>
        Right,

        /// <summary>
        /// 上
        /// </summary>
        Top,

        /// <summary>
        /// 下
        /// </summary>
        Bottom,

        /// <summary>
        /// 左上
        /// </summary>
        LeftTop,

        /// <summary>
        /// 右上
        /// </summary>
        RightTop,

        /// <summary>
        /// 左下
        /// </summary>
        LeftBottom,

        /// <summary>
        /// 右下
        /// </summary>
        RightBottom
    }

    /// <summary>
    /// 窗口改变大小条
    /// </summary>
    public class DanceWindowResizePart : Control
    {
        static DanceWindowResizePart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceWindowResizePart), new FrameworkPropertyMetadata(typeof(DanceWindowResizePart)));
        }

        /// <summary>
        /// 开始坐标
        /// </summary>
        private Point? StartPoint;

        #region ResizeMode -- 缩放模式

        /// <summary>
        /// 缩放模式
        /// </summary>
        public DanceWindowResizeMode ResizeMode
        {
            get { return (DanceWindowResizeMode)GetValue(ResizeModeProperty); }
            set { SetValue(ResizeModeProperty, value); }
        }

        /// <summary>
        /// 缩放模式
        /// </summary>
        public static readonly DependencyProperty ResizeModeProperty =
            DependencyProperty.Register("ResizeMode", typeof(DanceWindowResizeMode), typeof(DanceWindowResizePart), new PropertyMetadata(DanceWindowResizeMode.Left));

        #endregion

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.StartPoint = e.GetPosition(this);
            this.CaptureMouse();
        }

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            this.StartPoint = null;
            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.StartPoint == null || e.LeftButton != MouseButtonState.Pressed)
                return;

            Point endPoint = e.GetPosition(this);

            switch (this.ResizeMode)
            {
                case DanceWindowResizeMode.Left: this.ExecuteLeft(this.StartPoint.Value, endPoint); break;
                case DanceWindowResizeMode.Right: this.ExecuteRight(this.StartPoint.Value, endPoint); break;
                case DanceWindowResizeMode.Top: this.ExecuteTop(this.StartPoint.Value, endPoint); break;
                case DanceWindowResizeMode.Bottom: this.ExecuteBottom(this.StartPoint.Value, endPoint); break;
                case DanceWindowResizeMode.LeftTop: this.ExecuteLeftTop(this.StartPoint.Value, endPoint); break;
                case DanceWindowResizeMode.LeftBottom: this.ExecuteLeftBottom(this.StartPoint.Value, endPoint); break;
                case DanceWindowResizeMode.RightTop: this.ExecuteRightTop(this.StartPoint.Value, endPoint); break;
                case DanceWindowResizeMode.RightBottom: this.ExecuteRightBottom(this.StartPoint.Value, endPoint); break;
            }
        }

        /// <summary>
        /// 执行 -- 左
        /// </summary>
        private void ExecuteLeft(Point start, Point end)
        {
            Window window = Window.GetWindow(this);
            double x = end.X - start.X;
            if (window.Width + x <= 0)
                return;

            window.Left += x;
            window.Width -= x;
        }

        /// <summary>
        /// 执行 -- 右
        /// </summary>
        /// <param name="start">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteRight(Point start, Point end)
        {
            Window window = Window.GetWindow(this);
            double x = end.X - start.X;
            if (window.Width + x <= 0)
                return;

            window.Width += x;
        }

        /// <summary>
        /// 执行 -- 上
        /// </summary>
        /// <param name="start">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteTop(Point start, Point end)
        {
            Window window = Window.GetWindow(this);
            double y = end.Y - start.Y;
            if (window.Height - y <= 0)
                return;

            window.Top += y;
            window.Height -= y;
        }

        /// <summary>
        /// 执行 -- 下
        /// </summary>
        /// <param name="start">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteBottom(Point start, Point end)
        {
            Window window = Window.GetWindow(this);
            double y = end.Y - start.Y;
            if (window.Height + y <= 0)
                return;

            window.Height += y;
        }

        /// <summary>
        /// 执行 -- 左上
        /// </summary>
        /// <param name="start">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteLeftTop(Point start, Point end)
        {
            this.ExecuteLeft(start, end);
            this.ExecuteTop(start, end);
        }

        /// <summary>
        /// 执行 -- 左下
        /// </summary>
        /// <param name="start">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteLeftBottom(Point start, Point end)
        {
            this.ExecuteLeft(start, end);
            this.ExecuteBottom(start, end);
        }

        /// <summary>
        /// 执行 -- 右上
        /// </summary>
        /// <param name="start">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteRightTop(Point start, Point end)
        {
            this.ExecuteRight(start, end);
            this.ExecuteTop(start, end);
        }

        /// <summary>
        /// 执行 -- 右下
        /// </summary>
        /// <param name="start">开始坐标</param>
        /// <param name="end">结束坐标</param>
        private void ExecuteRightBottom(Point start, Point end)
        {
            this.ExecuteRight(start, end);
            this.ExecuteBottom(start, end);
        }
    }
}
