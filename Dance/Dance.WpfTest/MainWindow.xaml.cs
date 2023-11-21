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
using System.Collections;

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


        [Category("测试")]
        public HorizontalAlignment HorizontalAlignment { get; set; }
    }

    public class TimelineTrackItemModel
    {
        public TimeSpan BeginTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }

    public class TimelineTrackModel : DanceModel
    {
        public string Name { get; set; }

        #region IsSelected 

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; this.OnPropertyChanged(); }
        }

        #endregion

        public IList Items { get; } = new ObservableCollection<TimelineTrackItemModel>();
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
            //this.propertyGrid.SelectedObject = new Student();

            List<TimelineTrackModel> list = new()
            {
                new TimelineTrackModel() { Name = "轨道1" },
                new TimelineTrackModel() { Name = "轨道2" },
                new TimelineTrackModel() { Name = "轨道3" },
                new TimelineTrackModel() { Name = "轨道4" },
                new TimelineTrackModel() { Name = "轨道5" },
            };

            list[0].Items.Add(new TimelineTrackItemModel() { BeginTime = TimeSpan.Parse("00:00:01"), EndTime = TimeSpan.Parse("00:00:02") });
            list[0].Items.Add(new TimelineTrackItemModel() { BeginTime = TimeSpan.Parse("00:00:03"), EndTime = TimeSpan.Parse("00:00:10") });

            list[1].Items.Add(new TimelineTrackItemModel() { BeginTime = TimeSpan.Parse("00:01:01"), EndTime = TimeSpan.Parse("00:01:02") });
            list[1].Items.Add(new TimelineTrackItemModel() { BeginTime = TimeSpan.Parse("00:05:03"), EndTime = TimeSpan.Parse("00:05:10") });
            list[1].Items.Add(new TimelineTrackItemModel() { BeginTime = TimeSpan.Parse("00:10:01"), EndTime = TimeSpan.Parse("00:10:02") });
            list[1].Items.Add(new TimelineTrackItemModel() { BeginTime = TimeSpan.Parse("00:20:03"), EndTime = TimeSpan.Parse("00:20:10") });

            this.timeline.ItemsSource = list;
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current?.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.timeline.IsPlaying = !this.timeline.IsPlaying;
        }
    }
}
