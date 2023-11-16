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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.WpfTest
{
    [ExpandableObject]
    public class Lession
    {
        [Category("基础"), Description("描述描述123")]
        public string? Name { get; set; }

        [Category("基础"), Description("描述描述123")]
        public int Age { get; set; }


    }

    [ExpandableObject]
    public class Teacher
    {

        [Category("基础"), Description("描述描述123")]
        public string? Name { get; set; }

        [Category("基础"), Description("描述描述123")]
        public int Age { get; set; }

        [Category("基础"), Description("描述描述1231111")]
        public List<Lession> Lessions { get; set; } = new() { new Lession { Name = "t1" } };


        [Category("基础"), Description("描述描述1231111")]
        public Lession Lession2 { get; set; } = new Lession();
    }

    public class Student
    {
        [Category("基础"), Description("描述描述123")]
        public string? Property_1_1 { get; set; }


        [Category("基础")]
        public int Property_1_2 { get; set; }


        [Category("基础")]
        public string? Property_1_3 { get; set; }

        [Category("基础")]
        public string? Property_1_4 { get; set; }


        [Category("基础2")]
        public System.Windows.Media.SolidColorBrush Brush { get; set; }

        [Category("基础2")]
        public Thickness Thickness { get; set; } = new Thickness(1, 2, 3, 4);

        [Category("基础2")]
        public DateTime DateTime { get; set; }

        [Category("基础2")]
        public TimeSpan TimeSpan { get; set; }

        [Category("基础2")]
        public System.Windows.Media.Color Color { get; set; }

        [Category("老师")]
        public Teacher Teacher { get; set; } = new();

        [Category("列表")]
        public List<Teacher> Items { get; set; } = new() { new Teacher { Name = "zs", Age = 17 } };

        [Category("列表")]
        public List<string> Items2 { get; set; } = new();

        [Category("列表")]
        public List<int> Items3 { get; set; } = new List<int>() { 1, 2, 3 };
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.propertyGrid.SelectedObject = new Student();
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current?.Dispose();
            System.Windows.Application.Current.Shutdown();
        }
    }
}
