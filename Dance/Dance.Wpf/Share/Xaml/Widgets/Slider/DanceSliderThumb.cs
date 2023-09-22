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
    /// 滑块拖拽区域
    /// </summary>
    public class DanceSliderThumb : Control
    {
        static DanceSliderThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceSliderThumb), new FrameworkPropertyMetadata(typeof(DanceSliderThumb)));
        }

        /// <summary>
        /// 鼠标开始点
        /// </summary>
        private Point? StartPoint;

        /// <summary>
        /// 所属滑块布局容器
        /// </summary>
        private DanceSliderPanel? Panel;

        /// <summary>
        /// 鼠标按下
        /// </summary>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            this.StartPoint = e.GetPosition(this);

            this.CaptureMouse();
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            this.StartPoint = null;

            this.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (this.StartPoint == null)
                return;

            this.Panel ??= DanceXamlExpansion.GetVisualTreeParent<DanceSliderPanel>(this);
            if (this.Panel == null)
                return;

            Point endPoint = e.GetPosition(this);

            if (this.Panel.Orientation == Orientation.Vertical)
            {

            }
            else
            {

            }

        }
    }
}
