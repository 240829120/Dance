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
    /// DanceMessageBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DanceMessageBoxWindow : Window
    {
        public DanceMessageBoxWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 消息框
        /// </summary>
        public DanceMessageBox MessageBox { get { return this.box; } }
    }
}
