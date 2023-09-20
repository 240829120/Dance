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
    /// 轻扫
    /// </summary>
    public class DanceSwipeGestureRecognizer : UIElement, IDanceGestureRecognizer
    {
        // ========================================================================================================================
        // Field

        /// <summary>
        /// 开始点
        /// </summary>
        private Point? BeginPoint;

        /// <summary>
        /// 目标引用
        /// </summary>
        private WeakReference<FrameworkElement>? Target;

        // ========================================================================================================================
        // Property

        #region EffectiveDistance -- 生效距离

        /// <summary>
        /// 生效距离
        /// </summary>
        public double EffectiveDistance
        {
            get { return (double)GetValue(EffectiveDistanceProperty); }
            set { SetValue(EffectiveDistanceProperty, value); }
        }

        /// <summary>
        /// 生效距离
        /// </summary>
        public static readonly DependencyProperty EffectiveDistanceProperty =
            DependencyProperty.Register("EffectiveDistance", typeof(double), typeof(DanceSwipeGestureRecognizer), new PropertyMetadata(50d));

        #endregion

        // ========================================================================================================================
        // Event

        #region Swiped -- 轻扫事件

        /// <summary>
        /// 轻扫委托 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SwipedEventHandler(object sender, DanceSwipeGestureRecognizerEventArgs e);

        /// <summary>
        /// 轻扫事件
        /// </summary>
        public event SwipedEventHandler Swiped
        {
            add { AddHandler(SwipedEvent, value); }
            remove { RemoveHandler(SwipedEvent, value); }
        }

        /// <summary>
        /// 轻扫事件
        /// </summary>
        public static readonly RoutedEvent SwipedEvent =
            EventManager.RegisterRoutedEvent("Swiped", RoutingStrategy.Direct, typeof(SwipedEventHandler), typeof(DanceSwipeGestureRecognizer));

        #endregion

        // ========================================================================================================================
        // Public Function

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="element">元素</param>
        public void Register(FrameworkElement element)
        {
            this.UnRegister();

            element.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
            element.MouseLeftButtonDown += Element_MouseLeftButtonDown;

            element.MouseLeftButtonUp -= Element_MouseLeftButtonUp;
            element.MouseLeftButtonUp += Element_MouseLeftButtonUp;

            element.MouseMove -= Element_MouseMove;
            element.MouseMove += Element_MouseMove;

            this.Target = new WeakReference<FrameworkElement>(element);
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void UnRegister()
        {
            if (this.Target == null || !this.Target.TryGetTarget(out var element))
                return;

            element.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
            element.MouseLeftButtonUp -= Element_MouseLeftButtonUp;
            element.MouseMove -= Element_MouseMove;

            this.Target = null;
        }

        // ========================================================================================================================
        // Private Function

        /// <summary>
        /// 鼠标按下
        /// </summary>
        private void Element_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            this.BeginPoint = e.GetPosition(element);
            element.CaptureMouse();
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        private void Element_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            this.BeginPoint = null;
            element.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        private void Element_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.BeginPoint == null || sender is not FrameworkElement element)
                return;

            Point point = e.GetPosition(element);
            double offsetX = point.X - this.BeginPoint.Value.X;
            double offsetY = point.Y - this.BeginPoint.Value.Y;

            if (Math.Abs(offsetX) >= this.EffectiveDistance)
            {
                DanceSwipeGestureRecognizerEventArgs args = new(offsetX < 0 ? DanceSwipeGestureRecognizerDirection.Left : DanceSwipeGestureRecognizerDirection.Right)
                {
                    RoutedEvent = SwipedEvent,
                    Source = element,
                };

                this.RaiseEvent(args);
                this.BeginPoint = null;

                return;
            }
            if (Math.Abs(offsetY) >= this.EffectiveDistance)
            {
                DanceSwipeGestureRecognizerEventArgs args = new(offsetY < 0 ? DanceSwipeGestureRecognizerDirection.Up : DanceSwipeGestureRecognizerDirection.Down)
                {
                    RoutedEvent = SwipedEvent,
                    Source = element,
                };

                this.RaiseEvent(args);
                this.BeginPoint = null;

                return;
            }
        }
    }
}
