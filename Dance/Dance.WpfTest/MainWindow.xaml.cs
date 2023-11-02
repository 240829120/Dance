using Dance.Common;
using Dance.Wpf;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;

namespace Dance.WpfTest
{
    public class TestItem : DanceModel
    {
        public string? Name { get; set; }
    }

    public class TestGroup : DanceModel
    {
        public string? Name { get; set; }

        public List<TestItem>? Items { get; set; }
    }

    public class MainWindowModel : DanceViewModel
    {
        public MainWindowModel()
        {
            this.DragBeginCommand = new RelayCommand<DanceDragBeginEventArgs>(this.DragBegin);
            this.DropCommand = new RelayCommand<DanceDragEventArgs>(this.Drop);
        }

        public RelayCommand<DanceDragBeginEventArgs> DragBeginCommand { get; set; }

        private void DragBegin(DanceDragBeginEventArgs? e)
        {
            if (e == null)
                return;

            e.Data = "this is a try.";
        }

        public RelayCommand<DanceDragEventArgs> DropCommand { get; set; }

        private void Drop(DanceDragEventArgs? e)
        {
            if (e == null)
                return;

            string? data = e.EventArgs.Data.GetData(typeof(string))?.ToString();
            DanceMessageExpansion.ShowNotify(ToolTipIcon.Info, "测试", data ?? string.Empty);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowModel();

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<TestGroup> groups = new();
            groups.Add(new TestGroup { Name = "Group 1" });
            groups.Add(new TestGroup { Name = "Group 2" });
            groups.Add(new TestGroup { Name = "Group 3" });

            groups[0].Items = new List<TestItem>();
            groups[0].Items.Add(new TestItem { Name = "Item 1 -- 1" });
            groups[0].Items.Add(new TestItem { Name = "Item 1 -- 2" });

            groups[1].Items = new List<TestItem>();
            groups[1].Items.Add(new TestItem { Name = "Item 2 -- 1" });
            groups[1].Items.Add(new TestItem { Name = "Item 2 -- 2" });

            groups[2].Items = new List<TestItem>();
            groups[2].Items.Add(new TestItem { Name = "Item 3 -- 1" });
            groups[2].Items.Add(new TestItem { Name = "Item 3 -- 2" });

            this.groupBox.ItemsSource = groups;
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current?.Dispose();
            System.Windows.Application.Current.Shutdown();
        }
    }
}
