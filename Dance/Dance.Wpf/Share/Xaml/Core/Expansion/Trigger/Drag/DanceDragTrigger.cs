using CommunityToolkit.Mvvm.Input;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 拖拽触发器
    /// </summary>
    public class DanceDragTrigger : DependencyObject
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly static ILog log = LogManager.GetLogger(typeof(DanceDragTrigger));

        /// <summary>
        /// 拖拽装饰器
        /// </summary>
        private static DanceDragAdorner? DragAdorner;

        /// <summary>
        /// 拖拽装饰层
        /// </summary>
        private static AdornerLayer? DragAdornerLayer;

        // ================================================================================================
        // Property

        #region DragBeginCommand -- 开始拖拽命令

        /// <summary>
        /// 获取开始拖拽命令
        /// </summary>
        public static RelayCommand<DanceDragBeginEventArgs> GetDragBeginCommand(DependencyObject obj)
        {
            return (RelayCommand<DanceDragBeginEventArgs>)obj.GetValue(DragBeginCommandProperty);
        }

        /// <summary>
        /// 设置开始拖拽命令
        /// </summary>
        public static void SetDragBeginCommand(DependencyObject obj, RelayCommand<DanceDragBeginEventArgs> value)
        {
            obj.SetValue(DragBeginCommandProperty, value);
        }

        /// <summary>
        /// 开始拖拽命令
        /// </summary>
        public static readonly DependencyProperty DragBeginCommandProperty =
            DependencyProperty.RegisterAttached("DragBeginCommand", typeof(RelayCommand<DanceDragBeginEventArgs>), typeof(DanceDragTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.MouseMove -= SourceElement_MouseMove;
                element.MouseMove += SourceElement_MouseMove;

            })));

        #endregion

        #region DragEnterCommand -- 拖拽进入命令

        /// <summary>
        /// 获取拖拽进入命令
        /// </summary>
        public static RelayCommand<DanceDragEventArgs> GetDragEnterCommand(DependencyObject obj)
        {
            return (RelayCommand<DanceDragEventArgs>)obj.GetValue(DragEnterCommandProperty);
        }

        /// <summary>
        /// 设置拖拽进入命令
        /// </summary>
        public static void SetDragEnterCommand(DependencyObject obj, RelayCommand<DanceDragEventArgs> value)
        {
            obj.SetValue(DragEnterCommandProperty, value);
        }

        /// <summary>
        /// 拖拽进入命令
        /// </summary>
        public static readonly DependencyProperty DragEnterCommandProperty =
            DependencyProperty.RegisterAttached("DragEnterCommand", typeof(RelayCommand<DanceDragEventArgs>), typeof(DanceDragTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.DragEnter -= TargetElement_DragEnter;
                element.DragEnter += TargetElement_DragEnter;
            })));

        #endregion

        #region DragLeaveCommand -- 拖拽离开命令

        /// <summary>
        /// 获取拖拽离开命令
        /// </summary>
        public static RelayCommand<DanceDragEventArgs> GetDragLeaveCommand(DependencyObject obj)
        {
            return (RelayCommand<DanceDragEventArgs>)obj.GetValue(DragLeaveCommandProperty);
        }

        /// <summary>
        /// 设置拖拽离开命令
        /// </summary>
        public static void SetDragLeaveCommand(DependencyObject obj, RelayCommand<DanceDragEventArgs> value)
        {
            obj.SetValue(DragLeaveCommandProperty, value);
        }

        /// <summary>
        /// 拖拽离开命令
        /// </summary>
        public static readonly DependencyProperty DragLeaveCommandProperty =
            DependencyProperty.RegisterAttached("DragLeaveCommand", typeof(RelayCommand<DanceDragEventArgs>), typeof(DanceDragTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.DragLeave -= TargetElement_DragLeave;
                element.DragLeave += TargetElement_DragLeave;
            })));

        #endregion

        #region DragOverCommand -- 拖拽滑过命令

        /// <summary>
        /// 获取拖拽滑过命令
        /// </summary>
        public static RelayCommand<DanceDragEventArgs> GetDragOverCommand(DependencyObject obj)
        {
            return (RelayCommand<DanceDragEventArgs>)obj.GetValue(DragOverCommandProperty);
        }

        /// <summary>
        /// 设置拖拽滑过命令
        /// </summary>
        public static void SetDragOverCommand(DependencyObject obj, RelayCommand<DanceDragEventArgs> value)
        {
            obj.SetValue(DragOverCommandProperty, value);
        }

        /// <summary>
        /// 拖拽滑过命令
        /// </summary>
        public static readonly DependencyProperty DragOverCommandProperty =
            DependencyProperty.RegisterAttached("DragOverCommand", typeof(RelayCommand<DanceDragEventArgs>), typeof(DanceDragTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.DragOver -= TargetElement_DragOver;
                element.DragOver += TargetElement_DragOver;
            })));

        #endregion

        #region DropCommand -- 结束拖拽命令

        /// <summary>
        /// 获取结束拖拽命令
        /// </summary>
        public static RelayCommand<DanceDragEventArgs> GetDropCommand(DependencyObject obj)
        {
            return (RelayCommand<DanceDragEventArgs>)obj.GetValue(DropCommandProperty);
        }

        /// <summary>
        /// 设置结束拖拽命令
        /// </summary>
        public static void SetDropCommand(DependencyObject obj, RelayCommand<DanceDragEventArgs> value)
        {
            obj.SetValue(DropCommandProperty, value);
        }

        /// <summary>
        /// 结束拖拽命令
        /// </summary>
        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.RegisterAttached("DropCommand", typeof(RelayCommand<DanceDragEventArgs>), typeof(DanceDragTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Drop -= TargetElement_Drop;
                element.Drop += TargetElement_Drop;

            })));

        #endregion

        // ================================================================================================
        // Private Function

        /// <summary>
        /// 源鼠标移动
        /// </summary>
        private static void SourceElement_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton != MouseButtonState.Pressed || sender is not FrameworkElement element || DragAdorner != null || Window.GetWindow(element) is not Window window)
                    return;

                if (GetDragBeginCommand(element) is not RelayCommand<DanceDragBeginEventArgs> cmd)
                    return;

                DanceDragBeginEventArgs args = new(element);
                cmd.Execute(args);
                if (args.IsCancel)
                    return;

                CompositionTarget.Rendering -= CompositionTarget_Rendering;
                CompositionTarget.Rendering += CompositionTarget_Rendering;

                DragAdorner = new DanceDragAdorner(element) { Opacity = 0.5 };
                DragAdornerLayer = AdornerLayer.GetAdornerLayer(window.Content as FrameworkElement);
                DragAdornerLayer.Add(DragAdorner);

                DragDrop.DoDragDrop(element, args.Data, args.Effects);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
                DragAdornerLayer?.Remove(DragAdorner);
                DragAdorner = null;
                DragAdornerLayer = null;
            }
        }

        /// <summary>
        /// 拖拽进入
        /// </summary>
        private static void TargetElement_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetDragEnterCommand(element) is not RelayCommand<DanceDragEventArgs> cmd)
                    return;

                DanceDragEventArgs args = new(element, e.Data);
                cmd.Execute(args);

                e.Effects = args.Effects;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 拖拽离开
        /// </summary>
        private static void TargetElement_DragLeave(object sender, DragEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetDragLeaveCommand(element) is not RelayCommand<DanceDragEventArgs> cmd)
                    return;

                DanceDragEventArgs args = new(element, e.Data);
                cmd.Execute(args);

                e.Effects = args.Effects;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 拖拽滑过
        /// </summary>
        private static void TargetElement_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetDragOverCommand(element) is not RelayCommand<DanceDragEventArgs> cmd)
                    return;

                DanceDragEventArgs args = new(element, e.Data);
                cmd.Execute(args);

                e.Effects = args.Effects;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 拖拽结束
        /// </summary>
        private static void TargetElement_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetDropCommand(element) is not RelayCommand<DanceDragEventArgs> cmd)
                    return;

                DanceDragEventArgs args = new(element, e.Data);
                cmd.Execute(args);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
                DragAdornerLayer?.Remove(DragAdorner);
                DragAdorner = null;
                DragAdornerLayer = null;
            }
        }

        /// <summary>
        /// 渲染
        /// </summary>
        private static void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            if (DragAdorner == null || DragAdornerLayer == null)
                return;

            DragAdornerLayer.Update();
        }
    }
}
