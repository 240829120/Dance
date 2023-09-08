using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dance.Wpf
{
    /// <summary>
    /// DanceLogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DanceLogWindow : Window
    {
        public DanceLogWindow()
        {
            InitializeComponent();

            DanceLog.Logging -= DanceLog_Logging;
            DanceLog.Logging += DanceLog_Logging;

            this.Loaded += DanceLogWindow_Loaded;
            this.Closed += DanceLogWindow_Closed;
        }

        /// <summary>
        /// 最长日志数量
        /// </summary>
        public const int MAX_LOG_LINE = 1000;

        /// <summary>
        /// 窗口加载
        /// </summary>
        private void DanceLogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 确保只执行一次
            this.Loaded -= DanceLogWindow_Loaded;

            this.Left = 0;
            this.Top = 0;
            this.Topmost = true;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        private void DanceLogWindow_Closed(object? sender, EventArgs e)
        {
            DanceLog.Logging -= DanceLog_Logging;
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        private void DanceLog_Logging(object? sender, DanceLogEventArgs e)
        {
            if (this.tb.LineCount >= MAX_LOG_LINE)
            {
                this.tb.Clear();
            }

            this.tb.AppendText($"{e.Time:yyyy-MM-dd HH:mm:ss.fff} | {e.Trigger} ===> {e.Content}");
        }

        /// <summary>
        /// 清理
        /// </summary>
        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            this.tb.Clear();
        }
    }
}